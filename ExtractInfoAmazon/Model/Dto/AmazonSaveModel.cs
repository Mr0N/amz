namespace ExtractInfoAmazon.Model.Dto
{
    public record AmazonSaveModel
    {
        public string Asin { set; get; }
        public string Name { get; set; }
        public string Characteristics { get; set; }
        public string Brand { get; set; }
        public decimal? Price { get; set; }
        public double? Rating { get; set; }
        public string Seller { get; set; }
        public string SellerUrl { get; set; }
        public string ReviewCount { get; set; }
        public IEnumerable<string> Photo { set; get; }

    }
}
