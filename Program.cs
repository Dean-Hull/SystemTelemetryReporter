using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using SystemTelemetryReporter.Identity;
using SystemTelemetryReporter.Telemetry;
using SystemTelemetryReporter.Telemetry.Constants.PerformanceCounters;

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

        List<PerformanceCounterDefinition> counters = TelemetryService.GetPerformanceCounters();
        TelemetryService.Initialise(counters);

        _ = ReportTelemetry(deviceClient);
        await Task.Delay(Timeout.Infinite);
    }

    private static async Task ReportTelemetry(DeviceClient deviceClient)
    {
        while(true)
        {
            IReadOnlyList<double> values = TelemetryService.Read();
            IReadOnlyList<PerformanceCounterDefinition>? definitions = TelemetryService.Definitions;
            Dictionary<string, double> payload = [];

            for (int i = 0; i < values.Count; i++)
            {
                PerformanceCounterDefinition? definition = definitions?[i];
                if (definition != null)
                {
                    payload.Add($"{definition.Category}_{definition.Counter}", values[i]);
                }
            }

            string message = JsonConvert.SerializeObject(payload);
            await deviceClient.SendEventAsync(new Message(System.Text.Encoding.UTF8.GetBytes(message)));
            Console.WriteLine($"Telemetry sent: {message}");
            await Task.Delay(TimeSpan.FromMinutes(TELEMETRY_REPORT_INTERVAL_MINUTES));
        }
    }
}