namespace SystemTelemetryReporter.Telemetry.Constants.PerformanceCounters
{
    internal static class Memory
    {
        public const string COMMITTED_BYTES = "Committed Bytes";
        public const string AVAILABLE_BYTES = "Available Bytes";
        public const string AVAILABLE_MEGABYTES = "Available MBytes";
        public const string PERCENT_COMMITTED_BYTES_IN_USE = "% Committed Bytes In Use";
        public const string CACHE_BYTES = "Cache Bytes";
        public const string POOL_PAGED_BYTES = "Pool Paged Bytes";
        public const string POOL_NONPAGED_BYTES = "Pool Nonpaged Bytes";
        public const string PAGES_PER_SECOND = "Pages/sec";
        public const string PAGE_FAULTS_PER_SECOND = "Page Faults/sec";
    }

    internal static class Processor
    {
        public const string PROCESSOR_TIME_PERCENT = "% Processor Time";
        public const string PROCESSOR_USER_TIME_PERCENT = "% User Time";
        public const string PROCESSOR_FREQUENCY = "Processor Frequency";
    }

    internal static class System
    {
        public const string SYSTEM_PROCESSES = "Processes";
        public const string SYSTEM_UPTIME = "System Up Time";
        public const string CONTEXT_SWITCHES_PER_SECOND = "Context Switches/sec";
        public const string PROCESS_QUEUE_LENGTH = "Processor Queue Length";
    }

    internal static class Process
    {
        public const string THREAD_COUNT = "Thread Count";
        public const string HANDLES = "Handle Count";
    }

    internal static class LogicalDisk
    {
        public const string DISK_TIME_PERCENT = "% Disk Time";
        public const string DISK_READ_TIME_PERCENT = "% Disk Read Time";
        public const string DISK_WRITE_TIME_PERCENT = "% Disk Write Time";
        public const string DISK_IDLE_PERCENT = "% Idle Time";
        public const string DISK_BYTES_PER_SECOND = "Disk Bytes/sec";
        public const string DISK_WRITE_BYTES_PER_SECOND = "Disk Write Bytes/sec";
        public const string DISK_TRANSFERS_PER_SECOND = "Disk Transfers/sec";
        public const string DISK_READS_PER_SECOND = "Disk Reads/sec";
        public const string DISK_WRITES_PER_SECOND = "Disk Writes/sec";
        public const string AVG_DISK_SEC_TRANSFER = "Avg. Disk sec/Transfer";
        public const string AVG_DISK_SEC_READ = "Avg. Disk sec/Read";
        public const string AVG_DISK_SEC_WRITE = "Avg. Disk sec/Write";
        public const string AVG_DISK_QUEUE_LENGTH = "Avg. Disk Queue Length";
        public const string AVG_DISK_READ_QUEUE_LENGTH = "Avg. Disk Read Queue Length";
        public const string AVG_DISK_WRITE_QUEUE_LENGTH = "Avg. Disk Write Queue Length";
        public const string FREE_SPACE_PERCENT = "% Free Space";
        public const string FREE_MEGA_BYTES = "Free Megabytes";
    }
}