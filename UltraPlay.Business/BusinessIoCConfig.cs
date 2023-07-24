using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UltraPlay.Business.Services;
using UltraPlay.Core.Interfaces;
using UltraPlay.Persistence.Config;

namespace UltraPlay.Business
{
	public class BusinessIoCConfig
	{
		public BusinessIoCConfig(IServiceCollection services,
			IConfiguration configuration)
		{
			this.AddServices(services, configuration);
		}

		public void AddServices(IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddScoped<IMatchService, MatchService>();
			if (!string.IsNullOrEmpty(configuration.GetConnectionString("UltraPlayDB")))
			{
				var persConfig = new PersistenceIoCConfig(
					configuration.GetConnectionString("UltraPlayDB"));
				persConfig.AddServices(services);
			}
		}
	}
}