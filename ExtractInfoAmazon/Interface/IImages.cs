using System.Collections;

namespace ExtractInfoAmazon.Interface
{
    public record ImagesObj(string name,MemoryStream file);
    public interface IImages
    {
        public IEnumerable<ImagesObj> GetImages(IEnumerable<string> en);
    }
}
