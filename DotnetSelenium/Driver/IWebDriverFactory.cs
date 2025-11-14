namespace DotnetSelenium.Driver;

/// <summary>
/// Factory interface for creating WebDriver instances
/// </summary>
public interface IWebDriverFactory
{
    /// <summary>
    /// Creates a WebDriver instance based on the specified driver type
    /// </summary>
    /// <param name="driverType">The type of browser driver to create</param>
    /// <returns>An instance of IWebDriver</returns>
    IWebDriver CreateDriver(DriverType driverType);
}
