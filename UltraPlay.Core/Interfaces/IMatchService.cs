using UltraPlay.Core.Entities;

namespace UltraPlay.Core.Interfaces
{
	public interface IMatchService
	{
		Task<List<MatchEntity>> GetAllActiveAsync();

		Task<MatchEntity> GetActiveByIdAsync(int id);

		Task<object> GetSportDataChangesAsync();
	}
}
