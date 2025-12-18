using EvergreenWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EvergreenWebAPI;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Flower> Flowers { get; set; }

    public virtual DbSet<FlowersHexColor> FlowersHexColors { get; set; }

    public virtual DbSet<HexColor> HexColors { get; set; }

    public virtual DbSet<Tip> Tips { get; set; }

    public virtual DbSet<TipsFlower> TipsFlowers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserFavoriteFlower> UserFavoriteFlowers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flower>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("flowers_pkey");

            entity.ToTable("flowers");

            entity.HasIndex(e => e.NameLat, "flowers_name_lat_key").IsUnique();

            entity.HasIndex(e => e.NameUa, "flowers_name_ua_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(2000)
                .HasColumnName("description");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(250)
                .HasColumnName("image_path");
            entity.Property(e => e.Meaning)
                .HasMaxLength(1000)
                .HasColumnName("meaning");
            entity.Property(e => e.NameLat)
                .HasMaxLength(150)
                .HasColumnName("name_lat");
            entity.Property(e => e.NameUa)
                .HasMaxLength(150)
                .HasColumnName("name_ua");
            entity.Property(e => e.Symbolics)
                .HasMaxLength(500)
                .HasColumnName("symbolics");
            entity.Property(e => e.InspectRecomendations)
            .HasMaxLength(1000)
            .HasColumnName("inspection_recomendations");
        });

        modelBuilder.Entity<FlowersHexColor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("flowers_hex_colors_pkey");

            entity.ToTable("flowers_hex_colors");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.ColorId).HasColumnName("color_id");
            entity.Property(e => e.FlowerId).HasColumnName("flower_id");

            entity.HasOne(d => d.Color).WithMany(p => p.FlowersHexColors)
                .HasForeignKey(d => d.ColorId)
                .HasConstraintName("flowers_hex_colors_color_id_fkey");

            entity.HasOne(d => d.Flower).WithMany(p => p.FlowersHexColors)
                .HasForeignKey(d => d.FlowerId)
                .HasConstraintName("flowers_hex_colors_flower_id_fkey");
        });

        modelBuilder.Entity<HexColor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("hex_colors_pkey");

            entity.ToTable("hex_colors");

            entity.HasIndex(e => e.Color, "hex_colors_color_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Color)
                .HasMaxLength(7)
                .HasColumnName("color");
        });

        modelBuilder.Entity<Tip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tips_pkey");

            entity.ToTable("tips");

            entity.HasIndex(e => e.NameUa, "tips_name_ua_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.NameUa)
                .HasMaxLength(250)
                .HasColumnName("name_ua");
        });

        modelBuilder.Entity<TipsFlower>(entity =>
        {
            entity.HasKey(e => new { e.FlowerId, e.TipId }).HasName("tips_flowers_pkey");

            entity.ToTable("tips_flowers");

            entity.Property(e => e.FlowerId).HasColumnName("flower_id");
            entity.Property(e => e.TipId).HasColumnName("tip_id");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("description");

            entity.HasOne(d => d.Flower).WithMany(p => p.TipsFlowers)
                .HasForeignKey(d => d.FlowerId)
                .HasConstraintName("tips_flowers_flower_id_fkey");

            entity.HasOne(d => d.Tip).WithMany(p => p.TipsFlowers)
                .HasForeignKey(d => d.TipId)
                .HasConstraintName("tips_flowers_tip_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.IconPath)
                .HasMaxLength(200)
                .HasColumnName("icon_path");
            entity.Property(e => e.PasswordHashed)
                .HasMaxLength(150)
                .HasColumnName("password_hashed");
            entity.Property(e => e.Username)
                .HasMaxLength(150)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserFavoriteFlower>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_favorite_flowers_pkey");

            entity.ToTable("user_favorite_flowers");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.FlowerId).HasColumnName("flower_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Flower).WithMany(p => p.UserFavoriteFlowers)
                .HasForeignKey(d => d.FlowerId)
                .HasConstraintName("user_favorite_flowers_flower_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserFavoriteFlowers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_favorite_flowers_user_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}