using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(GroundCube))]
public class GroundCubeEditorComponent : MonoBehaviour
{
   [SerializeField] GroundCube _groundCube;

   private Vector3[] _rayPoints = new Vector3[121];
   private float _rayDistance = 5f;
// Bit shift the index of the layer (6) to get a bit mask
   int _treeLayerMask = 1 << 8;
   private int _plantsLayerMask = 1 << 10;
   internal void MakeDecorationsChildren()
   {
      var y = 0;
      var x = -3.5f;
      var z = -3.5f;
      
      for (int i = 0, j = 0; i < 121; i++ , j++)
      {
         _rayPoints[i] = new Vector3(x, y, z);
         z += 0.7f;
         if (j == 11)
         {
            x += 0.7f;
            z = -3.5f;
            j = 0;
         }
      }
      var _decorationsParent = GetComponentInChildren<DecorationsParent>().transform;

      foreach (var point in _rayPoints)
      {
         if(Physics.Raycast(transform.position + point, transform.up * _rayDistance, out RaycastHit raycastHit, _rayDistance, _treeLayerMask | _plantsLayerMask))
          {
             var decoration = raycastHit.collider.transform;
             decoration.SetParent(_decorationsParent, true);
          }
      }
   }

 
}
