using System.Collections.Generic;
using Behave.BehaveCore.DataClasses;

namespace Behave.Models.SubModels
{
    public struct UiRulesForGesture
    {
        public int occurrenceCode;
        public bool requiresReason;
    }

    public class UiRulesForGestureDefinitions : Dictionary<string, UiRulesForGesture>
    {
        public UiRulesForGestureDefinitions()
            : base()
        {
            IDictionary<OccurrenceType, bool> requiresReason = new RequireReasonByEventRules();
            foreach (KeyValuePair<Gestures, OccurrenceType> rule in new EventCodeByGestureRules())
            {
                var assembledRuleset = new UiRulesForGesture()
                {
                    occurrenceCode = (int)rule.Value,
                    requiresReason = requiresReason[rule.Value]
                };

                this.Add(rule.Key.ToString(), assembledRuleset);
            }
        }
    }

    public struct UiRulesForStatusCode
    {
        public int statusIntegerCode;
        public string statusDescription;
        public string cssClass;
    }

    public class UiRulesForStatusCodeDefinitions : Dictionary<string, UiRulesForStatusCode>
    {
        public UiRulesForStatusCodeDefinitions()
            : base()
        {
            foreach (KeyValuePair<OccurrenceType, ClientCssClassNames> rule in new UiStylesByEventType()) {
                var assembledRuleset = new UiRulesForStatusCode()
                {
                    statusIntegerCode = (int)rule.Key,
                    statusDescription = rule.Key.ToString(),
                    cssClass = rule.Value.ToString()
                };

                this.Add(((int)rule.Key).ToString(), assembledRuleset);
            }
        }
    }
}
