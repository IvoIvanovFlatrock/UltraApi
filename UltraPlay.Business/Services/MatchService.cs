using Microsoft.EntityFrameworkCore;
using UltraPlay.Core.Entities;
using UltraPlay.Core.Interfaces;
using UltraPlay.Persistence;

namespace UltraPlay.Business.Services
{
	public class MatchService : IMatchService
	{
		private UltraDbContext context;
		private List<string> prevBets;

		public MatchService(UltraDbContext context)
		{
			this.context = context;
			this.prevBets = new List<string>()
			{
				"Match Winner",
				"Map Advantage",
				"Total Maps Played"
			};
		}

		public async Task<List<MatchEntity>> GetAllActiveAsync()
		{
			var now = DateTime.UtcNow;
			var next = now.AddDays(1);
			var matches = context.Set<MatchEntity>()
				.Where(m => (m.StartDate >= now && m.StartDate <= next))
				.ToList();

			matches.ForEach(m => m.Bets.Where(b => prevBets.Contains(b.Name)));

			//Filter out special bets
			foreach (var match in matches)
			{
				foreach (var bet in match.Bets)
				{
					if (bet.Odds.Any(o => o.SpecialBetValue != 0))
					{
						var odds = bet.Odds.GroupBy(o => o.SpecialBetValue);
						var fg = odds.FirstOrDefault().FirstOrDefault().SpecialBetValue;
						bet.Odds = bet.Odds.Where(o => o.SpecialBetValue == fg).ToList();
					}
				}
			}

			return matches;
		}

		public async Task<MatchEntity> GetActiveByIdAsync(int id)
		{
			var match = await context.Set<MatchEntity>()
				.FirstOrDefaultAsync(m => m.Id == id);
			if (match == null)
			{
				return match;
			}

			match.Bets.Where(b => prevBets.Contains(b.Name));

			//Filter out special bets
			foreach (var bet in match.Bets)
			{
				if (bet.Odds.Any(o => o.SpecialBetValue != 0))
				{
					var odds = bet.Odds.GroupBy(o => o.SpecialBetValue);
					var fg = odds.FirstOrDefault().FirstOrDefault().SpecialBetValue;
					bet.Odds = bet.Odds.Where(o => o.SpecialBetValue == fg).ToList();
				}
			}

			return match;
		}

		//Can return history data for every entity in the last minute also highly configurable so can be fed to client.
		public async Task<object> GetSportDataChangesAsync()
		{
			var now = DateTime.Now;
			var matchChanges = this.context.Matches.TemporalFromTo(now.AddMinutes(-1), now);
			var betsChanges = this.context.Bets.TemporalFromTo(now.AddMinutes(-1), now);
			var oddsChanges = this.context.Odds.TemporalFromTo(now.AddMinutes(-1), now);

			var result = new
			{
				matches = matchChanges.Last(),
				bets = betsChanges.Last(),
				odds = oddsChanges.Last()
			};

			return result;
		}
	}
}
