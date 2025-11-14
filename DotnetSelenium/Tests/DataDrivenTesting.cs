using DotnetSelenium.Pages;
using System.Text.Json;
using FluentAssertions;
using DotnetSelenium.Models;
using DotnetSelenium.Driver;

namespace DotnetSelenium.Tests.Tests
{
    public class DataDrivenTesting : WebDriverFixture
    {

        private IWebDriver _driver = null!;


        [SetUp]
        public void SetUp()
        {
            InitializeDriver(DriverType.Chrome);
            _driver = Driver!;
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.Navigate().GoToUrl("http://eaapp.somee.com");
            _driver.Manage().Window.Maximize();
        }

        [Test]
        [Category("ddt")]
        [TestCaseSource(nameof(Login))]
        public void TestWithPOM(LoginModel loginModel)
        {
            // POM initalization
            //Arrange
            LoginPage loginPage = new LoginPage(_driver);

            //Act
            loginPage.ClickLogin();
            loginPage.Login(loginModel.UserName, loginModel.Password);

            //Assert
            var getLoggedIn = loginPage.IsLoggedIn();
            Assert.IsTrue(getLoggedIn.employeeDetails && getLoggedIn.manageUsers);

        }


        [Test]
        [Category("ddt")]
        [TestCaseSource(nameof(LoginJsonDataSource))]
        public void TestWithPOMUsingFluentAssertion(LoginModel loginModel)
        {
            // POM initalization
            //Arrange
            LoginPage loginPage = new LoginPage(_driver);

            //Act
            loginPage.ClickLogin();
            loginPage.Login(loginModel.UserName, loginModel.Password);

            //Assert
            var getLoggedIn = loginPage.IsLoggedIn();
            getLoggedIn.employeeDetails.Should().BeTrue();
            getLoggedIn.manageUsers.Should().BeTrue();
        }


        [Test]
        [Category("ddt")]
        public void TestWithPOMWithJsonData()
        {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "login.json");
            var jsonString = File.ReadAllText(jsonFilePath);

            var loginModel = JsonSerializer.Deserialize<LoginModel>(jsonString);

            // POM initalization
            LoginPage loginPage = new LoginPage(_driver);

            //Act
            loginPage.ClickLogin();

            loginPage.Login(loginModel.UserName, loginModel.Password);
        }

        public static IEnumerable<LoginModel> Login()
        {
            yield return new LoginModel()
            {
                UserName = "admin",
                Password = "password"
            };
        }


        public static IEnumerable<LoginModel> LoginJsonDataSource()
        {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Data", "login.json");
            var jsonString = File.ReadAllText(jsonFilePath);

            var loginModel = JsonSerializer.Deserialize<List<LoginModel>>(jsonString);

            foreach (var loginData in loginModel)
            {
                yield return loginData;
            }
        }


        private void ReadJsonFile()
        {

            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "login.json");
            var jsonString = File.ReadAllText(jsonFilePath);

            var loginModel = JsonSerializer.Deserialize<LoginModel>(jsonString);

            Console.WriteLine($"UserName: {loginModel.UserName} Password: {loginModel.Password}");
        }



        [TearDown]
        public void TearDown()
        {
            DisposeDriver();
        }
    }
}
