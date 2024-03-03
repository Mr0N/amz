using ExtractInfoAmazon.Interface;

namespace ExtractInfoAmazon.Service.Background
{
    public class MainBackgroundService(IServiceScopeFactory scope) : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var obj = scope.CreateScope();
            var provider = obj.ServiceProvider;
            provider.GetService<IMainExecute>().Execute();
            return Task.CompletedTask;
            //            throw new NotImplementedException();
        }

    }
}
