using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class TrailRendererCleaner : MonoBehaviour
{
    TrailRenderer rend;
    private void Awake()
    {
        rend = GetComponent<TrailRenderer>();
        rend.emitting = false;
    }
    private void OnEnable()
    {
        rend.Clear();
        rend.emitting = true;
    }
}
