using Microsoft.EntityFrameworkCore;

namespace Jurr.Data
{
    public class JurrDbContext : DbContext
    {
        public JurrDbContext(DbContextOptions<JurrDbContext> options) : base(options) { }

        public DbSet<Admin> Admins => Set<Admin>();
        public DbSet<Post> Posts => Set<Post>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(u => u.AdminId);
                entity.Property(u => u.UserName).IsRequired().HasMaxLength(256);
                entity.Property(u => u.NormalizedUserName).IsRequired().HasMaxLength(256);
                entity.Property(u => u.Email).HasMaxLength(256);
                entity.Property(u => u.NormalizedEmail).HasMaxLength(256);
                entity.Property(u => u.PasswordHash).IsRequired();
                entity.ToTable("Admins");

                entity.HasMany(u => u.Posts)
                      .WithOne(p => p.Admin)
                      .HasForeignKey(p => p.AdminId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(p => p.PostId);
                entity.Property(p => p.Title).IsRequired().HasMaxLength(256);
                entity.ToTable("Posts");
            });
        }
    }
}
