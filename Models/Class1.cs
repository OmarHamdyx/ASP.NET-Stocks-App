namespace Models
{
    public class StockModel
    {
        public float CurrentPrice { get; set; }
        public float HighPriceOfDay { get; set; }
        public float LowPriceOfDay { get; set; }
        public long PreviousClosePrice { get; set; }
        public long TimeStamps { get; set; }
    }

}
