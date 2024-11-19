using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TemplateDotNet.Domains;

namespace TemplateDotNet.Migrations;

public partial class ApiDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ApiDbContext> _logger;
    public virtual DbSet<Test> Test { get; set; } = null!;

    public ApiDbContext(
        DbContextOptions<ApiDbContext> options,
        IConfiguration configuration,
        ILogger<ApiDbContext> logger
    ) : base(options)
    {
        _configuration = configuration;
        _logger = logger;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var credentials = Environment.GetEnvironmentVariable("TEMPLATE_DB");
        optionsBuilder.UseNpgsql(credentials);
    }
}
