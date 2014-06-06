using System.Collections.Generic;
using Behave.BehaveCore.DataClasses;

namespace Behave.Models.SubModels
{
    public struct UiRules
    {
        public string gestureName;
        public int occurrenceCode;
        public bool requiresReason;
        public string cssClass;
    }

    public class ClientUiRuleDefinitions : List<UiRules>
    {
        public ClientUiRuleDefinitions() : base()
        {
            IDictionary<OccurrenceType, bool> requiresReason = new RequireReasonByEventRules();
            IDictionary<OccurrenceType, ClientCssClassNames> clientCssClass = new UiStylesByEventType();
            foreach (KeyValuePair<Gestures, OccurrenceType> rule in new EventCodeByGestureRules())
            {
                Add(
                    new UiRules()
                    {
                        gestureName = rule.Key.ToString(),
                        occurrenceCode = (int)rule.Value,
                        requiresReason = requiresReason[rule.Value],
                        cssClass = clientCssClass[rule.Value].ToString()
                    }
                );
            }
        }
    }
}
