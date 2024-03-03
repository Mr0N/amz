namespace ExtractInfoAmazon.Interface
{
    public interface IOptions
    {
        public bool CheckIsOptions();
        public IEnumerable<string> GetAsinOptions();
    }
}
