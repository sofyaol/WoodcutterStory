using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/List")]
public class ScriptableObjectList : ScriptableObject
{
    [SerializeField] private List<GameObject> _list;
    public List<GameObject> List => _list;
}
