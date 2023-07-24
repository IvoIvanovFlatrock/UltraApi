using Microsoft.AspNetCore.Mvc;
using UltraPlay.Core.Interfaces;

namespace UltraApi.Controllers
{
	[Route("api/[controller]")]
	public class ESportMatchesController : Controller
	{
		private IMatchService matchService;

		public ESportMatchesController(IMatchService matchService)
		{
			this.matchService = matchService;
		}

		[HttpGet()]
		public async Task<IActionResult> Get()
		{
			var result = await matchService.GetAllActiveAsync();

			var response = result.Select(m => new
			{
				Name = m.Name,
				StartDate = m.StartDate,
				ActiveBets = m.Bets.Select(b => new
				{
					MatchId = b.MatchId,
					Name = b.Name,
					IsLive = b.IsLive,
					Odds = b.Odds.Select(o => new
					{
						BetId = o.BetId,
						Name = o.Name,
						Value = o.Value
					})
				})
			}).ToList();

			return Ok(response);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var result = await matchService.GetActiveByIdAsync(id);

			var response =  new
			{
				Name = result.Name,
				StartDate = result.StartDate,
				ActiveBets = result.Bets.Select(b => new
				{
					MatchId = b.MatchId,
					Name = b.Name,
					IsLive = b.IsLive,
					Odds = b.Odds.Select(o => new
					{
						BetId = o.BetId,
						Name = o.Name,
						Value = o.Value
					})
				})
			};

			return Ok(response);
		}
	}
}
