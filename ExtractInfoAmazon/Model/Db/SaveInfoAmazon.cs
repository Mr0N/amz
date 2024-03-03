namespace ExtractInfoAmazon.Model.Db
{
    public class SaveInfoAmazon
    {
        public int Id { set; get; }
        public string Asin { set; get; }
        public string Name { get; set; }
        public string? Characteristics { get; set; }
        public string? Brand { get; set; }
        public decimal? Price { get; set; }
        public double? Rating { get; set; }
        public string? Seller { get; set; }
        public string? SellerUrl { get; set; }
        public string? ReviewCount { get; set; }
        public ICollection<FilesInfoAmazon> Files { set; get; }
    }
}
