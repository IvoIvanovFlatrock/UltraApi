using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Xml.Serialization;
using UltraPlay.Business;
using UltraPlay.Core.Models;
using UltraPlay.Core.Models.Config;
using UltraPlay.Persistence;
using UltraPlay.Business.RecurringTask;

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
			//services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
			var infrConfig = new BusinessIoCConfig(
				services, Configuration);
			services.AddDbContext<UltraDbContext>(options =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("UltraPlayDB"));
			}, ServiceLifetime.Transient);

			services.AddTransient<DbContext, UltraDbContext>();
			//var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			//builder.WebHost.UseStartup<StartUp>();
			//builder.Services.AddControllers();
			//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			//builder.Services.AddEndpointsApiExplorer();
			//builder.Services.AddSwaggerGen();

			//var app = builder.Build();

			//// Configure the HTTP request pipeline.
			//if (app.Environment.IsDevelopment())
			//{
			//	app.UseSwagger();
			//	app.UseSwaggerUI();
			//}

			//app.UseHttpsRedirection();

			//app.UseAuthorization();

			//app.MapControllers();

			//app.Run();
		}

		public void Configure(IApplicationBuilder app,
			IWebHostEnvironment env,
			IServiceProvider services)
		{
			// Configure the HTTP request pipeline.
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
