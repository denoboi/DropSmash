using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Utilities;
using DG.Tweening;

public class GemPanel : MonoBehaviour
{
    [SerializeField] GameObject _gemPrefab;
    [SerializeField] Transform _gemHolder;

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

        gem.transform.DOLocalMove(Vector3.zero, .5f).OnComplete(() => Destroy(gem));


     }
}