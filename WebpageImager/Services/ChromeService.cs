using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebpageImager.Services
{
    public class ChromeService : BackgroundService
    {
        ChromeDriver? driver = null;

        string executablePath;
        string windowSize;
        string url;

        bool stopped = false;

        public ChromeService(string executablePath, string windowSize, string url)
        {
            this.executablePath = executablePath;
            this.windowSize = windowSize;
            this.url = url;
        }

        public void Initialize()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("headless");
            options.AddArgument($"--window-size={windowSize}");

            driver = new ChromeDriver(Path.GetDirectoryName(executablePath), options);
            driver.Navigate().GoToUrl(url);
        }

        public byte[]? GetScreenshot()
        {
            if (stopped || driver == null)
            {
                return null;
            }

            lock (this)
            {
                var screenshot = (driver as ITakesScreenshot).GetScreenshot();
                var image = screenshot.AsByteArray;

                return image;
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                Initialize();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error initializing ChromeService: {ex.Message}");
                return;
            }

            Console.WriteLine("Initialization done");

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(1000, stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Cancellation requested");
                return;
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Stopping...");
            stopped = true;

            if (driver != null)
            {
                Console.WriteLine("Stopping driver");
                driver.Quit();
                driver = null;
                Console.WriteLine("Driver stopped");
            }
        }
    }
}
