﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using HCB.Core;

namespace HCB.Utilities
{
    [System.Serializable]
    public class PlayerData
    {

        public PlayerData()
        {
            CurrencyData = new Dictionary<ExchangeType, int>();
            CurrencyData[ExchangeType.Coin] = 0;
            
        }


        private Dictionary<ExchangeType, int> currencyData = new Dictionary<ExchangeType, int>();
        [BoxGroup("Currency Data")]
        [ShowInInspector]
        [OnValueChanged("NotifyChange")]
        public Dictionary<ExchangeType, int> CurrencyData
        {
            get
            {
                return currencyData;
            }
            set
            {
                currencyData = value;
            }
        }

        private void NotifyChange()
        {
            EventManager.OnPlayerDataChange.Invoke();
        }
    }
}