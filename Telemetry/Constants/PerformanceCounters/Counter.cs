namespace SystemTelemetryReporter.Telemetry.Constants.PerformanceCounters
{
    internal class PerformanceCounterDefinition(string category, string counter, string? instance = null)
    {
        public string Category { get; set; } = category;
        public string Counter { get; set; } = counter;
        public string? Instance { get; set; } = instance;
    }
}