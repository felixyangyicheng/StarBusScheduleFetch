using Microsoft.AspNetCore.SignalR;
using StarBusScheduleFetch.Models.Parkings;

namespace StarBusScheduleFetch.Hubs
{
	public class ParkingHub : Hub
	{
		public ParkingHub()
		{
				
		}
		[HubMethodName("SendParkingOpenDataResult")]
		public async Task SendParkingOpenDataResultAsync(ParkingOpenDataResult parkingOpenDataResult)
		{
			if (Clients!=null)
			{

				await Clients.Group("parking").SendAsync("ReceiveParkingOpenDataResult", parkingOpenDataResult);

			}
		}
		/// <summary>
		/// SignalR connexion
		/// </summary>
		/// <returns></returns>
		public override async Task OnConnectedAsync()
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, "parking");
			await base.OnConnectedAsync();
		}
		/// <summary>
		/// SignalR interrupt connexion
		/// </summary>
		/// <param name="exception"></param>
		/// <returns></returns>
		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, "parking");

			await base.OnDisconnectedAsync(exception);
		}
	}
}
