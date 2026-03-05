namespace SystemTelemetryReporter.Identity
{
    internal class DeviceIdentityService
    {
        private const string IOT_HUB_CONNECTION_STRING = "";
        private readonly string _deviceConnectionString = string.Empty;

        public static async Task<string?> CreateDeviceIdentityAsync(string deviceId)
        {
            Console.WriteLine("Running CreateDeviceIdentityAsync...");
            Console.WriteLine($"Device ID: {deviceId}");
            return null;
        }
    }
}