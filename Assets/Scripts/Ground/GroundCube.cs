using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCube : MonoBehaviour
{ 
   private GroundCube _leftNeighbor;
   private GroundCube _rightNeighbor;
   private GroundCube _backNeighbor;
   private GroundCube _forwardNeighbor;

   [SerializeField] private BoxCollider _leftBoxCollider;
   [SerializeField] private BoxCollider _rightBoxCollider;
   [SerializeField] private BoxCollider _backBoxCollider;
   [SerializeField] private BoxCollider _forwardBoxCollider;

   float _rayDistance = 7f;

    void Awake()
    {
        
    }

    
    
    
    internal void FindNeighbors()
    {
        FindNeighborOf(transform.forward, out _forwardNeighbor);
        FindNeighborOf((transform.forward * -1), out _backNeighbor);
        FindNeighborOf(transform.right, out _rightNeighbor);
        FindNeighborOf((transform.right * -1), out _leftNeighbor);
    }

    private void FindNeighborOf(Vector3 direction, out GroundCube neighbor)
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position, direction, out raycastHit, _rayDistance))
        {
           neighbor = raycastHit.collider.gameObject.GetComponent<GroundCube>();
        }
        else
        {
            neighbor = null;
        }
    }
}
