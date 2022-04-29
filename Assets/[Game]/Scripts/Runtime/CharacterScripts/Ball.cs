using UnityEngine;
using RayFire;
using HCB.Core;

public class Ball : MonoBehaviour {
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private GameObject _poofPrefab;
    private bool _isGhost;

    public void Init(Vector3 velocity, bool isGhost) {
        _isGhost = isGhost;
        _rb.AddForce(velocity * _rb.mass, ForceMode.Impulse);
    }

    public void OnCollisionEnter(Collision col) {
        if (_isGhost) return;
        Instantiate(_poofPrefab, col.contacts[0].point, Quaternion.Euler(col.contacts[0].normal));
        _source.clip = _clips[Random.Range(0, _clips.Length)];
        _source.Play();

        

       

    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        RayfireRigid rigid = other.GetComponentInParent<RayfireRigid>();

        if (rigid == null)
            return;

        if (other.gameObject.CompareTag("Rock")) //hammer kaya'ya carptigi zaman.
        {
            Invoke("DestroyGameObject", .5f);
            HapticManager.Haptic(HapticTypes.RigidImpact);

        }

        //rigid.ApplyDamage(1, other.GetContact(0).point);
        rigid.Activate();
        rigid.Initialize();
    }


}