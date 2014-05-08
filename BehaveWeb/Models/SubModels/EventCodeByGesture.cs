using System.Collections.Generic;
using Behave.BehaveCore.DataClasses;

namespace Behave.Models.SubModels
{
    public class EventCodeByGestureRules : Dictionary<Gestures, OccurrenceType>
    {
        public EventCodeByGestureRules()
            : base()
        {
            Add(Gestures.farLeft, OccurrenceType.UnplannedNotComplete);
            Add(Gestures.slightLeft, OccurrenceType.PlannedNotComplete);
            Add(Gestures.tap, OccurrenceType.Completed);
            Add(Gestures.slightRight, OccurrenceType.Completed);
            Add(Gestures.farRight, OccurrenceType.PartiallyCompleted);
        }
    }
}