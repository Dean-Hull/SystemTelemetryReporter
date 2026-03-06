# SystemTelemetryReporter

A Windows .NET console application that collects Windows Performance Counter telemetry from the local machine and reports it to **Azure IoT Hub** on a configurable interval. On startup the application auto-registers the device with IoT Hub using the machine hostname as the Device ID and begins streaming JSON telemetry.

This project was created as part of an asset digital twin test using Azure Digital Twins (ADT). Telemetry data is sent to Azure IoT Hub, routed to Blob Storage, and then consumed by ADT.

## Requirements

| Requirement | Version |
|---|---|
| .NET SDK | 8.0 |
| Target Framework | `net8.0-windows` |
| OS | Windows (Performance Counter API requirement) |
| Azure IoT Hub | Any tier (Free, S1, S2, S3) |

### NuGet Dependencies

| Package | Version |
|---|---|
| `Microsoft.Azure.Devices` | 1.41.0 |
| `Microsoft.Azure.Devices.Client` | 1.42.3 |
| `System.Diagnostics.PerformanceCounter` | 7.0.0 |

## Configuration

The application requires two environment variables.

Both variables must be set before running the application.

Replace the placeholder values with your Azure IoT Hub configuration.

| Variable | Description | Value to Set |
|---|---|---|
| `IOT_HUB_CONNECTION_STRING` | IoT Hub **service-level** connection string (Registry Read/Write permission) | `HostName=YOUR_IOT_HUB_SERVICE_HOSTNAME;SharedAccessKeyName=YOUR_KEY_NAME;SharedAccessKey=YOUR_SERVICE_KEY` |
| `HOST_NAME` | IoT Hub hostname used when constructing the device connection string | `YOUR_IOT_HUB_HOSTNAME` |

### Example (Replace placeholders with actual values)

```powershell
$env:IOT_HUB_CONNECTION_STRING = "HostName=your-iot-hub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=your_service_key_here"
$env:HOST_NAME = "your-host-name.azure-devices.net"
```

Or set them permanently via System Properties > Environment Variables.

## Build, Clean & Run

From the repository root, run the following `dotnet` CLI commands.

### Restore dependencies

```powershell
dotnet restore
```

### Clean

```powershell
dotnet clean
```

### Build

```powershell
dotnet build
```

### Run

```powershell
dotnet run
```

## How It Works

1. **Device registration** - On startup, the machine hostname (`Environment.MachineName`) is used as the Device ID. The application connects to IoT Hub via `RegistryManager` and registers the device. If the device already exists, it retrieves the existing record.
2. **Device client** - A `DeviceClient` is created from the device connection string.
3. **Telemetry loop** - Windows Performance Counters are initialised and read every **30 minutes**. The values are serialised to a JSON object and sent to IoT Hub as a device-to-cloud message.

## Console Output

```
Creating device identity...
Device ID: <HOSTNAME>
Adding device identity to IoT Hub...
Device identity already exists. Retrieving device...
Device identity created successfully.
Device client created successfully.
Reporting telemetry every 30 minutes...
Telemetry sent: { ... }
```

## Telemetry Payload Structure

Each message is a flat JSON object. Keys follow the pattern `<Category>_<Counter>`. All values are `double`.

```json
{
  "Memory_Committed Bytes":          0.0,
  "Memory_Available Bytes":          0.0,
  "Memory_Available MBytes":         0.0,
  "Memory_% Committed Bytes In Use": 0.0,
  "Memory_Cache Bytes":              0.0,
  "Memory_Pool Paged Bytes":         0.0,
  "Memory_Pool Nonpaged Bytes":      0.0,
  "Memory_Pages/sec":                0.0,
  "Memory_Page Faults/sec":          0.0,

  "System_Processes":                0.0,
  "System_System Up Time":           0.0,
  "System_Context Switches/sec":     0.0,
  "System_Processor Queue Length":   0.0,

  "Process_Thread Count":            0.0,
  "Process_Handle Count":            0.0,

  "LogicalDisk_% Disk Time":                  0.0,
  "LogicalDisk_% Disk Read Time":             0.0,
  "LogicalDisk_% Disk Write Time":            0.0,
  "LogicalDisk_% Idle Time":                  0.0,
  "LogicalDisk_Disk Bytes/sec":               0.0,
  "LogicalDisk_Disk Write Bytes/sec":         0.0,
  "LogicalDisk_Disk Transfers/sec":           0.0,
  "LogicalDisk_Disk Reads/sec":               0.0,
  "LogicalDisk_Disk Writes/sec":              0.0,
  "LogicalDisk_Avg. Disk sec/Transfer":       0.0,
  "LogicalDisk_Avg. Disk sec/Read":           0.0,
  "LogicalDisk_Avg. Disk sec/Write":          0.0,
  "LogicalDisk_Avg. Disk Queue Length":       0.0,
  "LogicalDisk_Avg. Disk Read Queue Length":  0.0,
  "LogicalDisk_Avg. Disk Write Queue Length": 0.0,
  "LogicalDisk_% Free Space":                 0.0,
  "LogicalDisk_Free Megabytes":               0.0
}
```

## Project Structure

```
SystemTelemetryReporter/
├── Program.cs                             # Entry point; device setup and telemetry loop
├── SystemTelemetryReporter.csproj
├── SystemTelemetryReporter.sln
├── Identity/
│   └── DeviceIdentityService.cs           # Registers / retrieves the device in IoT Hub
└── Telemetry/
    ├── TelemetryService.cs                # Initialises and reads Performance Counters
    └── Constants/
        └── PerformanceCounters/
            ├── Categories.cs              # Performance Counter category name constants
            ├── Counter.cs                 # PerformanceCounterDefinition model
            └── Counters.cs                # Counter name constants per category
```
