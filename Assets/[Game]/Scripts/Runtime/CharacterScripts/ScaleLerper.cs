using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleLerper : MonoBehaviour
{
   
    private Vector3 _defaultScale;

    [SerializeField] private Vector3 _minScale;
    [SerializeField] private float _fadeDuration;
    

    void Start()
    {
        //ilk scale'i default scale'e esitliyorum
        _defaultScale = transform.localScale;
        

    }


    //bu methoda dikkat
    public IEnumerator FadeMallet(Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time);

        while(i <  1.0f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }

    
    //yere degdiginde yok olacak.
   IEnumerator OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //once 3 saniye bekleyecek
            yield return new WaitForSeconds(5);

            //ardindan lerp ile scale'i 0 yapacagim
            yield return FadeMallet(_defaultScale, _minScale, _fadeDuration);

            //seek and destroy yapmazsak bir suru scale 0 mallet kaliyor.
            Destroy(gameObject);
        }
            
    }
   

}
