using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;

namespace SystemTelemetryReporter.Identity
{
    internal class DeviceIdentityService
    {
        private const string IOT_HUB_CONNECTION_STRING = "IOT_HUB_CONNECTION_STRING";
        private const string HOST_NAME= "HOST_NAME";

        public static async Task<string?> CreateDeviceIdentityAsync(string deviceId)
        {
            Console.WriteLine("Creating device identity...");
            Console.WriteLine($"Device ID: {deviceId}");

            string iotConnectionString = Environment.GetEnvironmentVariable(IOT_HUB_CONNECTION_STRING) ?? throw new InvalidOperationException($"Environment variable '{IOT_HUB_CONNECTION_STRING}' is not set.");

            string hostName = Environment.GetEnvironmentVariable(HOST_NAME) ?? throw new InvalidOperationException($"Environment variable '{HOST_NAME}' is not set.");

            RegistryManager registryManager = RegistryManager.CreateFromConnectionString(iotConnectionString);
            Device device = new(deviceId);

            try
            {
                Console.WriteLine("Adding device identity to IoT Hub...");
                device = await registryManager.AddDeviceAsync(device);
            }
            catch(DeviceAlreadyExistsException)
            {
                Console.WriteLine("Device identity already exists. Retrieving device...");
                device = await registryManager.GetDeviceAsync(deviceId);
            }

            string connectionString = $"HostName={hostName};DeviceId={deviceId};SharedAccessKey=";
            string primaryKey = device.Authentication.SymmetricKey.PrimaryKey;

            return connectionString + primaryKey;
        }
    }
}