using ExtractInfoAmazon.Interface;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ExtractInfoAmazon.Service
{
    public class Images(HttpClient client) : IImages
    {
        
        string Hash(string input)
        {
            using var sha1 = SHA1.Create();
            return Convert.ToHexString(sha1.ComputeHash(Encoding.UTF8.GetBytes(input)));
        }
        public IEnumerable<ImagesObj> GetImages(IEnumerable<string> en)
        {
            var ls = new List<ImagesObj>();
            foreach (var item in en)
            {
                var file = client.GetAsync(item).GetAwaiter().GetResult();
                //client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/121.0.0.0 Safari/537.36");
               
                //  if (!file.IsSuccessStatusCode)
                {
                    //    string html = file.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    //    throw new Exception(html);
                }
                var memory = new MemoryStream();
                var res = file.Content.ReadAsStream();
                res.Position = 0;
                res.CopyTo(memory);
                var obj = new ImagesObj(GetNameImage(item), memory);
                ls.Add(obj);
            }
            return ls;
        }
        //https://images-na.ssl-images-amazon.com/images/I/71H9efHJytL.__AC_SX300_SY300_QL70_ML2_.jpg
        private string GetNameImage(string uri)
        {
            string file = Regex.Match(uri, "[^\\/]+$").Value?.Trim(' ');
            string ext = Path.GetExtension(file);
            return Hash(uri) +ext;
        }
    }
}
