using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(KeyBoolBehaviour))]
public class BoolBehaviour_Editor : Editor
{
    public VisualTreeAsset treeAsset;
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement tmp = treeAsset.CloneTree();
        return tmp;
    }
}
#endif
