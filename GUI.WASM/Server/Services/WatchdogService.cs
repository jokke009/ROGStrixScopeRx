using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace GUI.WASM.Server.Services
{
    public class WatchdogService : BackgroundService
    {
        private readonly ILogger<WatchdogService> _logger;
        private readonly IServiceProvider _services;

        private volatile bool _ready = false;

        public WatchdogService(ILogger<WatchdogService> logger, IServiceProvider services, IHostApplicationLifetime lifetime)
        {
            _services = services;
            _logger = logger;
            lifetime.ApplicationStarted.Register(() => _ready = true);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // TODO: wait here until Kestrel is ready

            while (!_ready)
            {
                // App hasn't started yet, keep looping!
                await Task.Delay(1_000);
            }
            //-----------------------------------------------------------------------
            // Start services that need kestrel beyond here
            //-----------------------------------------------------------------------

            PrintStartupSuccess(_services);
            //IEnumerable<ILiveFeedService> liveFeeds = _services.GetRequiredService<IEnumerable<ILiveFeedService>>(); // now let the rest of the program start only after the kestrelis up and running.
            // make sure we get the CompositeLiveFeedService



            //var liveFeedComp = _services.GetRequiredService<ILiveFeedService>();
            //await StartILiveFeedService(liveFeedComp);

        }

        //private async Task StartILiveFeedServices(IEnumerable<ILiveFeedService> liveFeeds)
        //{
        //    foreach (var liveFeed in liveFeeds)
        //    {
        //        await liveFeed.StartCommunication();
        //    }
        //}

        //private async Task StartILiveFeedService(ILiveFeedService liveFeed)
        //{
        //    await liveFeed.StartCommunication();
        //}

        private void PrintStartupSuccess(IServiceProvider services)
        {

            var myservices = services.GetRequiredService<IServer>();
            var addresses = myservices.Features.Get<IServerAddressesFeature>().Addresses;

            _logger.LogInformation($"ALCMSCLOUD started successfully, kestrel ready and serving {addresses.Count()} endpoints!");

            //foreach (var address in addresses)
            //{
            //    Console.WriteLine($"Listening on: {address}");
            //}
        }
    }
}
