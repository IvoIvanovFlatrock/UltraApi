using Microsoft.AspNetCore.Hosting;
using UltraApi;

internal class Program
{
	private static void Main(string[] args)
	{
		IHost host = BuildWebHost(args);
		host.Run();
	}

	public static IHost BuildWebHost(string[] args)
	{
		return Host
		.CreateDefaultBuilder(args)
		.ConfigureHostOptions(options =>
		{
			options.ShutdownTimeout = new TimeSpan(0, 0, 10);
		})
		.ConfigureAppConfiguration((builderContext, config) =>
		{
			var env = builderContext.HostingEnvironment;
			config.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile("appsettings.local.json", optional: true)
				.AddJsonFile("config/appsettings-ac.json", optional: true, reloadOnChange: true)
				.AddJsonFile("config/appsettings-tf.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables()
				.AddCommandLine(args);
		})
		.ConfigureWebHostDefaults(webBuilder =>
		{
			webBuilder.UseStartup<StartUp>();
		}).Build();
	}
}