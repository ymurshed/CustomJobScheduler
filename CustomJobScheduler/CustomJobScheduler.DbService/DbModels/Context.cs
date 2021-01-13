using Microsoft.EntityFrameworkCore;

namespace CustomJobScheduler.DbService.DbModels
{
    public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<Trigger> Trigger { get; set; }
        public virtual DbSet<TriggerType> TriggerType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB;initial catalog=CustomJobScheduler;Trusted_Connection=True");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Group)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'DEFAULT')");

                entity.Property(e => e.IsNew)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.JobKey)
                    .IsRequired()
                    .HasMaxLength(160);

                entity.Property(e => e.JobName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.OldGroup).HasMaxLength(50);

                entity.Property(e => e.OldJobName).HasMaxLength(100);

                entity.Property(e => e.Recovery)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Trigger>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateFormat).HasMaxLength(25);

                entity.Property(e => e.DateTimeFormat).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.EndTimeUtc).HasMaxLength(50);

                entity.Property(e => e.IsNew)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.OldTriggerGroup).HasMaxLength(50);

                entity.Property(e => e.OldTriggerName).HasMaxLength(100);

                entity.Property(e => e.StartTimeUtc).HasMaxLength(50);

                entity.Property(e => e.TimeFormat).HasMaxLength(25);

                entity.Property(e => e.TriggerGroup)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'DEFAULT')");

                entity.Property(e => e.TriggerKey)
                    .IsRequired()
                    .HasMaxLength(160);

                entity.Property(e => e.TriggerName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Trigger)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trigger_Job");
            });

            modelBuilder.Entity<TriggerType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Expression).HasMaxLength(50);

                entity.Property(e => e.Friday)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Monday)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.RepeatForever)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Saturday)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Sunday)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Thursday)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TimeZone).HasMaxLength(200);

                entity.Property(e => e.Tuesday)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Wednessday)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });
        }
    }
}
