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
    
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private Vector3 _oldTransform;
    
    [Header("Movement settings")]
    [SerializeField] private float _runSpeed;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            _animator.SetBool(IsRunning, true);
            Vector3 offset = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
            Vector3 direction = transform.position - _oldTransform; // - offset
            _rigidbody.MovePosition(transform.position + offset * (_runSpeed * Time.deltaTime));

            //transform.LookAt(direction);
            
            float rotationY = Quaternion.LookRotation(direction).eulerAngles.y;

            Quaternion LookAtRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rotationY, transform.rotation.eulerAngles.z);
            transform.rotation = LookAtRotation;
            _oldTransform = transform.position;
        }

        else
        {
            _animator.SetBool(IsRunning, false);
        }
    }
}