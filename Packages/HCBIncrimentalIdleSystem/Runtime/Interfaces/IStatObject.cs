using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.IncrimantalIdleSystem
{
    public interface IStatObject 
    {
        public StatData StatData { get; }
        public abstract void UpdateStat(string id);
    }
}
