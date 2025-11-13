# WebDriver Dependency Injection

This directory contains the dependency injection infrastructure for managing Selenium WebDriver instances across test classes.

## Components

### 1. IWebDriverFactory
An interface that defines the contract for creating WebDriver instances.

```csharp
public interface IWebDriverFactory
{
    IWebDriver CreateDriver(DriverType driverType);
}
```

### 2. WebDriverFactory
The concrete implementation of `IWebDriverFactory` that creates WebDriver instances based on the specified driver type (Chrome, Firefox, or Edge).

**Features:**
- Supports Chrome, Firefox, and Edge browsers
- Chrome runs in headless mode by default for CI/CD environments
- Throws meaningful exceptions for unsupported driver types

### 3. WebDriverFixture
A base class for test fixtures that provides:
- Dependency injection configuration
- Driver lifecycle management
- Service provider setup

**Usage in test classes:**
```csharp
public class MyTests : WebDriverFixture
{
    private IWebDriver _driver;
    
    [SetUp]
    public void SetUp()
    {
        InitializeDriver(DriverType.Chrome);
        _driver = Driver!;
    }
    
    [TearDown]
    public void TearDown()
    {
        DisposeDriver();
    }
}
```

### 4. DriverType
An enum that defines supported browser types:
- Chrome
- Firefox
- Edge

## Benefits of Dependency Injection

1. **Separation of Concerns**: Driver creation logic is separated from test logic
2. **Testability**: Easy to mock or replace driver factory for unit testing
3. **Maintainability**: Single place to update driver configuration
4. **Flexibility**: Easy to add new browser types or configurations
5. **Reusability**: Common driver management code shared across all test classes

## Usage Examples

### Example 1: Using WebDriverFixture Base Class
```csharp
public class DataDrivenTesting : WebDriverFixture
{
    private IWebDriver _driver;
    
    [SetUp]
    public void SetUp()
    {
        InitializeDriver(DriverType.Chrome);
        _driver = Driver!;
    }
    
    [TearDown]
    public void TearDown()
    {
        DisposeDriver();
    }
}
```

### Example 2: Using Factory Pattern Directly
```csharp
public class UnitTest1 : WebDriverFixture
{
    private IWebDriverFactory _driverFactory;
    
    [SetUp]
    public void Setup()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();
        _driverFactory = ServiceProvider.GetRequiredService<IWebDriverFactory>();
    }
    
    [Test]
    public void MyTest()
    {
        var driver = _driverFactory.CreateDriver(DriverType.Chrome);
        // ... test code ...
        driver.Quit();
    }
}
```

## Dependencies

- Microsoft.Extensions.DependencyInjection (10.0.0+)
- Selenium.WebDriver (4.15.0+)
- NUnit (3.13.3+)
