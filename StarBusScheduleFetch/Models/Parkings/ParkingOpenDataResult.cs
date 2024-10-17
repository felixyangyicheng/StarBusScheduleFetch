namespace StarBusScheduleFetch.Models.Parkings
{
	public class ParkingOpenDataResult
	{
		public int Total_count { get; set; }
		public List<ParkingModel> Results { get; set; } = new(); 
	}
}
