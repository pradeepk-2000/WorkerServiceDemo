namespace WorkerServiceDemo
{
    public class Worker1 : BackgroundService
    {
        private readonly ILogger<Worker1> _logger;
        private readonly string _folderPath = @"C:\Pradeep\BackgroundSchedulerDemoTestFiles\Worker1";

        public Worker1(ILogger<Worker1> logger)
        {
            _logger = logger;
            _logger.LogInformation("Worker1 instanmtiated at : {time}", DateTimeOffset.Now);    
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker1 started at: {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                LogFilesInDirectory();

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker1 is stopping at: {time}", DateTimeOffset.Now);
            await base.StopAsync(cancellationToken);
        }

        //Select Worker Service from the list of project templates.
        private void LogFilesInDirectory()
        {
            try
            {
                // Check if the directory exists
                if (Directory.Exists(_folderPath))
                {
                    var files = Directory.GetFiles(_folderPath);

                    // Log each file in the directory
                    foreach (var file in files)
                    {
                        _logger.LogInformation("File found: {fileName}", Path.GetFileName(file));
                    }

                    if (files.Length == 0)
                    {
                        _logger.LogInformation("No files found in the directory.");
                    }
                }
                else
                {
                    _logger.LogWarning("Directory does not exist: {folderPath}", _folderPath);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while accessing the directory.");
            }
        }
    }
}
