using DotnetSelenium.Pages;
using DotnetSelenium.Driver;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetSelenium.Tests
{
    public class UnitTest1 : WebDriverFixture
    {
        private IWebDriverFactory _driverFactory = null!;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
            _driverFactory = ServiceProvider.GetRequiredService<IWebDriverFactory>();
        }

        [Test]
        public void Test1()
        {
            // Sudo code for setting up Selenium
            // 1. Create a new instance of Selenium Web Driver using dependency injection
            IWebDriver driver = _driverFactory.CreateDriver(DriverType.Chrome);
            // 2. Navigate to the URL
            driver.Navigate().GoToUrl("https://www.google.com/");
            // 2a. Maximize the browser window
            driver.Manage().Window.Maximize();
            // 3. Find the element
            IWebElement webElement = driver.FindElement(By.Name("q"));
            // 4. Type in the element
            webElement.SendKeys("Selenium");
            // 5. Click on the element
            webElement.SendKeys(Keys.Return);

            webElement.Click();
            driver.Quit();
        }


        [Test]
        public void EAWebSiteTest()
        {
            // 1. Create a new instance of Selenium Web Driver using dependency injection
            var driver = _driverFactory.CreateDriver(DriverType.Chrome);
            // 2. Navigate to the URL
            driver.Navigate().GoToUrl("http://eaapp.somee.com/");
            // 3. Find the Login link
            var loginLink = driver.FindElement(By.Id("loginLink"));
            // 4. Click the Login link
            loginLink.Click();

            //Explicit Wait
            WebDriverWait driverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            {
                PollingInterval = TimeSpan.FromMilliseconds(200),
                Message = "Textbox UserName does not appear during that timeframe"
            };

            driverWait.IgnoreExceptionTypes(typeof(NoSuchElementException));


            var txtUserName = driverWait.Until(d =>
            {
                var element = driver.FindElement(By.Name("UserNames"));
                return (element != null && element.Displayed) ? element : null;
            });

            // 6. Typing on the textUserName
            txtUserName.SendKeys("admin");
            // 7. Find the Password text box
            var txtPassword = driver.FindElement(By.Id("Password"));
            // 8. Typing on the textUserName
            txtPassword.SendKeys("password");
            // 9. Identify the Login Button using Class Name
            //IWebElement btnLogin = driver.FindElement(By.ClassName("btn"));
            // 9. Identify the Login Button using CssSelector
            var btnLogin = driver.FindElement(By.CssSelector(".btn"));
            // 10. Click login button
            btnLogin.Submit();
            driver.Quit();
        }

        [Test]
        public void TestWithPOM()
        {
            // 1. Create a new instance of Selenium Web Driver using dependency injection
            var driver = _driverFactory.CreateDriver(DriverType.Chrome);
            // 2. Navigate to the URL
            driver.Navigate().GoToUrl("http://eaapp.somee.com/");
            // POM initalization
            LoginPage loginPage = new LoginPage(driver);

            loginPage.ClickLogin();

            loginPage.Login("admin", "password");
            driver.Quit();
        }

        //[Test]
        //public void EAWebSiteTestReduceSizeCode()
        //{
        //    // 1. Create a new instance of Selenium Web Driver
        //    IWebDriver driver = new ChromeDriver();
        //    // 2. Navigate to the URL
        //    driver.Navigate().GoToUrl("http://eaapp.somee.com/");
        //    // 3. Find and Click the Login link
        //    SeleniumCustomMethods.Click(driver, By.Id("loginLink"));
        //    // 4. Find the UserName text box
        //    SeleniumCustomMethods.EnterText(driver, By.Name("UserName"), "admin");
        //    // 5. Find the Password text box
        //    SeleniumCustomMethods.EnterText(driver, By.Name("Password"), "password");
        //    // 6. Click Submit
        //    driver.FindElement(By.CssSelector(".btn")).Submit();
        //}

        //[Test]
        //public void WorkingWithAdvancedControls()
        //{
        //    // 1. Create a new instance of Selenium Web Driver
        //    IWebDriver driver = new ChromeDriver();
        //    // 2. Navigate to the URL
        //    driver.Navigate().GoToUrl("file:///C:/testpage.html");

        //    SeleniumCustomMethods.SelectDropDownByText(driver, By.Id("dropdown"), "Option 2");

        //    SeleniumCustomMethods.MultiSelectElements(driver, By.Id("multiselect"), ["multi1", "multi2"]);

        //    var getSelectedOptions = SeleniumCustomMethods.GetAllSelectedLists(driver, By.Id("multiselect"));

        //    getSelectedOptions.ForEach(Console.WriteLine);

        //}


        [Test]
        public void Test4()
        {

        }

        [Test]
        public void Test5()
        {

        }


    }
}