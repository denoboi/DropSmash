using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gem : MonoBehaviour
{

   [Button]
   void OnRevealed()
    {
        EventManager.OnRevealed.Invoke(transform.position);
    }
}
