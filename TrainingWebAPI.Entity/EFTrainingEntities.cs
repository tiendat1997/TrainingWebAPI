namespace TrainingWebAPI.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EFTrainingEntities : DbContext
    {
        public EFTrainingEntities()
            : base("name=EFTrainingEntities")
        {
        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Cast> Casts { get; set; }
        public virtual DbSet<Director> Directors { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Reviewer> Reviewers { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Actor>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Actor>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<Actor>()
                .HasMany(e => e.Casts)
                .WithRequired(e => e.Actor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cast>()
                .Property(e => e.Role)
                .IsUnicode(false);

            modelBuilder.Entity<Director>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Director>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Director>()
                .HasMany(e => e.Movies)
                .WithMany(e => e.Directors)
                .Map(m => m.ToTable("MovieDirector").MapLeftKey("DirectorId").MapRightKey("MovieId"));

            modelBuilder.Entity<Genre>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Genre>()
                .HasMany(e => e.Movies)
                .WithMany(e => e.Genres)
                .Map(m => m.ToTable("MovieGenre").MapLeftKey("GenreId").MapRightKey("MovieId"));

            modelBuilder.Entity<Movie>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Movie>()
                .HasMany(e => e.Casts)
                .WithRequired(e => e.Movie)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Movie>()
                .HasMany(e => e.Ratings)
                .WithRequired(e => e.Movie)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reviewer>()
                .HasMany(e => e.Ratings)
                .WithRequired(e => e.Reviewer)
                .WillCascadeOnDelete(false);
        }
    }
}
