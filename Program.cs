using SystemTelemetryReporter.Identity;

internal class Program
{
    private static async Task Main(string[] args)
    {
        string deviceId = Environment.MachineName;

        string? deviceConnectionString = await DeviceIdentityService.CreateDeviceIdentityAsync(deviceId);
        if (deviceConnectionString == null)
        {
            Console.WriteLine("Failed to create device identity.");
            return;
        }
    }
}