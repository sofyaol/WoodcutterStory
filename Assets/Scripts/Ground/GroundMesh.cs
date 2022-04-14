using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMesh : MonoBehaviour
{
   [SerializeField] private Material _origin;
   [SerializeField] private Material _fade;
   private MeshRenderer _meshRenderer;
   internal MeshRenderer MeshRenderer
   {
      get => _meshRenderer;
      set => _meshRenderer = value;
   }

   void Awake()
   {
      _meshRenderer = GetComponent<MeshRenderer>();
   }

   public void SetOriginMaterial()
   {
      MeshRenderer.material = _origin;
   }
   
   public void SetFadeMaterial()
   {
      MeshRenderer.material = _fade;
   }
}
