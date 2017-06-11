using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AliveBoatPoint : MonoBehaviour {
	public Text _text;
	// Use this for initialization
	void Start () {
		_text = GetComponent<Text> ();
		_text.text = (1f + GameMgr.Instance._score.aliveBoat * 0.1f).ToString();
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
