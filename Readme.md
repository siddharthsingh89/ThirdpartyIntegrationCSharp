## ExternalDataService

### Overview


ExternalDataService is a .NET 9 solution designed to integrate with third-party user data APIs. It provides a clean abstraction for fetching, converting, and caching user data and is built with extensibility and reliability in mind.

### Build Instructions


1. Prerequisites
   - .NET 9 SDK installed
   - (Optional) Visual Studio 2022 or later

2. Restore Dependencies
   dotnet restore

3. Build the Solution
   dotnet build

### Test Instructions


1. Run All Tests
   dotnet test

## Key Design Decisions


- Dependency Injection: All services are registered using IServiceCollection extensions for easy integration and testing.
- 
- HttpClient Factory: Uses AddHttpClient for resilient HTTP calls, with Polly for retry and timeout policies.
- 
- Error Handling: The ExternalDataClient handles HTTP errors, deserialization issues, and timeouts gracefully, logging errors and returning null or empty results as appropriate.
- 
- Separation of Concerns: The solution separates DTOs, domain models, and service logic for maintainability.
- 
- Testing: Unit tests mock external dependencies and cover both success and failure scenarios.

- Caching: Implements a simple in-memory cache for user data to reduce API calls and improve performance.

- Configuration: Uses a configuration settings to manage API endpoints and cache eviction policies, allowing for easy adjustments without code changes.


## Contact


For questions or contributions, please open an issue or submit a pull request.

## License
MIT License