﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    Text Text;

    public const float LIMIT_TIME = 120f;
    public float  remainTime = LIMIT_TIME;
	// Use this for initialization
	void Start () {
     Text = GetComponentInChildren<Text>();
    }
	
	// Update is called once per frame
	void Update () {
     remainTime -= Time.deltaTime;
     GetComponent<Text>().text = remainTime.ToString("F2");

        if (remainTime <= 0)
        {
            //終わった時の処理
        }

    }
}

