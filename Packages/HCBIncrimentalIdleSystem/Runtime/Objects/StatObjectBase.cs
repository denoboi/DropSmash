using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;

namespace HCB.IncrimantalIdleSystem
{
    public abstract class StatObjectBase : MonoBehaviour, IStatObject
    {
        [SerializeField]
        private StatData statData;
        public StatData StatData { get { return (statData == null) ? statData = new StatData(): statData; } set { statData = value; } }

        protected virtual void Start()
        {
            UpdateStat(StatData.IdleStatData.StatID);
        }

        protected virtual void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            EventManager.OnStatUpdated.AddListener(UpdateStat);
        }

        protected virtual void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            EventManager.OnStatUpdated.RemoveListener(UpdateStat);
        }

        public abstract void UpdateStat(string id);
    }
}
