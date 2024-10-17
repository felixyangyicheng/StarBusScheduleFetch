using Microsoft.AspNetCore.SignalR;
using Quartz;
using StarBusScheduleFetch.Hubs;
using StarBusScheduleFetch.Models.Parkings;
using System.Net.Http.Headers;

namespace StarBusScheduleFetch.Timers
{
	public class ParkingJob : IJob
	{
		private readonly IConfiguration configuration;
		private  ParkingHub _parkingHub;
		public ParkingJob(
			IConfiguration configuration,
			ParkingHub parkingHub
			)
		{
	
			this.configuration = configuration;
			this._parkingHub = parkingHub;
		}
		public async Task Execute(IJobExecutionContext context)
		{
			HttpClient _client = new HttpClient();
			var getUrl = configuration["parking"];
			var response = await _client.GetFromJsonAsync<ParkingOpenDataResult>(
				$"{getUrl}"
				);
			//await Console.Out.WriteLineAsync($"count {response.Total_count}");
			foreach (var item in response.Results)
			{
				await Console.Out.WriteLineAsync($"{item.Key}: {item.Max} {item.Free}");
			}
		
			await _parkingHub.SendParkingOpenDataResultAsync(response);
			await Console.Out.WriteLineAsync($"Testing timer the time is now {DateTime.Now}, UTC is {DateTime.UtcNow}!");
		}

	}
}
