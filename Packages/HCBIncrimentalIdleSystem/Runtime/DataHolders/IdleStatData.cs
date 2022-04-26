using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace HCB.IncrimantalIdleSystem
{
    public class IdleStatData : ScriptableObject
    {
        [Header("SetUp @Dev")]
        public string StatID;
        public Sprite Icon;

        [Header("Settings @Design")]
        [BoxGroup("StatValues")]
        public float InitialValue = 1;

        [BoxGroup("StatValues")]
        [PropertyRange(0.1f, 2)]
        public float UpgradeMultiplier = 1.2f;

        [BoxGroup("CostValues")]
        public float InitialCost = 40;

        [BoxGroup("CostValues")]
        [PropertyRange(1f, 1.9f)]
        public float CostMultiplier = 1.1f;

        [BoxGroup("CostValues")]
        public ExchangeType ExchangeType = ExchangeType.Coin;


        [Header("Debug")]
        [ReadOnly]
        [SerializeField]
        private int Level;

        [BoxGroup("CostValues")]
        [ShowInInspector]
        [ReadOnly]
        public double CurrentCost { get { return  Mathf.RoundToInt(InitialCost * Mathf.Pow(CostMultiplier, Level)); } }


        [BoxGroup("StatValues")]
        [ShowInInspector]
        [ReadOnly]
        public double CurrentValue { get { return (Level * UpgradeMultiplier) + InitialValue; } }


        [Button]
        private void UpgradeStat()
        {
            Level++;
        }

        [Button]
        private void ResetData()
        {
            Level = 0;
        }

    }
}
