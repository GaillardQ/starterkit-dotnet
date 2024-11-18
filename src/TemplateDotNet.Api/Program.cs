using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json.Serialization;
using TemplateDotNet.Middlewares;
using TemplateDotNet.Migrations;
using TemplateDotNet.Repositories;
using TemplateDotNet.Services;

var builder = WebApplication.CreateBuilder(args);

IConfigurationSection authentConf = builder.Configuration.GetSection("Authentification");

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Password = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri(authentConf.GetValue<string>("authority-config") ?? ""),
                TokenUrl = new Uri(authentConf.GetValue<string>("authority-token-url") ?? ""),
            }
        },
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference =new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="oauth2"
                }
            },
            Array.Empty<string>()
        }
    });
});


builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

// BDD
var credentials = Environment.GetEnvironmentVariable("TEMPLATE_DB");
builder.Services.AddDbContext<ApiDbContext>(options => options.UseNpgsql(credentials));

// Repositories
builder.Services.AddTransient<ITestRepository, TestRepository>();

// Services
builder.Services.AddScoped<ITestService, TestService>();

// Authentification

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = authentConf.GetValue<string>("authority");
    options.Audience = authentConf.GetValue<string>("client-id");
    options.RequireHttpsMetadata = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidAudience = "account"
    };
    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = OnTokenValidated
        
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c=>
    {
        c.OAuthClientId(builder.Configuration.GetSection("Authentification").GetValue<string>("client-id"));
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Utilisation du middleware de logging
app.UseMiddleware<RequestLoggingMiddleware>();

app.MapControllers();

await app.RunAsync();

Task OnTokenValidated(TokenValidatedContext  context)
{
    if (context.SecurityToken is JwtSecurityToken accessToken && context.Principal?.Identity is ClaimsIdentity identity)
    {
        identity.AddClaim(new Claim("access_token", accessToken.RawData));

        // flatten realm_access because Microsoft identity model doesn't support nested claims
        // by map it to Microsoft identity model, because automatic JWT bearer token mapping already processed here
        if (identity.IsAuthenticated)
        {
            // Ajout du claim l'e-mail
            var emailClaim = accessToken.Claims.ToList().Find((claim) => claim.Type == "email");
            if (emailClaim != null)
            {
                identity.AddClaim(new Claim(ClaimTypes.Email, emailClaim.Value));
            }
            // Ajout du claim ClientId
            if (identity.HasClaim((claim) => claim.Type == "azp"))
            {
                var clientIdClaim = identity.FindFirst((claim) => claim.Type == "azp");
                identity.AddClaim(new Claim("client_id", clientIdClaim!.Value));
            }
        }
    }

    return Task.CompletedTask;
}
