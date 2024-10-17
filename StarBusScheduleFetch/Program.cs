
using Quartz;
using StarBusScheduleFetch.Hubs;
using StarBusScheduleFetch.Timers;

namespace StarBusScheduleFetch
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
			builder.Services.AddSingleton<ParkingHub>();
			// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
			builder.Services.AddOpenApi();
			builder.Services.AddQuartz(q =>
			{
				// Just use the name of your job that you created in the Jobs folder.
				var jobKey = new JobKey("SendEmailJob");
				q.AddJob<ParkingJob>(opts => opts.WithIdentity(jobKey));

				q.AddTrigger(opts => opts
					.ForJob(jobKey)
					.WithIdentity("SendEmailJob-trigger")
					//This Cron interval can be described as "run every minute" (when second is zero)
					.WithCronSchedule("0 */1 * * * ?")
				);
			});
			builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
			builder.Services.AddCors(options => {
				options.AddPolicy("AllowAll",
					b =>
					{
						b.SetIsOriginAllowed(origin => true);
						b.AllowAnyMethod();
						b.AllowAnyHeader();
					});
			});
			builder.Services.AddSignalR();
			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

			app.UseRouting();

			app.UseCors("AllowAll");

			app.UseAuthorization();
			app.MapHub<ParkingHub>("/parking-hub").RequireCors("AllowAll");
			app.MapControllers();

            app.Run();
        }
    }
}
