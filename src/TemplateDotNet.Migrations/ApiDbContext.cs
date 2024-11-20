using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TemplateDotNet.Domains.Test;

namespace TemplateDotNet.Migrations;

public partial class ApiDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ApiDbContext> _logger;
    public virtual DbSet<User> User { get; set; } = null!;

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
       #region Entité User
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("user", schema: "public");
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id")
                .HasComment("Identifiant de l'utilisateur");
            entity.Property(e => e.FirstName)
                .HasColumnName("firstname")
                .HasComment("Prénom de l'utilisateur");
            entity.Property(e => e.LastName)
                .HasColumnName("lastname")
                .HasComment("Nom de l'utilisateur");
            entity.Property(e => e.Birthdate)
                .HasColumnName("lastname")
                .HasComment("Date de naissance de l'utilisateur");
            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()")
                .HasComment("Date de création de l'utilisateur");
            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("now()")
                .HasComment("Date d'update de l'utilisateur");
        });
        #endregion
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var credentials = Environment.GetEnvironmentVariable("TEMPLATE_DB");
        optionsBuilder.UseNpgsql(credentials);
    }
}
