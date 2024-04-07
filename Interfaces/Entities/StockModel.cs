namespace Domain.Entities
{
	public class StockModel
	{
		public float C { get; set; } // CurrentPrice
		public float D { get; set; } // Difference
		public float Dp { get; set; }  // DifferencePercentage
		public float H { get; set; } // HighPriceOfDay
		public float L { get; set; } // LowPriceOfDay
		public float O { get; set; } // OpenPrice
		public float Pc { get; set; } // PreviousClosePrice
        public long T { get; set; } // TimeStamps
    }

}
