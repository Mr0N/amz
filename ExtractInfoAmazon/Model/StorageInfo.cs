using ExtractInfoAmazon.Model.Dto;

namespace ExtractInfoAmazon.Model
{
    public record FilesInfo(string nameFile, Stream stream) : IDisposable
    {
        public void Dispose()
        {
            stream?.Dispose();
        }
    };
    public record StorageInfo : AmazonSaveModel,IDisposable
    {
        public List<FilesInfo> Files { set; get; }

        public void Dispose()
        {
            foreach (var item in this.Files ?? [])
            {
                item?.Dispose();
            }
        }
    }
}
