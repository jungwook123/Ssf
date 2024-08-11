using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageText : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    float counter;
    Vector2 dir;
    public void Set(string content, Color color, Vector2 dir)
    {
        text.text = content;
        text.color = color;
        text.color = new Color(color.r, color.g, color.b, 1);
        this.dir = dir;
        counter = duration;
    }
    const float duration = 0.5f, moveSpeed = 2.0f;
    private void Update()
    {
        transform.Translate(dir * (counter / duration) * Time.deltaTime * moveSpeed);
        if(counter > 0)
        {
            counter -= Time.deltaTime;
            if(counter/duration < 0.5f) text.color = new Color(text.color.r, text.color.g, text.color.b, counter / (duration/2.0f));
        }
        else
        {
            GameManager.Instance.UIs.Release(this);
        }
    }
}
