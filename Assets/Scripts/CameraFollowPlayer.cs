using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private GameObject _player;
    private Vector3 offset;
    
    void Start()
    {
        _player = GameObject.FindObjectOfType<PlayerMovement>().gameObject;
        offset = transform.position - _player.transform.position;
    }

    void FixedUpdate()
    {
        transform.position = _player.transform.position + offset;
    }
}
