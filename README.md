# SeleniumCICDDemo
Selenium CI/CD integration demo for the course

## Features

### Dependency Injection for WebDriver Management
This project uses dependency injection to manage Selenium WebDriver instances across test classes. This provides better separation of concerns, improved testability, and easier maintenance.

See [DotnetSelenium/Driver/README.md](DotnetSelenium/Driver/README.md) for detailed documentation on the dependency injection pattern implementation.

**Key Components:**
- `IWebDriverFactory` - Interface for driver creation
- `WebDriverFactory` - Factory implementation supporting Chrome, Firefox, and Edge
- `WebDriverFixture` - Base class for test fixtures with DI support

## Running Tests

```bash
# Build the project
dotnet build

# Run all tests
dotnet test

# Run smoke tests only (used in CI/CD)
dotnet test --filter "Category=smoke"
```
