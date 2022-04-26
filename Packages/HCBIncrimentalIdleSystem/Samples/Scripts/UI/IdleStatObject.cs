using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using HCB.Core;

namespace HCB.IncrimantalIdleSystem.Examples
{
    public class IdleStatObject : StatObjectBase
    {
        private TextMeshProUGUI valueText;
        protected TextMeshProUGUI ValueText { get { return (valueText == null) ? valueText = GetComponent<TextMeshProUGUI>() : valueText; } }


       

        public override void UpdateStat(string id)
        {
            if (!string.Equals(id, StatData.IdleStatData.StatID))
                return;

            ValueText.SetText(StatData.IdleStatData.StatID + " Current Value" + StatData.CurrentValue.ToString());
        }
    }
}
