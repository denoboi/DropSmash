using UnityEngine;
using HCB.Core;
using System;
using UnityEngine.EventSystems;
using HCB.IncrimantalIdleSystem;

public class Cannon : StatObjectBase {
    [SerializeField] private Projection _projection;
    
    [SerializeField] private float _throwRate;

    //ilk basta beklemesin diye infinity
    private float _timer = Mathf.Infinity;

    private Ball spawned;

    //rotate'i basta kapatmak icin cagiriyorum.

    private void Awake()
    {
        CreateHammer();       
    }

   

    private void Update() {

        //Tiklanilan sey Ui objesi ise return atiyor.
        if (EventSystem.current == null) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;
        foreach (Touch touch in Input.touches)
        {
            int id = touch.fingerId;
            if (EventSystem.current.IsPointerOverGameObject(id))
            {
                return;
            }
        }

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

        


        //Instantiating and throwing ball
        if (Input.GetMouseButton(0)) {

            //HapticManager.Haptic(HapticTypes.RigidImpact);
            _timer += Time.deltaTime;
            if(_timer >= _throwRate)
            {
                ThrowHammer();
                _timer = 0;
            }
            

            
        }

        //elimizi cektigimizde hemen birakmasi icin. Input lag engelliyor
        else if(Input.GetMouseButtonUp(0))
        {
            _timer = _throwRate + 1;
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

        //meshrenderer'i kapatiyorum cunku ikinci bir sey atiyor bu spawn ettigimiz. Cannon ucundaki top. Fake ediyorum aslinda
        //(InChildren yapmak gerekiyor dikkat)
        spawned.GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    void ThrowHammer()
    {


        //isKinematic ve rotate objeyi aciyoruz.
        spawned.GetComponent<Rigidbody>().isKinematic = false;
        spawned.GetComponent<RotateObject>().enabled = true;

        spawned.GetComponentInChildren<MeshRenderer>().enabled = true;

        //we have 2 colliders 
        foreach (var collider in spawned.GetComponentsInChildren<Collider>())
        {
            collider.isTrigger = false;
        }
        spawned.Init(_ballSpawn.forward * _force, false);
        _launchParticles.Play();
        _source.PlayOneShot(_clip);

        //bunu sonradan cagiriyoruz ikinci hammer'a gectiginde.
        CreateHammer();
    }

    public override void UpdateStat(string id)
    {
        if (!string.Equals(StatData.IdleStatData.StatID, id))
            return;

        _throwRate = StatData.IdleStatData.InitialValue * StatData.IdleStatData.UpgradeMultiplier / StatData.Level;
    }

    #endregion
}