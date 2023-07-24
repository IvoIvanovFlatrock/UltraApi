using Microsoft.EntityFrameworkCore;
using UltraPlay.Business;
using UltraPlay.Business.RecurringTask;
using UltraPlay.Core.Models.Config;
using UltraPlay.Persistence;

namespace UltraApi
{
	public class StartUp
	{
		public StartUp(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			var endpoints = Configuration.GetSection("Endpoints");
			services.Configure<Endpoints>(endpoints);
			services.AddControllers();
			services.AddSwaggerGen();
			var infrConfig = new BusinessIoCConfig(
				services, Configuration);
			services.AddDbContext<UltraDbContext>(options =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("UltraPlayDB"));
			}, ServiceLifetime.Transient);

			services.AddTransient<DbContext, UltraDbContext>();
		}

		public void Configure(IApplicationBuilder app,
			IWebHostEnvironment env,
			IServiceProvider services)
		{
			if (env.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			var context = app.ApplicationServices.GetService<UltraDbContext>();
			var cts = new CancellationTokenSource();
			RT(async () => await GetSportDataXml.GetUltraPlayData(context), 60, cts.Token);
		}

		static void RT(Action action, int seconds, CancellationToken token)
		{
			if (action == null)
				return;
			Task.Run(async () =>
			{
				while (!token.IsCancellationRequested)
				{
					action();
					await Task.Delay(TimeSpan.FromSeconds(seconds), token);
				}
			}, token);
		}
	}
}
