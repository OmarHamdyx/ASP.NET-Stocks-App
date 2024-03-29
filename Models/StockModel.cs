namespace Models
{
	public class StockModel
	{
		public float c { get; set; } // CurrentPrice
		public float d { get; set; } // Difference
		public float dp { get; set; }  // DifferencePercentage
		public float h { get; set; } // HighPriceOfDay
		public float l { get; set; } // LowPriceOfDay
		public float o { get; set; } // OpenPrice
		public float pc { get; set; } // PreviousClosePrice
		public long t { get; set; } // TimeStamps
	}

}
