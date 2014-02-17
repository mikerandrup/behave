using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Behave.BehaveCore.Models
{
    public class Occurrence
    {
        int OccurrenceId { get; set; }
        DateTime Timestamp { get; set; }
        int HabitId { get; set; }
        string Notes { get; set; }
    }
}
