using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using DotnetSelenium.Driver;
using DotnetSelenium.Pages;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace DotnetSelenium.Tests.Tests;


/*
 * This is the test to demonstrate NUnit Tests
 */
[TestFixture("admin", "password", DriverType.Chrome)]
public class NUnitTestsDemo
{

    private IWebDriver _driver;
    private readonly string userName;
    private readonly string password;
    private readonly DriverType driverType;
    private ExtentReports _extentReports;
    private ExtentTest _extentTest;
    private ExtentTest _testNode;

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
        _driver = GetDriverType(driverType);
        _testNode = _extentTest.CreateNode("Setup and Tear Down").Pass("Browser Launched");
        _driver.Navigate().GoToUrl("http://eaapp.somee.com");
        _driver.Manage().Window.Maximize();
    }

    private IWebDriver GetDriverType(DriverType driverType)
    {
        return driverType switch
        {
            DriverType.Chrome => new ChromeDriver(),
            DriverType.Firefox => new FirefoxDriver(),
            DriverType.Edge => new EdgeDriver(),
            _ => _driver
        };
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
        _driver.Quit();
        _testNode.Pass("Browser Quit");
        _extentReports.Flush();
    }
}
