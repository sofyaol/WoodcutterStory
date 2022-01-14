using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class GroundSide : MonoBehaviour
{
   [SerializeField] private GroundSideType _groundSideType; 
   internal GroundSideType GroundSideType => _groundSideType;

      // public delegate void GroundSideHandler(GroundSideType groundSideType);
  // public event GroundSideHandler OnTriggerByPlayer;
   [SerializeField] private GroundCanvas _canvas;
   [SerializeField] private BoxCollider _wall;

   public GroundCube NeighbourGroundCube { get; set; }

   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.layer == 7) // 7 layer - player
      {
         if (NeighbourGroundCube.gameObject.CompareTag("Hidden"))
        {
           NeighbourGroundCube.SaleOfGroundSetActive(true);
        }
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.gameObject.layer == 7) // 7 layer - player
      {
         if (NeighbourGroundCube.gameObject.CompareTag("Hidden"))
         {
            NeighbourGroundCube.SaleOfGroundSetActive(false);
            NeighbourGroundCube.StopSelling();
         }
      }
   }
   

   private void Start()
   {
      if (NeighbourGroundCube == null || !NeighbourGroundCube.gameObject.CompareTag("Hidden"))
      {
         GetComponent<BoxCollider>().enabled = false;
         
      }

      DeleteUselessWall();
   }

   public void DeleteUselessWall()
   {
      if (NeighbourGroundCube != null && !NeighbourGroundCube.gameObject.CompareTag("Hidden"))
      {
         _wall.enabled = false;
      }
   }
}

