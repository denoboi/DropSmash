using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using HCB.Core;
using DG.Tweening;

public class Countdown : MonoBehaviour
{

    private void OnEnable()
    {
        SceneController.Instance.OnSceneLoaded.AddListener(Reset);
    }

    private void OnDisable()
    {
        SceneController.Instance.OnSceneLoaded.RemoveListener(Reset);
    }

    private Tween _punchTween;
    private float _currentTime;
    public float startingTime = 10f;

    TextMeshProUGUI _countdownText;
    void Start()
    {
        //_currentTime = startingTime;
        _countdownText = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        Timer();
    }


    void Timer()
    {
        if (!LevelManager.Instance.IsLevelStarted)
            return;

        _currentTime -= 1 * Time.deltaTime;
        _countdownText.text = _currentTime.ToString("0");

        if(_currentTime <= 35)
        {
            _countdownText.color = new Color32(123,224,6,255);
        }

        if(_currentTime <= 20 && _currentTime >= 10)
        {
            _countdownText.color = new Color32(248,144,6,255);
        }



        if(_currentTime <= 10)
        {
            //you should say "new" and give 4 parameters
            _countdownText.color = new Color32(166,4,23,255);

            if (_punchTween != null) //to prevent punchtween bug(it was too big on UI)
                _punchTween.Kill(true);
            _punchTween = _countdownText.transform.DOPunchScale(Vector3.one * 1f, 5f,1,.5f);
            
        }


        if (_currentTime <= 0)
        {

            _currentTime = 0;
            GameManager.Instance.CompeleteStage(false);

        }


    }

    private void Reset()
    {
        _currentTime = startingTime;
    }
}


