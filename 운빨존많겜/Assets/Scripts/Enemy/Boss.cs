using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Enemy
{
    [SerializeField] float timeLimit = 60.0f;
    [SerializeField] Text timeLimitText;
    protected override void Update()
    {
        base.Update();
        timeLimit -= Time.deltaTime;
        if(timeLimit < 0)
        {
            GameManager.Instance.GameOver(false);
        }
        else
        {
            timeLimitText.text = $"{Mathf.FloorToInt(timeLimit / 60)}:{(Mathf.FloorToInt(timeLimit) > 9 ? Mathf.FloorToInt(timeLimit) / 10 : "0")}{Mathf.FloorToInt(timeLimit) % 10}";
        }
    }
}