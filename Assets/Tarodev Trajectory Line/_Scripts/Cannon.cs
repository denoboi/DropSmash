using UnityEngine;

public class Cannon : MonoBehaviour {
    [SerializeField] private Projection _projection;

    private Ball spawned;

    //rotate'i basta kapatmak icin cagiriyorum.
   


    private void Awake()
    {
        CreateHammer();        
    }


    private void Update() {
        HandleControls();
        _projection.SimulateTrajectory(_ballPrefab, _ballSpawn.position, _ballSpawn.forward * _force);
    }

    #region Handle Controls

    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private float _force = 20;
    [SerializeField] private Transform _ballSpawn;
    [SerializeField] private Transform _barrelPivot;
    [SerializeField] private float _rotateSpeed = 30;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private Transform _leftWheel, _rightWheel;
    [SerializeField] private ParticleSystem _launchParticles;
   


    
    /// <summary>
    /// This is absolute spaghetti and should not be look upon for inspiration. I quickly smashed this together
    /// for the tutorial and didn't look back
    /// </summary>
    private void HandleControls() {

        
        //if (Input.GetMouseButton(0)) _barrelPivot.Rotate(Vector3.right * _rotateSpeed * Time.deltaTime);
        //else if (Input.GetKey(KeyCode.W)) _barrelPivot.Rotate(Vector3.left * _rotateSpeed * Time.deltaTime);

        //if (Input.GetKey(KeyCode.A)) {
        //    transform.Rotate(Vector3.down * _rotateSpeed * Time.deltaTime);
        //    _leftWheel.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
        //    _rightWheel.Rotate(Vector3.back * _rotateSpeed * Time.deltaTime);
        //}
        //else if (Input.GetKey(KeyCode.D)) {
        //    transform.Rotate(Vector3.up * _rotateSpeed * Time.deltaTime);
        //    _leftWheel.Rotate(Vector3.back * _rotateSpeed * 1.5f * Time.deltaTime);
        //    _rightWheel.Rotate(Vector3.forward * _rotateSpeed * 1.5f * Time.deltaTime);
        //}


        //Instantiating and throwing ball
        if (Input.GetMouseButtonUp(0)) {

            //isKinematic ve rotate objeyi aciyoruz.
            spawned.GetComponent<Rigidbody>().isKinematic = false;
            spawned.GetComponent<RotateObject>().enabled = true;

            //we have 2 colliders 
            foreach (var collider in spawned.GetComponentsInChildren<Collider>())
            {
                collider.isTrigger = false;
            }
            spawned.Init(_ballSpawn.forward * _force, false);
            _launchParticles.Play();
            _source.PlayOneShot(_clip);

            //bunu sonradan cagiriyoruz ikinci hammer'a gectiginde
            CreateHammer();
        }
    }

    //Awake'de Instantiate ediyoruz isKinematic acik. Yoksa basta yere dusuyor.
    void CreateHammer()
    {
        spawned = Instantiate(_ballPrefab, _ballSpawn.position, _ballSpawn.rotation);
        foreach (var collider in spawned.GetComponentsInChildren<Collider>())
        {
            collider.isTrigger = true;
        }
        spawned.GetComponent<Rigidbody>().isKinematic = true;
        spawned.GetComponent<RotateObject>().enabled = false;
    }

    #endregion
}