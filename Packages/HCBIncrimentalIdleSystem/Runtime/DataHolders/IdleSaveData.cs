using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace HCB.IncrimantalIdleSystem
{
    [System.Serializable]
    public class SavedStatData
    {
        public int Level = 1;
    }

    [System.Serializable]
    public class IdleSaveData 
    {
        [SerializeField]
        private Dictionary<string, SavedStatData> StatCollection = new Dictionary<string, SavedStatData>();

        public SavedStatData GetStatData(string id)
        {
            if (StatCollection.ContainsKey(id))
            {
                return StatCollection[id];
            }
            else
            {
                StatCollection.Add(id, new SavedStatData());
                return StatCollection[id];
            }
        }

        public void SetStatData(string id, SavedStatData savedStatData)
        {
            StatCollection[id] = savedStatData;
        }
    }
}
