using DotnetSelenium.Driver;
using DotnetSelenium.Pages;
using OpenQA.Selenium.Remote;
namespace DotnetSelenium.Tests;

[TestFixture("admin", "password", DriverType.Edge)]
public class SeleniumGridTests : WebDriverFixture
{

    private IWebDriver _driver = null!;
    private readonly string userName;
    private readonly string password;
    private readonly DriverType driverType;

    public SeleniumGridTests(string userName, string password, DriverType driverType)
    {
        this.userName = userName;
        this.password = password;
        this.driverType = driverType;
    }

    [SetUp]
    public void SetUp()
    {
        // Note: SeleniumGrid tests still use RemoteWebDriver directly
        // This could be extended to use DI factory pattern for RemoteWebDriver as well
        _driver = new RemoteWebDriver(new Uri("http://localhost:4444"), new ChromeOptions());
        _driver.Navigate().GoToUrl("http://eaapp.somee.com");
        _driver.Manage().Window.Maximize();
    }

    [Test]
    public void TestWithPOM()
    {
        // POM initalization
        LoginPage loginPage = new LoginPage(_driver);

        loginPage.ClickLogin();

        loginPage.Login(userName, password);
    }

    [Test]
    [TestCase("chrome", "30")]
    public void TestBrowserVersion(string browser, string version)
    {

        Console.WriteLine($"The browser is {browser} with version {version}");
    }

    [TearDown]
    public void TearDown()
    {
        _driver.Quit();
    }
}
