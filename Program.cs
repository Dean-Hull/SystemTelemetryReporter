using Microsoft.Azure.Devices.Client;
using SystemTelemetryReporter.Identity;

internal class Program
{
    private const int TELEMETRY_REPORT_INTERVAL_MINUTES = 30;

    private static async Task Main(string[] args)
    {
        string deviceId = Environment.MachineName;

        string? deviceConnectionString = await DeviceIdentityService.CreateDeviceIdentityAsync(deviceId);
        if (deviceConnectionString == null)
        {
            Console.WriteLine("Failed to create device identity.");
            return;
        }

        Console.WriteLine("Device identity created successfully.");

        DeviceClient deviceClient = DeviceClient.CreateFromConnectionString(deviceConnectionString);

        if (deviceClient == null)
        {
            Console.WriteLine("Failed to create device client.");
            return;
        }

        Console.WriteLine("Device client created successfully.");
        Console.WriteLine($"Reporting telemetry every {TELEMETRY_REPORT_INTERVAL_MINUTES} minutes...");
    }
}