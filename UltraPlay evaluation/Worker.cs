using AutoMapper;
using System.Xml.Linq;
using System.Xml.Serialization;
using UltraPlay_evaluation;
using UltraPlay_evaluation.Data;
using UltraPlay_evaluation.Data.Entities;
using UltraPlay_evaluation.QueueService;
using UltraPlay_evaluation.Utils;

namespace WorkerService1
{
    public sealed class Worker : IHostedService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMapper _mapper;
        private readonly UltraPlay_EvalContext _context;
        private Timer? _timer;
        private readonly IQueueService _queueService;


        public Worker(ILogger<Worker> logger, IQueueService queueService)
        {
            var mapperConfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new DataProfile());
            });
            _logger = logger;
            _mapper = mapperConfig.CreateMapper();
            _context = new UltraPlay_EvalContext();
            _queueService = queueService;
        }

        public void FetchData(object? state)
        {
            try
            {
                string address = "https://sports.ultraplay.net/sportsxml?clientKey=9C5E796D-4D54-42FD-A535-D7E77906541A&sportId=2357&days=7";
                HttpClient client = new()
                {
                    BaseAddress = new Uri(address)
                };
                var result = client.GetAsync(address).Result.Content.ReadAsStringAsync().Result;
                DataFetchUnitOfWork unitOfWork = new(_context, _mapper)
                {
                    XDocument = XDocument.Parse(result),
                    QueueService = _queueService
                };

                unitOfWork
                    .Serve<Sport>()
                    .Serve<Event>()
                    .Serve<Match>()
                    .Serve<Bet>()
                    .Serve<Odd>()
                    .UpdateDatabase();
            }
            catch (Exception e)
            {
                do
                {
                    _logger.LogError(e.Message);
                    e = e.InnerException;
                } while (e != null);
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(FetchData, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(60));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}