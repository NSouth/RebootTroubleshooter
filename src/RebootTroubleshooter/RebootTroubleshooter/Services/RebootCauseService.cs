using RebootTroubleshooter.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace RebootTroubleshooter.Services
{
    class RebootCauseService
    {
        public static List<RebootCause> LoadKnownRebootCauses(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<RebootCause>>(json) ?? new List<RebootCause>();
        }
    }
}
