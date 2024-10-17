namespace StarBusScheduleFetch.Models.Parkings
{
	public class ParkingModel
	{
		public string Key { get; set; } = "";
		public string Status { get; set; } = "";
		public int Max { get; set; }
		public int Free { get; set; }
		public string Id { get; set; } = "";
		public Geo Geo { get; set; } = new();
		public string Tarif_15 { get; set; } = "";
		public string Tarif_30 { get; set; } = "";
		public string Tarif_1h { get; set; } = "";
		public string Tarif_1h30 { get; set; } = "";
		public string Tarif_2h { get; set; } = "";
		public string Tarif_3h { get; set; } = "";
		public string Tarif_4h { get; set; } = "";
		public string Orgahoraires { get; set; } = "";
		public double? Tarif_nuit_15 { get; set; }
		public double? Tarif_nuit_30 { get; set; }
		public double? Tarif_nuit_1h { get; set; }
		public double? Tarif_nuit_1h30 { get; set; }
		public double? Tarif_nuit_2h { get; set; }
		public double? Tarif_nuit_3h { get; set; }
		public double? Tarif_nuit_4h { get; set; }
	}
	public class Geo
	{
		public double? Lon { get; set; }
		public double? Lat { get; set; }
	}

}
