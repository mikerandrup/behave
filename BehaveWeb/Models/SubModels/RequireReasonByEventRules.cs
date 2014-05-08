using System.Collections.Generic;
using Behave.BehaveCore.DataClasses;

namespace Behave.Models.SubModels
{
    public class RequireReasonByEventRules : Dictionary<OccurrenceType, bool>
    {
        public RequireReasonByEventRules() : base()
        {
            Add(OccurrenceType.UnplannedNotComplete, true);
            Add(OccurrenceType.PlannedNotComplete, true);
            Add(OccurrenceType.Completed, false);
            Add(OccurrenceType.PartiallyCompleted, true);
        }
    }
}