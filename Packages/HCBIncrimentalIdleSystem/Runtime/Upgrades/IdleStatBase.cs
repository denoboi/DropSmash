using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using HCB.Utilities;
using Sirenix.OdinInspector;

namespace HCB.IncrimantalIdleSystem
{
    [System.Serializable]
    public class StatData
    {
        private SavedStatData savedStatData;
        protected SavedStatData SavedStatData
        {
            get
            {
                if (IdleStatData == null)
                    return null;

                IdleSaveData idleSaveData = (IdleSaveData)SaveLoadManager.LoadPDP<IdleSaveData>("IdleData", new IdleSaveData());
                savedStatData = idleSaveData.GetStatData(IdleStatData.StatID);
                return savedStatData;
            }
        }

        [InlineEditor(InlineEditorModes.GUIOnly)]
        public IdleStatData IdleStatData;


        [BoxGroup("CostValues")]
        [ShowIf("hasData")]
        [ShowInInspector]
        [ReadOnly]
        public double CurrentCost { get { return (IdleStatData == null) ? 0 : Mathf.RoundToInt(IdleStatData.InitialCost * Mathf.Pow(IdleStatData.CostMultiplier, Level)); } }


        [BoxGroup("StatValues")]
        [ShowIf("hasData")]
        [ShowInInspector]
        [ReadOnly]
        public double CurrentValue { get { return (IdleStatData == null) ? 0 : IdleStatData.InitialValue + (Level * IdleStatData.UpgradeMultiplier); } }

        [ShowIf("hasData")]
        [ShowInInspector]
        [InlineButton("ResetData")]
        public int Level { get { return (SavedStatData == null) ? 0 : SavedStatData.Level; } set { SavedStatData.Level = value; } }

        private bool hasData { get { return IdleStatData; } }

        public void SaveData()
        {
            IdleSaveData idleSaveData = (IdleSaveData)SaveLoadManager.LoadPDP<IdleSaveData>("IdleData", new IdleSaveData());
            idleSaveData.SetStatData(IdleStatData.StatID, savedStatData);
            SaveLoadManager.SavePDP(idleSaveData, "IdleData");
        }

        private void ResetData()
        {
            Level = 0;
            SaveData();
        }
    }


    public class IdleStatBase : MonoBehaviour, IStat
    {
        [SerializeField]
        private StatData statData;
        public StatData StatData => statData;

        private void Start()
        {
            Debug.Log(StatData.Level);
        }

        [Button]
        public virtual void UpgradeStat()
        {
            StatData.Level++;
            StatData.SaveData();
            EventManager.OnStatUpdated.Invoke(StatData.IdleStatData.StatID);
        }

        [Button]
        private void ResetStats()
        {
            StatData.Level = 0;
            StatData.SaveData();
        }
    }
}
