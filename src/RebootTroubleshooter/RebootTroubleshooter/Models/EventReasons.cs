using System.Collections.Generic;

namespace RebootTroubleshooter.Models
{
    internal static class EventReasonCodes
    {
        public static readonly Dictionary<string, string> Dictionary = new Dictionary<string, string>()
        {
            { "0x0", "Unknown" },
            { "0x1", "Hardware issue" },
            { "0x2", "Power failure" },
            { "0x3", "Manual shutdown" },
            { "0x4", "Automatic update" },
            { "0x5", "System failure" },
            { "0x6", "Service pack installation" },
            { "0x7", "New Year's Day" },
            { "0x8", "User-defined" },
            { "0x9", "Application" },
            { "0xa", "System failure: Stop error" },
            { "0xb", "Planned scheduled task" },
            { "0xc", "Unplanned scheduled task" },
            { "0xd", "System idle" },
            { "0xe", "Power loss expected" },
            { "0xf", "Session disconnect" },
            { "0x10", "Environment" },
            { "0x11", "Operating System Recovery" },
            { "0x12", "Driver Installation" },
            { "0x13", "Critical process died" },
            { "0x14", "Kernel security check failure" },
            { "0x15", "Manual restart" },
            { "0x16", "Unresponsive UI" },
            { "0x17", "Unresponsive application" },
            { "0x18", "Unresponsive Windows" },
            { "0x19", "System overheating" },
            { "0x1a", "System firmware update" },
            { "0x1b", "System scan" },
            { "0x1c", "Windows Update" },
            { "0x1d", "Network connectivity (Unplanned)" },
            { "0x1e", "Update initiated by user" },
            { "0x1f", "System partitions" },
            { "0x20", "Secure boot" },
            { "0x21", "Event timer" },
            { "0x22", "Customized Software Update" },
            { "0x23", "Windows clean boot" },
            { "0x24", "System trigger" },
            { "0x25", "Low battery capacity" },
            { "0x26", "Driver update required" },
            { "0x27", "Recommended update" },
            { "0x28", "Enforcement policy" },
            { "0x29", "Volume snapshot" },
            { "0x2a", "Windows firewall" },
            { "0x2b", "Data deduplication" },
            { "0x2c", "Storage space" },
            { "0x2d", "Device control policy" },
            { "0x2e", "System partition expanded" },
            { "0x2f", "Log collection" },
            { "0x30", "System partition changed" },
            { "0x31", "Defer upgrade" },
            { "0x32", "Upgrade error" },
            { "0x33", "Feature update" },
            { "0x34", "Setup" },
            { "0x500ff", "No title for this reason could be found" },
            { "0x80020010", "No title for this reason could be found" }
        };

    }
}
