using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using Sirenix.OdinInspector;


namespace HCB.IncrimantalIdleSystem
{
    public interface IStat 
    {
        public StatData StatData { get; }
        public void UpgradeStat();
    }
}
