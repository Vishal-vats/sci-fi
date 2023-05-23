using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private CharacterController _charactercontroller;
    private NavMeshAgent _agent;
    private AudioSource _audiosource;
    private UImanager _uiManager;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private GameObject _muzzelFlash;

    [SerializeField]
    private GameObject _hitMarkerEffect;

    [SerializeField]
    private GameObject _weapon;

    [SerializeField]
    private AudioClip _reloadingSound;

    [SerializeField]
    private float _gravity = 1f;

    //[SerializeField]
    //private float _jumpHeight = 15f;

    [SerializeField]
    private float _speed = 3f;


    [SerializeField]
    private int _currentAmmo;
    private int _maxMagazineAmmo = 30;
    [SerializeField]
    private int _maxAmmoCapacity = 240;
    private int _tempMaxAmmoCapacity;

    private float _fireRate = 0.1f;
    private float _canfire;
    public bool _isCoinCollected = false;
    private bool _isReloading = false;

    public bool _isWeaponCollected = false;

    /*
    one way to create reloading is below
    private float _reloading = 1.5f;
    private float _noShooting;
    */

    void Start()
    {
        _charactercontroller = GetComponent<CharacterController>();
        _agent = GetComponent<NavMeshAgent>();
        _audiosource = GetComponent<AudioSource>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UImanager>();
        _muzzelFlash.SetActive(false);
        _currentAmmo = _maxMagazineAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && _currentAmmo > 0 && _isReloading == false && _isWeaponCollected == true)
        {
            //apply FireRate system
            if (Time.time > _canfire)
            {
                Shooting();
                _canfire = Time.time + _fireRate;
            }

        }

        else
        {
            _muzzelFlash.SetActive(false);
            _audiosource.Stop();
        }


        //weapon Reloading
        if (Input.GetKeyDown(KeyCode.R) && (_currentAmmo == 0 || _currentAmmo < 30) && _maxAmmoCapacity > 0)
        {
            _isReloading = true;
            _audiosource.PlayOneShot(_reloadingSound, 1f); // it is not Working. However it works while in debug mode.
            AudioSource.PlayClipAtPoint(_reloadingSound, Camera.main.transform.position, 1f);
            StartCoroutine(ShootingReloading());
        }
        else if (_maxAmmoCapacity == 0)
        {
            Debug.Log("You Don't have enough Ammo");
        }

        CalculateMovement();

    }


    //waepon reloading (one way)
    IEnumerator ShootingReloading()
    {

        if (_maxAmmoCapacity > 0)
        {
            _tempMaxAmmoCapacity = _maxAmmoCapacity; //for storing maxAmmoCpacity value temprary.
        }
        _animator.SetTrigger("Reloading"); // play Reload Animation Clip
        yield return new WaitForSeconds(2f);
        ReloadingLogic();
        _isReloading = false;
    }

    private void ReloadingLogic()
    {
        _maxAmmoCapacity = _maxAmmoCapacity - (_maxMagazineAmmo - _currentAmmo);

        if (_maxAmmoCapacity < 0)
        {
            _currentAmmo = _tempMaxAmmoCapacity + _currentAmmo;
        }
        else
        {
            _currentAmmo = _maxMagazineAmmo;
        }

        _uiManager.currentAmmoUpdate(_currentAmmo);// updating _current Ammo Text

        if (_maxAmmoCapacity > 0)
        {
            _uiManager.maxAmmoCapacityUpdate(_maxAmmoCapacity); //updating _maxAmmoCapacity Ammo Text
        }
        else if (_maxAmmoCapacity < (_maxMagazineAmmo - _currentAmmo))
        {
            _uiManager.maxAmmoCapacityUpdate(0); //updating _maxAmmoCapacity Ammo Text
        }
    }


    private void CalculateMovement()
    {
        float moveSide = Input.GetAxis("Horizontal");
        float moveForward = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(moveSide, 0, moveForward);
        Vector3 velocity = _speed * direction;

        velocity.y -= _gravity;

        velocity = transform.transform.TransformDirection(velocity); //here we Converting our local coordinates into world Space.
        _charactercontroller.Move(velocity * Time.deltaTime);
        transform.position = _agent.nextPosition;
        //_agent.Move(velocity * Time.deltaTime);
    }


    private void Shooting()
    {
        //we casted a ray
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;
        _currentAmmo--;
        _uiManager.currentAmmoUpdate(_currentAmmo);

        _muzzelFlash.SetActive(true);

        if (_audiosource.isPlaying == false)
        {
            _audiosource.Play();
        }

        //now get output from this ray
        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            Debug.Log("We Hit: " + hitInfo.transform.name);
            GameObject _hit = Instantiate(_hitMarkerEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
            Destroy(_hit, 1.5f);

            if(hitInfo.transform.tag == "Crate")
            {
                Destructible crate = hitInfo.transform.GetComponent<Destructible>();
                if (crate != null)
                {
                    crate.destroyCrate();
                }
            }
        }
    }


    public void enableWeapon()
    {
        _weapon.SetActive(true);
        _isWeaponCollected = true;
    }


    /*
      another way to create reloading system

     if (Input.GetMouseButton(0) && _currentAmmo > 0 && Time.time > _noShooting)
        {
            //apply FireRate system
            if (Time.time > _canfire)
            {
                Shooting();
                _canfire = Time.time + _fireRate;
            }

        }

        else
        {
            _muzzelFlash.SetActive(false);
            _audiosource.Stop();
        }
        
        
        //weapon Reloading
        if(Input.GetKeyDown(KeyCode.R) && (_currentAmmo == 0 || _currentAmmo < 50))
        {

            _noShooting = Time.time + _reloading;
            Invoke("WeaponReloading", 1.5f);

        }

        //Weapon Reloading (other way)
        private void WeaponReloading()
        {
            _currentAmmo = _maxAmmo;
        }



    */

}
