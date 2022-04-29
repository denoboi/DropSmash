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

    [SerializeField] TextMeshProUGUI _countdownText;
    void Start()
    {
        _currentTime = startingTime;
    }
    void Update()
    {
        Timer();
    }


    void Timer()
    {

        _currentTime -= 1 * Time.deltaTime;
        _countdownText.text = _currentTime.ToString("0");

        if(_currentTime <= 35)
        {
            _countdownText.color = Color.green;
        }

        if(_currentTime <= 20 && _currentTime >= 10)
        {
            _countdownText.color = Color.yellow;
        }



        if(_currentTime <= 10)
        {
            _countdownText.color = Color.HSVToRGB(1,0,1);

            if (_punchTween != null) //to prevent punchtween bug(it was too big on UI)
                _punchTween.Kill(true);
            _punchTween = _countdownText.transform.DOPunchScale(Vector3.one * 0.1f, 0.5f);
            
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


