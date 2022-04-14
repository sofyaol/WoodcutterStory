using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

[SelectionBase]
[RequireComponent(typeof(BoxCollider))]
public class GroundCube : MonoBehaviour
{ 
    [SerializeField] private int _price;
    [SerializeField] private ResourceType _resourceType;
    public ResourceType ResourceType => _resourceType;
    public int Price => _price; 
    [SerializeField] private GroundCube _leftNeighbor;
  [SerializeField] private GroundCube _rightNeighbor;
  [SerializeField] private GroundCube _backNeighbor;
  [SerializeField] private GroundCube _forwardNeighbor;
  [SerializeField] private GroundCanvas _canvas;
  [SerializeField] private GroundMesh _groundMesh;
  private BoxCollider _collider;
  private DecorationsParent _decorationsParent;

  float _rayDistance = 5f;
  
  void Awake()
    {
        foreach (var child in GetComponentsInChildren<GroundSide>())
        {
            switch (child.GroundSideType) // 1. иниц поля GroundSide
            {
                case GroundSideType.Forward:
                    child.NeighbourGroundCube = _forwardNeighbor;
                    break;

                case GroundSideType.Back:
                    child.NeighbourGroundCube = _backNeighbor;
                    break;

                case GroundSideType.Left:
                    child.NeighbourGroundCube = _leftNeighbor;
                    break;

                case GroundSideType.Right:
                    child.NeighbourGroundCube = _rightNeighbor;
                    break;
            }
        }

        _groundMesh = GetComponentInChildren<GroundMesh>();
         _canvas = GetComponentInChildren<GroundCanvas>();
         _collider = GetComponent<BoxCollider>();
         _decorationsParent = GetComponentInChildren<DecorationsParent>();
    }

    void Start()
    {
        if (CompareTag("Hidden"))
        {
            _groundMesh.MeshRenderer.enabled = false;
            _collider.enabled = false;
            _decorationsParent.gameObject.SetActive(false);
        }
        _canvas?.SetPrice(Price);
        _canvas?.SetResource(ResourceType);
        _canvas?.gameObject.SetActive(false);
    }

    private void TryBuyGround() 
    {                  
        if (Player.Instance.Coin >= Price)
        {
            _canvas.SetGreenBackground();
            StartCoroutine("Selling");
        }

        else
        {
            _canvas.SetRedBackground();
        }
        
    }

    public void FindNeighbors()
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
            UnityEditor.EditorUtility.SetDirty(neighbor); // for saving changes we made with UnityEditor
        }
        else
        {
            neighbor = null;
        }
    }

    public void SaleOfGroundSetActive(bool active)
    {
        _canvas.gameObject.SetActive(active);
        _groundMesh.MeshRenderer.enabled = active;
        
        if (!active)
        {
            _groundMesh.SetOriginMaterial();
            return;
        }
        
        _groundMesh.SetFadeMaterial();
        TryBuyGround();
    }

    private IEnumerator Selling()
    {
        yield return new WaitForSeconds(1.5f);
        while (true)
        {
            Player.Instance.Coin--;
            _price--;
            _canvas.SetPrice(_price);

            if (_price == 0)
            {
                MakeGroundNotHidden();
                yield break;
            }

            yield return new WaitForSeconds(0.1f); // сделать Data с временем 
        }
    }

    private void MakeGroundNotHidden()
    {
        _canvas.gameObject.SetActive(false);
        tag = "Untagged";
        foreach (var myGroundSide in GetComponentsInChildren<GroundSide>())
        {
            if (myGroundSide.NeighbourGroundCube != null)
            {
                myGroundSide.DeleteUselessWall();

                foreach (var neighborGroundSide in
                    myGroundSide.NeighbourGroundCube.GetComponentsInChildren<GroundSide>())
                {
                    neighborGroundSide.DeleteUselessWall();
                }
            }
        }

        _collider.enabled = true;
        _groundMesh.SetOriginMaterial();
        _decorationsParent.gameObject.SetActive(true);

    }

    public void StopSelling()
    {
        StopCoroutine("Selling");
    }
}
