using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gem : MonoBehaviour
{

    [SerializeField] Color _color;


    private GemManager gemManager ;
    private void Awake()
    {
        gemManager = FindObjectOfType<GemManager>();

        if(gemManager != null)
        {
            gemManager._gemCount.Add(gameObject);
        }
    }

    //when gems collide with the ground I moved them to the upper right corner. Change it with Coroutine
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("HITTHEGROUND");
            OnRevealed();
            if(gemManager != null)
            {
                gemManager.RemoveGem(gameObject);
            }


            Destroy(gameObject);
            
        }
    }

    //this is the method that throws gems to the upper right. I invoked it here and listened in GemPanel.cs
    void OnRevealed()
    {
        EventManager.OnRevealed.Invoke(transform.position, _color);
    }
}
