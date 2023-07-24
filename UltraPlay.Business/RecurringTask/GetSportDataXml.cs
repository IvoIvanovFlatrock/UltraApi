using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;
using UltraPlay.Core.Entities;
using UltraPlay.Core.Models;
using UltraPlay.Persistence;

namespace UltraPlay.Business.RecurringTask
{
	public static class GetSportDataXml
	{
		public static async Task GetUltraPlayData(UltraDbContext context)
		{
			var url = "https://sports.ultraplay.net/sportsxml?clientKey=9C5E796D-4D54-42FD-A535-D7E77906541A&sportId=2357&days=7";
			var HttpClient = new HttpClient();
			var response = await HttpClient.GetAsync(url);
			var data = new XmlSports();
			if (response.IsSuccessStatusCode)
			{
				var serializer = new XmlSerializer(typeof(XmlSports));
				var stream = await response.Content.ReadAsStringAsync();
				using (StringReader xmlReader = new StringReader(stream))
				{
					data = (XmlSports)serializer.Deserialize(xmlReader);
				}

				if (data != null)
				{
					await AddChangedDataHistoryAsync(context, data.Sport);
					await AddNewDataAsync(context, data.Sport);
				}
			}
		}

		private static async Task AddNewDataAsync(UltraDbContext context,
			SportEntity sport)
		{
			//(I) : Since there is no requirement for performance it is easier and cleaner to just delete and add,
			//instead of checking every single entity and decide where to delete, update or add.
			var sportEntity = await context.Set<SportEntity>()
				.FirstOrDefaultAsync();
			if (sportEntity != null)
			{
				context.Set<SportEntity>().Remove(sportEntity);
				await context.SaveChangesAsync();
			}

			await context.Set<SportEntity>().AddAsync(sport);
			await context.SaveChangesAsync();
		}

		private static async Task AddChangedDataHistoryAsync(UltraDbContext context,
			SportEntity sport)
		{
			var matches = context.Set<MatchEntity>().AsTracking();
			var matchesIncoming = sport.Events.SelectMany(e => e.Matches).ToList();
			foreach (var m in matches)
			{
				var matchInc = matchesIncoming.FirstOrDefault(x => x.Id == m.Id);
				if (matchInc != null && !m.Equals(matchInc))
				{
					context.Entry(m).State = EntityState.Modified;
				}
			}

			var bets = context.Set<BetEntity>().AsTracking();
			var betsInc = matchesIncoming.SelectMany(m => m.Bets).ToList();
			foreach (var b in bets)
			{
				var betInc = betsInc.FirstOrDefault(x => x.Id == b.Id);
				if (betInc != null && !b.Equals(betInc))
				{
					context.Entry(b).State = EntityState.Modified;
				}
			}

			var odds = context.Set<OddEntity>().AsTracking();
			var oddsInc = betsInc.SelectMany(b => b.Odds).ToList();
			foreach (var o in odds)
			{
				var oddInc = oddsInc.FirstOrDefault(x => x.Id == o.Id);
				if (oddInc != null && !o.Equals(oddInc))
				{
					context.Entry(o).State = EntityState.Modified;
				}
			}

			await context.SaveChangesAsync();

			//(I) : After triggering temporal changes release,
			//entities so we can use context to delete and add new data.
			context.ChangeTracker.Clear();
		}
	}
}

