using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gem : MonoBehaviour
{

    //when gems collide with the ground I moved them to the upper right corner. 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("HITTHEGROUND");
            OnRevealed();
            Destroy(gameObject);
        }
    }

    //this is the method that throws gems to the upper right. I invoked it here and listened in GemPanel.cs
    void OnRevealed()
    {
        EventManager.OnRevealed.Invoke(transform.position);
    }
}
