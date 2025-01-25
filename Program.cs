namespace WorkerServiceDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //..
            // Create a logger
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();  // Log to console
            });
            var logger = loggerFactory.CreateLogger<Program>();

            // Log that the application is starting
            logger.LogInformation("Application is starting...");

            //..

            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    // Log when services are being registered
                    logger.LogInformation("Registering hosted services...");

                    // Register hosted services
                    services.AddHostedService<Worker>();
                    services.AddHostedService<Worker1>();

                    // Log after services are registered
                    logger.LogInformation("Hosted services registered.");
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();  // Log to console
                })
                .Build();

            // Log when the host is about to run
            logger.LogInformation("Host is starting...");

            host.Run();

            // Log after the host stops
            logger.LogInformation("Host has stopped.");
        }
    }
}