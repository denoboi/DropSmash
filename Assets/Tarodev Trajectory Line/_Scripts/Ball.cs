using UnityEngine;
using RayFire;

public class Ball : MonoBehaviour {
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private GameObject _poofPrefab;
    private bool _isGhost;

    public void Init(Vector3 velocity, bool isGhost) {
        _isGhost = isGhost;
        _rb.AddForce(velocity, ForceMode.Impulse);
    }

    public void OnCollisionEnter(Collision col) {
        if (_isGhost) return;
        Instantiate(_poofPrefab, col.contacts[0].point, Quaternion.Euler(col.contacts[0].normal));
        _source.clip = _clips[Random.Range(0, _clips.Length)];
        _source.Play();

        RayfireRigid rigid = col.collider.GetComponent<RayfireRigid>();

        if (rigid == null)
            return;

        rigid.ApplyDamage(1, col.GetContact(0).point);
        rigid.Activate();
        rigid.Initialize();
       


    }


}