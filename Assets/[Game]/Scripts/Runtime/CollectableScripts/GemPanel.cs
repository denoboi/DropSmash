using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Utilities;
using DG.Tweening;
using TMPro;
using HCB.Core;
using HCB.IncrimantalIdleSystem;
using UnityEngine.UI;


public class GemPanel : StatObjectBase
{
    [SerializeField] GameObject _gemPrefab;
    [SerializeField] Transform _gemHolder;
    private Tween _punchTween;

    private TextMeshProUGUI _scoreText;
    private int _numofGems = 0;
    private int _incomeRate = 1;



    protected override void OnEnable()
    {
        if (Managers.Instance == null)
            return;

        SceneController.Instance.OnSceneLoaded.AddListener(UpdateText);
        EventManager.OnPlayerDataChange.AddListener(UpdateText);

        _scoreText = GetComponentInChildren<TextMeshProUGUI>();
        Events.OnRevealed.AddListener(CreateGemImage);
    }

    protected override void OnDisable()
    {
        if (Managers.Instance == null)
            return;

        SceneController.Instance.OnSceneLoaded.RemoveListener(UpdateText);
        EventManager.OnPlayerDataChange.RemoveListener(UpdateText);

        Events.OnRevealed.RemoveListener(CreateGemImage);
        //SceneController.Instance.OnSceneLoaded.RemoveListener(() => _scoreText.text = 0.ToString());
    }

    private void UpdateText()
    {
        _scoreText.SetText(GameManager.Instance.PlayerData.CurrencyData[HCB.ExchangeType.Coin].ToString("N0"));
    }




    void CreateGemImage(Vector3 worldPosition, Color color)
    {
        GameObject gem = Instantiate(_gemPrefab, _gemHolder);
        gem.transform.position = HCBUtilities.WorldToUISpace(transform.root.GetComponent<Canvas>(), worldPosition);

        //changing color of 
        gem.GetComponent<Image>().color = color;

        //yukari cikartiyoruz daha sonra gemleri yok ediyoruz. + DoPunchScale
        gem.transform.DOLocalMove(Vector3.zero, .5f).OnComplete(() =>
        {
            Destroy(gem);
            if (_punchTween != null) //to prevent punchtween bug(it was too big on UI)
                _punchTween.Kill(true);
            _punchTween = _gemHolder.transform.DOPunchScale(Vector3.one * 0.1f, 0.5f);

            HapticManager.Haptic(HapticTypes.SoftImpact);

            //scoretextupdate
            
        });

        
     }

   

    public override void UpdateStat(string id)
    {
        if (!string.Equals(StatData.IdleStatData.StatID, id))
            return;

        //Incremental coin Upgrade
        _incomeRate = (int)StatData.CurrentValue;
        
        

    }
}