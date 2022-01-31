using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Joystick _joystick;
   // private readonly Vector3 _checkGroundOffset = new Vector3(0, 0, -1);

  
    private float _movementSpeed = 0;
    [Header("Movement settings")]
    [SerializeField] private float _stepSpeed;
    [SerializeField] private float _runSpeed;
    [Space]
    [SerializeField] private float _timeOfSpeedIncrease = 4f;
    
    private float _timer = 0f;
    
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");

    private Vector3 _oldTransform;

    void Start()
    {
        _movementSpeed = _stepSpeed;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            _animator.SetBool(IsMoving, true);
            Vector3 offset = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
            Vector3 direction = transform.position - _oldTransform; // - offset
            _rigidbody.MovePosition(transform.position + offset * (_movementSpeed * Time.deltaTime));

            //transform.LookAt(direction);
            
            float rotationY = Quaternion.LookRotation(direction).eulerAngles.y;

            Quaternion LookAtRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rotationY, transform.rotation.eulerAngles.z);
            transform.rotation = LookAtRotation;
            _oldTransform = transform.position;
            
            SpeedIncrease();
        }

        else
        {
            _animator.SetBool(IsMoving, false);
            SpeedDecrease();
        }
        
    //    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down +_checkGroundOffset));
    }

    private void SpeedDecrease()
    {
        _movementSpeed = _stepSpeed;
        _timer = 0;
    }

    private void SpeedIncrease()
    {
        _timer += Time.deltaTime;
        if (_timer >= _timeOfSpeedIncrease && _movementSpeed != _runSpeed)
        {
            _movementSpeed = _runSpeed;
            _animator.SetTrigger(IsRunning);
        }
    }

   /* private bool CheckGround()
    {
        RaycastHit hit;
        
       if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down +_checkGroundOffset), out hit))
       {
           if (hit.transform.gameObject.layer == 6)
           {
               return true;
           }
       }
       return false;
    }*/
}