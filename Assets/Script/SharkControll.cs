using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkControll : MonoBehaviour {

	GameObject Shark;

	public enum SharkStats{
		Wait,
		Eatable,
		Eating_ship,
		Eating_fish,
		Eating_dust,
		Eating_mushroom

	}

	public SharkStats _sharkStats;
	public float _hungryNum;
	public float _hunglyspeed = 0.1f;
	public float _eatableNum = 0.5f;

	// Use this for initialization
	void Start () {
		_sharkStats = SharkStats.Wait;
		_hungryNum = Random.value * 0.5f;

	}

	
	// Update is called once per frame
	void Update () {
		
		_hungryNum -= Time.deltaTime * _hunglyspeed;

		//ステータスの評価
		if (_sharkStats == SharkStats.Wait) {
			if (_hungryNum < _eatableNum) {
				_sharkStats = SharkStats.Eatable;

			}

		}

	}
	void OnTriggerEnter(Collider other) {
		Debug.Log(other.name);
	}

}
