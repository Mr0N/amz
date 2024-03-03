
using ExtractInfoFromPage.Interface;
using OpenQA.Selenium.DevTools.V119.WebAuthn;

namespace ExtractInfoFromPage.Service
{
    public class MainBackgroundService(IServiceScopeFactory factory) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                using var res = factory.CreateScope();
                var getLink = res.ServiceProvider.GetService<IGetLinkFromUri>();
                Console.WriteLine("read....");
                Console.ReadKey();
                using var writer = new StreamWriter("link.txt");
                var ls = File.ReadAllLines("linkread.txt");
                foreach (var item in ls)
                {
                    var obj = getLink.GetLinks(item, 0, 10_000_000);
                    foreach (var element in obj)
                    {
                        writer.WriteLine(element);
                    }
                }
            }catch(Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.ToString());
            }

            Console.ReadKey();
        }

    }
}
