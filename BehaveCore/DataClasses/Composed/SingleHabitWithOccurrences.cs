using System;
using System.Linq;
using System.Collections.Generic;

namespace Behave.BehaveCore.DataClasses
{
    public class SingleHabitWithOccurrences
    {
        public SingleHabitWithOccurrences()
        {
            Occurrences = new List<Occurrence>();
        }
        public Habit Habit { get; set; }
        public List<Occurrence> Occurrences { get; set; }

        public bool HasOccurrences
        {
            get
            {
                return Occurrences.Count > 0;
            }
        }

        public string OccurrenceIdListAsCommaDelimitedString
        {
            get
            {
                if (HasOccurrences)
                {
                    string[] occIdStrings = new string[Occurrences.Count];
                    int i = 0;
                    foreach (var occ in Occurrences)
                    {
                        occIdStrings[i] = occ.OccurrenceId.ToString();
                        i++;
                    }
                    return String.Join(",", occIdStrings);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public OccurrenceType FirstOccurrenceType
        {
            get
            {
                return HasOccurrences
                    ? Occurrences.First().EventType
                    : OccurrenceType.Pending;
            }
        }
    }
}
