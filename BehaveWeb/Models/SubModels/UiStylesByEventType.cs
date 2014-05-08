using System.Collections.Generic;
using Behave.BehaveCore.DataClasses;

namespace Behave.Models.SubModels
{
    public enum ClientCssClassNames
    {
        deemedGreat,
        deemedGood,
        deemedOkay,
        deemedBad
    }

    public class UiStylesByEventType : Dictionary<OccurrenceType, ClientCssClassNames>
    {
        public UiStylesByEventType() : base()
        {
            Add(OccurrenceType.UnplannedNotComplete, ClientCssClassNames.deemedBad);
            Add(OccurrenceType.PartiallyCompleted, ClientCssClassNames.deemedOkay);
            Add(OccurrenceType.PlannedNotComplete, ClientCssClassNames.deemedGood);
            Add(OccurrenceType.Completed, ClientCssClassNames.deemedGreat);
        }
    }
}
