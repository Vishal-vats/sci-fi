using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private CharacterController _charactercontroller;
    private NavMeshAgent _agent;

    [SerializeField]
    private float _gravity = 1f;

    [SerializeField]
    private float _jumpHeight = 15f;

    [SerializeField]
    private float _speed = 2f;

    private float _tempVelocity = 0f;


    void Start()
    {
        _charactercontroller = GetComponent<CharacterController>();
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

    }

    private void CalculateMovement()
    {
        float moveSide = Input.GetAxis("Horizontal");
        float moveForward = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(moveSide, 0, moveForward);
        Vector3 velocity = _speed * direction;

        if (_agent.isOnNavMesh == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _tempVelocity = _jumpHeight;
            }
        }
        else
        {
            _tempVelocity -= _gravity;
        }

        velocity.y = _tempVelocity;

        velocity = transform.transform.TransformDirection(velocity); //here we Converting our local coordinates into world Space.
        //transform.Translate(velocity * Time.deltaTime);
        //_charactercontroller.Move(velocity * Time.deltaTime);
        _agent.Move(velocity * Time.deltaTime);
    }
}
