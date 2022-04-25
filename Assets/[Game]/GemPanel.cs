using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Utilities;
using DG.Tweening;

public class GemPanel : MonoBehaviour
{
    [SerializeField] GameObject _gemPrefab;
    [SerializeField] Transform _gemHolder;
    private Tween _punchTween;

    private void OnEnable()
    {
        EventManager.OnRevealed.AddListener(CreateGemImage);
    }

    private void OnDisable()
    {
        EventManager.OnRevealed.RemoveListener(CreateGemImage);
    }

    void CreateGemImage(Vector3 worldPosition)
    {
        GameObject gem = Instantiate(_gemPrefab, _gemHolder);
        gem.transform.position = HCBUtilities.WorldToUISpace(transform.root.GetComponent<Canvas>(), worldPosition);

        //yukari cikan gemleri yok ediyoruz. + DoPunchScale
        gem.transform.DOLocalMove(Vector3.zero, .5f).OnComplete(() => 
        { Destroy(gem);
            if (_punchTween != null) //to prevent punchtween bug(it was too big on UI)
                _punchTween.Kill(true);
         _punchTween = _gemHolder.transform.DOPunchScale(Vector3.one * 0.1f, 0.5f); 
        });

        
     }
}