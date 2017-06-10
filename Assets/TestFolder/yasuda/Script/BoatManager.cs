using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatManager : MonoBehaviour {

	// 初期速度
 	float StratSpeed = -0.05f;

	// ボートの配列
	public GameObject[] Boats;

	bool HaveTreasure;

	bool Outward = true;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Departure (0);
	}

	// 右端から出発
	void Departure(int boatNum){
		BoatMove move = Boats [boatNum].GetComponent<BoatMove> ();
		GameObject boat = Boats [boatNum];

		if (Outward) {			// 往路
			move.setXSpeed (StratSpeed);
		} else if (HaveTreasure) {		// 復路
			move.setXSpeed (-StratSpeed);
		} else {
			move.setXSpeed (0);
		}

		if (boat.transform.position.x < -3.7f) {
			Debug.Log ("stop");
			move.setXSpeed (0);
			Outward = false;
			if (Input.GetMouseButtonDown (0)) {
				boat.transform.position = new Vector3 (-3.6f, boat.transform.position.y, 0);
				boat.transform.Rotate (new Vector3 (0, 180, 0));
				HaveTreasure = true;
			}
		}		
	
		if (boat.transform.position.x >= 6) {
			boat.transform.Rotate (new Vector3 (0, 180, 0));
			boat.transform.position = new Vector3 (5, boat.transform.position.y, 0);
			HaveTreasure = false;
			Outward = true;
		}
	}

	void setOutward(bool sOutward){
		Outward = sOutward;
	}
}