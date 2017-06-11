using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatManager : MonoBehaviour {

	// 初期速度
 	const float StratSpeed = -0.05f;

	// ボートの配列
	const int BOATS_NUM = 5;
	public GameObject[] Boats = new GameObject[BOATS_NUM];
	// ボートの生存
	bool[] BoatsAlive = new bool[BOATS_NUM];
	// ボートの詳細の構造体配列
	BoatStr[] BoatArray = new BoatStr[BOATS_NUM];

//	bool HaveTreasure =false;
//
//	bool Outward = true;
//
//	float Second = 0;
//	float  Count;

	float GameTime = 0;

	public struct BoatStr
	{
		public bool Outward;
		public bool HaveTreashre;
		public float Second;
		public float Count;

		public BoatStr (bool outward, bool haveTreashre, float second, float count)
		{
			Outward = outward;
			HaveTreashre = haveTreashre;
			Second = second;
			Count = count;
		}
	}

	// Use this for initialization
	void Start () {
		for(int i = 0; i < Boats.Length; i++){
			BoatsAlive[i] = true;
			BoatArray [i] = new BoatStr{ Outward = true, HaveTreashre = false, Second = 0, Count = 0 };
		}
	}
	
	// Update is called once per frame
	void Update () {
//		Departure (0);
//		TreasureGet (0);

		BoatControl ();
	}

	// 右端から出発
	void Departure(int boatNum){
		if (BoatsAlive [boatNum]) {
			BoatMove move = Boats [boatNum].GetComponent<BoatMove> ();
			GameObject boat = Boats [boatNum];

			if (BoatArray[boatNum].Outward) {			// 往路
				move.setXSpeed (StratSpeed);
			} else if (BoatArray[boatNum].HaveTreashre) {		// 復路
				move.setXSpeed ((-StratSpeed) - (float)(BoatArray[boatNum].Count * 0.005));
				Debug.Log ("Speed:" + move.xSpeed);
			} else {
				move.setXSpeed (0);
			}

			// 左端についた時の処理
			if (boat.transform.position.x < -3.7f) {
				Debug.Log ("stop");
				move.setXSpeed (0);
				BoatArray[boatNum].Outward = false;
				// ボタンを押したら帰る
				if (Input.GetMouseButtonDown (0)) {
					boat.transform.position = new Vector3 (-3.6f, boat.transform.position.y, 0);
					boat.transform.Rotate (new Vector3 (0, 180, 0));
					BoatArray[boatNum].Second = 0;
					BoatArray[boatNum].HaveTreashre = true;
				}
			}		
	
			// 初期位置に戻る
			if (boat.transform.position.x >= 6) {
				boat.transform.Rotate (new Vector3 (0, 180, 0));
				boat.transform.position = new Vector3 (5, boat.transform.position.y, 0);
				BoatArray[boatNum].HaveTreashre = false;
				BoatArray[boatNum].Outward = true;
				BoatsAlive[boatNum] = true;
			}
		}
	}

//	void setOutward(bool sOutward){
//		Outward = sOutward;
//	}

	// 財宝の取得及び速度設定
	void TreasureGet(int boatNum){
		GameObject boat = Boats [boatNum];

		if (!BoatArray[boatNum].Outward && !BoatArray[boatNum].HaveTreashre) {
			BoatArray[boatNum].Second += Time.deltaTime;
			BoatArray[boatNum].Count = (int)BoatArray[boatNum].Second;
			//Debug.Log ("Time:" + (int)Count);
		}
		// 6カウントすると初期位置に戻る
		if (BoatArray[boatNum].Second >= 6f) {
			boat.transform.position = new Vector3 (5, boat.transform.position.y, 0);
			BoatArray[boatNum].Second = 0;
			BoatArray[boatNum].HaveTreashre = false;
			BoatArray[boatNum].Outward = true;
			BoatsAlive [boatNum] = false;
		}
	}
		
	// サメに食べられた時の処理
	public void BoatDie(string boatName){
		for (int i = 0; i < Boats.Length; i++) {
			if (Boats [i].name == boatName) {
				//Die
				Destroy(Boats[i]);
			}
		}
	}

	void BoatControl(){
		int timeSplit;

		if (GameTime <= 120) {
			GameTime += Time.deltaTime;
			timeSplit = (int)GameTime / 24;
			Debug.Log ("Time:" + (int)GameTime);
			Debug.Log ("sprit:" + timeSplit);

			Departure (0);
			TreasureGet (0);

			if (timeSplit >= 1) {
				Departure (1);
				TreasureGet (1);
			}
			if (timeSplit >= 2) {
				Departure (2);
				TreasureGet (2);
			}
			if (timeSplit >= 3) {
				Departure (3);
				TreasureGet (3);
			}
			if (timeSplit >= 4) {
				Departure (4);
				TreasureGet (4);
			}

			if (GameTime == 120) {
			}
		}
	}
}