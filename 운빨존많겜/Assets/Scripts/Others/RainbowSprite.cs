using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RainbowSprite : MonoBehaviour
{
    [SerializeField] float rainbowSpeed = 1.0f;
    [SerializeField] float s = 1.0f, v = 1.0f;
    SpriteRenderer rend;
    float counter = 0.0f;
    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        counter += Time.deltaTime * rainbowSpeed;
        rend.color = Color.HSVToRGB(counter%1, s, v);
    }
}
