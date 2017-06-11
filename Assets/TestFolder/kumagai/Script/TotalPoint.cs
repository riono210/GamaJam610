using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TotalPoint : MonoBehaviour {
	public Text _text;
	// Use this for initialization
	void Start () {
		_text = GetComponent<Text> ();
		_text.text = (GameMgr.Instance._score.treasurePoint*(GameMgr.Instance._score.aliveBoat * 0.1f)).ToString();
	// Use this for initialization
	}

	// Update is called once per frame
	void Update () {

	}
}