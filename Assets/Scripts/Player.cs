using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private CharacterController _charactercontroller;
    private NavMeshAgent _agent;
    private AudioSource _audiosource;

    [SerializeField]
    private GameObject _muzzelFlash;

    [SerializeField]
    private GameObject _hitMarkerEffect;

    [SerializeField]
    private float _gravity = 1f;

    [SerializeField]
    private float _jumpHeight = 15f;

    [SerializeField]
    private float _speed = 3f;


    [SerializeField]
    private int _currentAmmo;
    private int _maxAmmo = 50;
   
    private float _fireRate = 0.1f;
    private float _canfire;
    private float _reloading = 1.5f;
    private float _noShooting;
    
    void Start()
    {       
        _charactercontroller = GetComponent<CharacterController>();
        _agent = GetComponent<NavMeshAgent>();
        _audiosource = GetComponent<AudioSource>();
        _muzzelFlash.SetActive(false);
        _currentAmmo = _maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
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


        CalculateMovement();

    }

    private void WeaponReloading()
    {
        _currentAmmo = _maxAmmo;
    }

    private void CalculateMovement()
    {
        float moveSide = Input.GetAxis("Horizontal");
        float moveForward = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(moveSide, 0, moveForward);
        Vector3 velocity = _speed * direction;

        velocity.y -= _gravity;

        velocity = transform.transform.TransformDirection(velocity); //here we Converting our local coordinates into world Space.
        
        //_charactercontroller.Move(velocity * Time.deltaTime);
        _agent.Move(velocity * Time.deltaTime);
    }


    private void Shooting()
    {
        //we casted a ray
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;
        _currentAmmo--;

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
            Destroy(_hit, 1f);
        }
    }
    

}
