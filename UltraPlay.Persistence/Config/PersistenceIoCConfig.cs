using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace UltraPlay.Persistence.Config
{
	public class PersistenceIoCConfig
	{
		private readonly string connectionString;

		public PersistenceIoCConfig(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public void AddServices(IServiceCollection services)
		{
			services.AddDbContext<UltraDbContext>(options =>
			{
				options.UseSqlServer(connectionString);
			}, ServiceLifetime.Singleton);

			services.AddSingleton<DbContext, UltraDbContext>();
		}
	}
}
