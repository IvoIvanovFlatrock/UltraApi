using Microsoft.EntityFrameworkCore;
using UltraPlay.Core.Entities;

namespace UltraPlay.Persistence
{
	public class UltraDbContext : DbContext
	{
		public UltraDbContext(DbContextOptions<UltraDbContext> options)
			: base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<SportEntity>()
				.ToTable("Sports", SportTable => SportTable.IsTemporal());

			modelBuilder.Entity<EventEntity>()
				.ToTable("Events", EventTable => EventTable.IsTemporal());

			modelBuilder.Entity<MatchEntity>()
				.ToTable("Matches", MatchTable => MatchTable.IsTemporal());

			modelBuilder.Entity<BetEntity>()
				.ToTable("Bets", BetTable => BetTable.IsTemporal());

			modelBuilder.Entity<OddEntity>()
				.ToTable("Odds", OddsTable => OddsTable.IsTemporal());

			modelBuilder.Entity<SportEntity>()
				.HasMany(s => s.Events)
				.WithOne(e => e.Sport)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<EventEntity>()
				.HasMany(e => e.Matches)
				.WithOne(m => m.Event)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<MatchEntity>()
				.HasMany(m => m.Bets)
				.WithOne(b => b.Match)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<BetEntity>()
				.HasMany(b => b.Odds)
				.WithOne(o => o.Bet)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<SportEntity>()
				.Property(s => s.Id).ValueGeneratedNever();

			modelBuilder.Entity<EventEntity>()
				.Property(s => s.Id).ValueGeneratedNever();

			modelBuilder.Entity<MatchEntity>()
				.Property(s => s.Id).ValueGeneratedNever();

			modelBuilder.Entity<BetEntity>()
				.Property(s => s.Id).ValueGeneratedNever();

			modelBuilder.Entity<OddEntity>()
				.Property(s => s.Id).ValueGeneratedNever();
		}

		public DbSet<SportEntity> Sports { get; set; }

		public DbSet<EventEntity> Events { get; set; }

		public DbSet<MatchEntity> Matches { get; set; }

		public DbSet<BetEntity> Bets { get; set; }

		public DbSet<OddEntity> Odds { get; set; }
	}
}
