using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using HCB.Core;
using HCB.Utilities;
using TMPro;

namespace HCB.IncrimantalIdleSystem.Examples
{
    public class IdleUpgradeButton : IdleStatBase
    {
        private Button button;
        protected Button Button { get { return (button == null) ? button = GetComponent<Button>() : button; } }

        [Header("Components")]
        [SerializeField]
        private TextMeshProUGUI StatIDText;
        [SerializeField]
        private TextMeshProUGUI StatLevelText;
        [SerializeField]
        private TextMeshProUGUI StatCostText;
        [SerializeField]
        private Image StatIconImage;

        private void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            SceneController.Instance.OnSceneLoaded.AddListener(InitializeButton);
            Button.onClick.AddListener(UpgradeStat);
            EventManager.OnStatUpdated.AddListener(CheckBuyablity);
        }

        private void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            SceneController.Instance.OnSceneLoaded.RemoveListener(InitializeButton);
            Button.onClick.RemoveListener(UpgradeStat);
            EventManager.OnStatUpdated.RemoveListener(CheckBuyablity);
        }

        private void CheckBuyablity(string id)
        {
            Button.interactable = GameManager.Instance.PlayerData.CurrencyData[StatData.IdleStatData.ExchangeType] > StatData.CurrentCost;
        }

        private void InitializeButton()
        {
            Button.interactable = GameManager.Instance.PlayerData.CurrencyData[StatData.IdleStatData.ExchangeType] > StatData.CurrentCost;
            StatIDText.SetText(StatData.IdleStatData.StatID);
            StatLevelText.SetText("lvl " + StatData.Level);
            StatCostText.SetText(HCBUtilities.ScoreShow(StatData.CurrentCost));
            StatIconImage.sprite = StatData.IdleStatData.Icon;
        }

        public override void UpgradeStat()
        {
            if (GameManager.Instance.PlayerData.CurrencyData[StatData.IdleStatData.ExchangeType] < StatData.CurrentCost)
            {
                Button.interactable = false;
                return;
            }


            GameManager.Instance.PlayerData.CurrencyData[ExchangeType.Coin] -= (int)StatData.CurrentCost;
            //EventManager.OnCurrencyInteracted.Invoke(StatData.IdleStatData.ExchangeType, GameManager.Instance.PlayerData.CurrencyData[StatData.IdleStatData.ExchangeType]);
            EventManager.OnPlayerDataChange.Invoke();
            base.UpgradeStat();
            Button.interactable = GameManager.Instance.PlayerData.CurrencyData[StatData.IdleStatData.ExchangeType] > StatData.CurrentCost;
            StatLevelText.SetText("lvl " + StatData.Level);
            StatCostText.SetText(StatData.CurrentCost.ToString());
        }
    }
}
