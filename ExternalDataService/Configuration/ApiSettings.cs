namespace ExternalDataService.Configuration
{
    public class ApiSettings
    {
        public string BaseUrl { get; set; }
        
        // will later move it to secret manager or environment variable in production
        public string ApiKey { get; set; } // Default API key, can be overridden in configuration
    }
}