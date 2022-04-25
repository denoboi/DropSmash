using HCB.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemManager : MonoBehaviour //this script helps to count gems in order to end or restart the level on another script
{

    public List<GameObject>_gemCount = new List<GameObject>();

   public void RemoveGem(GameObject gem)
    {
        _gemCount.Remove(gem);
        CheckGemCount();
    }

    public void CheckGemCount()
    {
        if(_gemCount.Count <= 0)
        {
            Debug.Log("all revealed");
            GameManager.Instance.CompeleteStage(true);
        }
    }
   
}
