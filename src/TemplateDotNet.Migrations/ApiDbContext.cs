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
        #region Entité Test
        modelBuilder.Entity<Test>(entity =>
        {
            entity.ToTable("test", schema: "public");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id")
                .HasComment("Identifiant du test");

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .HasComment("Nom du test");

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()")
                .HasComment("Date de création du test");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("now()")
                .HasComment("Date d'update du test");
        });
        #endregion

        #region Entité Line
        modelBuilder.Entity<Line>(entity =>
        {
            entity.ToTable("line", schema: "public");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id")
                .HasComment("Identifiant de la ligne");

            entity.Property(e => e.Number)
                .HasColumnName("number")
                .HasComment("Numéro de la ligne");

            entity.Property(e => e.IsError)
                .HasColumnName("is_error")
                .HasDefaultValueSql("false")
                .HasComment("Si c'est une erreur");

            entity.Property(e => e.ErrorMessage)
                .HasColumnName("error_message")
                .HasComment("Message d'erreur");

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()")
                .HasComment("Date de création de la ligne");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("now()")
                .HasComment("Date d'update de la ligne");

            entity.HasOne(l => l.Test)
                .WithMany(t => t.Lines)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("line_fk_test");
        });
        #endregion
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var credentials = Environment.GetEnvironmentVariable("TEMPLATE_DB");
        optionsBuilder.UseNpgsql(credentials);
    }
}
