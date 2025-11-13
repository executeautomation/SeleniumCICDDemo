using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using DotnetSelenium.Driver;
using DotnetSelenium.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetSelenium.Tests.Tests;


/*
 * This is the test to demonstrate NUnit Tests
 */
[TestFixture("admin", "password", DriverType.Chrome)]
public class NUnitTestsDemo : WebDriverFixture
{

    private IWebDriver _driver = null!;
    private readonly string userName;
    private readonly string password;
    private readonly DriverType driverType;
    private ExtentReports _extentReports = null!;
    private ExtentTest _extentTest = null!;
    private ExtentTest _testNode = null!;
    private IWebDriverFactory _driverFactory = null!;

    public NUnitTestsDemo(string userName, string password, DriverType driverType)
    {
        this.userName = userName;
        this.password = password;
        this.driverType = driverType;
    }

    [SetUp]
    public void SetUp()
    {
        SetupExtentReports();
        
        // Initialize driver using dependency injection
        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();
        _driverFactory = ServiceProvider.GetRequiredService<IWebDriverFactory>();
        _driver = _driverFactory.CreateDriver(driverType);
        
        _testNode = _extentTest.CreateNode("Setup and Tear Down").Pass("Browser Launched");
        _driver.Navigate().GoToUrl("http://eaapp.somee.com");
        _driver.Manage().Window.Maximize();
    }

    private void SetupExtentReports()
    {
        _extentReports = new ExtentReports();
        var spark = new ExtentSparkReporter("TestReport.html");
        _extentReports.AttachReporter(spark);
        _extentReports.AddSystemInfo("OS", "Windows 11");
        _extentReports.AddSystemInfo("Browser", driverType.ToString());
        _extentTest = _extentReports.CreateTest("Login Test with POM").Log(Status.Info, "Extent report initialized");
    }



    [Test]
    [Category("smoke")]
    public void TestWithPOM()
    {
        // POM initalization
        LoginPage loginPage = new LoginPage(_driver);

        loginPage.ClickLogin();
        _extentTest.Log(Status.Pass, "Click Login");

        loginPage.Login(userName, password);
        _extentTest.Log(Status.Pass, "UserName and Password entered with login happened");

        var getLoggedIn = loginPage.IsLoggedIn();
        Assert.IsTrue(getLoggedIn.employeeDetails && getLoggedIn.manageUsers);
        _extentTest.Log(Status.Pass, "Assertion successful");

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
        DisposeDriver();
        _testNode.Pass("Browser Quit");
        _extentReports.Flush();
    }
}
