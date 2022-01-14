using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HiddenGroundContainer))]
public class HiddenGroundContainerEditor : Editor
{
   private HiddenGroundContainer _hgContainer;
   public void OnEnable()
   {
      _hgContainer = (HiddenGroundContainer)target;
   }

   public override void OnInspectorGUI()
   {
      if (GUILayout.Button("Make Children's Tag Hidden"))
      {
         _hgContainer.MakeChildrenTagHidden();
      }
      
      if (GUILayout.Button("Make Children Not Active"))
      {
         _hgContainer.MakeChildrenNotActive();
      }
      
      if (GUILayout.Button("Make Children Active"))
      {
         _hgContainer.MakeChildrenActive();
      }
   }
}
