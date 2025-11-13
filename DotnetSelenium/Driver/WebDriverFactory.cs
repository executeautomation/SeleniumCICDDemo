using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace DotnetSelenium.Driver;

/// <summary>
/// Factory implementation for creating WebDriver instances with support for multiple browser types
/// </summary>
public class WebDriverFactory : IWebDriverFactory
{
    /// <summary>
    /// Creates a WebDriver instance based on the specified driver type
    /// </summary>
    /// <param name="driverType">The type of browser driver to create</param>
    /// <returns>An instance of IWebDriver configured for the specified browser</returns>
    public IWebDriver CreateDriver(DriverType driverType)
    {
        ChromeOptions chromeOptions = new ChromeOptions();
        chromeOptions.AddArgument("--headless=new");

        return driverType switch
        {
            DriverType.Chrome => new ChromeDriver(chromeOptions),
            DriverType.Firefox => new FirefoxDriver(),
            DriverType.Edge => new EdgeDriver(),
            _ => throw new ArgumentException($"Unsupported driver type: {driverType}")
        };
    }
}
