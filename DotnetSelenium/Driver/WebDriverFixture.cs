using Microsoft.Extensions.DependencyInjection;

namespace DotnetSelenium.Driver;

/// <summary>
/// Base fixture class for managing WebDriver lifecycle with dependency injection
/// </summary>
public class WebDriverFixture
{
    protected IServiceProvider? ServiceProvider { get; set; }
    protected IWebDriver? Driver { get; private set; }

    /// <summary>
    /// Initializes the driver with the specified driver type using dependency injection
    /// </summary>
    /// <param name="driverType">The type of browser driver to create</param>
    protected void InitializeDriver(DriverType driverType)
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();

        var factory = ServiceProvider.GetRequiredService<IWebDriverFactory>();
        Driver = factory.CreateDriver(driverType);
    }

    /// <summary>
    /// Configures dependency injection services
    /// </summary>
    /// <param name="services">The service collection to configure</param>
    protected virtual void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IWebDriverFactory, WebDriverFactory>();
    }

    /// <summary>
    /// Disposes the driver and service provider
    /// </summary>
    protected void DisposeDriver()
    {
        Driver?.Quit();
        Driver?.Dispose();
        
        if (ServiceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}
