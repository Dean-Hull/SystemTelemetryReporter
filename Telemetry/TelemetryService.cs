using System.Diagnostics;
using SystemTelemetryReporter.Telemetry.Constants;
using SystemTelemetryReporter.Telemetry.Constants.PerformanceCounters;

namespace SystemTelemetryReporter.Telemetry
{
    internal class TelemetryService
    {
        public static IReadOnlyList<PerformanceCounterDefinition>? Definitions { get; private set; }
        private static List<PerformanceCounter>? _counters = [];

        public static void Initialise(IEnumerable<PerformanceCounterDefinition> definitions)
        {
            try
            {
                if (definitions == null) return;

                Definitions = definitions.ToList();
                _counters = [];

                foreach (PerformanceCounterDefinition definition in definitions)
                {
                    PerformanceCounter counter = string.IsNullOrWhiteSpace(definition.Instance)
                    ? new PerformanceCounter(definition.Category, definition.Counter)
                    : new PerformanceCounter(definition.Category, definition.Counter, definition.Instance);
                    counter.NextValue();
                    _counters.Add(counter);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing performance counters: {ex.Message}");
                Definitions = null;
                _counters = null;
            }
        }

        public static IReadOnlyList<double> Read()
        {
            if (_counters == null) return [];

            List<double> values = [];

            foreach (PerformanceCounter counter in _counters)
            {
                values.Add(counter.NextValue());
            }

            return values;
        }

        public static List<PerformanceCounterDefinition> GetPerformanceCounters()
        {
            return
            [
                new(Categories.MEMORY, MemoryConstants.COMMITTED_BYTES),
                new(Categories.MEMORY, MemoryConstants.AVAILABLE_BYTES),
                new(Categories.MEMORY, MemoryConstants.AVAILABLE_MEGABYTES),
                new(Categories.MEMORY, MemoryConstants.PERCENT_COMMITTED_BYTES_IN_USE),
                new(Categories.MEMORY, MemoryConstants.CACHE_BYTES),
                new(Categories.MEMORY, MemoryConstants.POOL_PAGED_BYTES),
                new(Categories.MEMORY, MemoryConstants.POOL_NONPAGED_BYTES),
                new(Categories.MEMORY, MemoryConstants.PAGES_PER_SECOND),
                new(Categories.MEMORY, MemoryConstants.PAGE_FAULTS_PER_SECOND),

                new(Categories.SYSTEM, SystemConstants.SYSTEM_PROCESSES),
                new(Categories.SYSTEM, SystemConstants.SYSTEM_UPTIME),
                new(Categories.SYSTEM, SystemConstants.CONTEXT_SWITCHES_PER_SECOND),
                new(Categories.SYSTEM, SystemConstants.PROCESS_QUEUE_LENGTH),

                new(Categories.PROCESS, ProcessConstants.THREAD_COUNT, "_Total"),
                new(Categories.PROCESS, ProcessConstants.HANDLES, "_Total"),

                new(Categories.LOGICAL_DISK, LogicalDiskConstants.DISK_TIME_PERCENT, "_Total"),
                new(Categories.LOGICAL_DISK, LogicalDiskConstants.DISK_READ_TIME_PERCENT, "_Total"),
                new(Categories.LOGICAL_DISK, LogicalDiskConstants.DISK_WRITE_TIME_PERCENT, "_Total"),
                new(Categories.LOGICAL_DISK, LogicalDiskConstants.DISK_IDLE_PERCENT, "_Total"),
                new(Categories.LOGICAL_DISK, LogicalDiskConstants.DISK_BYTES_PER_SECOND, "_Total"),
                new(Categories.LOGICAL_DISK, LogicalDiskConstants.DISK_WRITE_BYTES_PER_SECOND, "_Total"),
                new(Categories.LOGICAL_DISK, LogicalDiskConstants.DISK_TRANSFERS_PER_SECOND, "_Total"),
                new(Categories.LOGICAL_DISK, LogicalDiskConstants.DISK_READS_PER_SECOND, "_Total"),
                new(Categories.LOGICAL_DISK, LogicalDiskConstants.DISK_WRITES_PER_SECOND, "_Total"),
                new(Categories.LOGICAL_DISK, LogicalDiskConstants.AVG_DISK_SEC_TRANSFER, "_Total"),
                new(Categories.LOGICAL_DISK, LogicalDiskConstants.AVG_DISK_SEC_READ, "_Total"),
                new(Categories.LOGICAL_DISK, LogicalDiskConstants.AVG_DISK_SEC_WRITE, "_Total"),
                new(Categories.LOGICAL_DISK, LogicalDiskConstants.AVG_DISK_QUEUE_LENGTH, "_Total"),
                new(Categories.LOGICAL_DISK, LogicalDiskConstants.AVG_DISK_READ_QUEUE_LENGTH, "_Total"),
                new(Categories.LOGICAL_DISK, LogicalDiskConstants.AVG_DISK_WRITE_QUEUE_LENGTH, "_Total"),
                new(Categories.LOGICAL_DISK, LogicalDiskConstants.FREE_SPACE_PERCENT, "_Total"),
                new(Categories.LOGICAL_DISK, LogicalDiskConstants.FREE_MEGA_BYTES, "_Total")
            ];
        }
    }
}