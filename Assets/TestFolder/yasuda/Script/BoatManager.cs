﻿using System.Collections;
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

	bool HaveTreasure =false;

	bool Outward = true;

	float Second = 0;
	float  Count;

	public struct BoatStr
	{
		public bool Outward1;
		public bool HaveTreashre1;
		public float Second1;
		public float Count1;

		public BoatStr (bool outward1, bool haveTreashre1, float second1, float count1)
		{
			Outward1 = outward1;
			HaveTreashre1 = haveTreashre1;
			Second1 = second1;
			Count1 = count1;
		}
	}

	// Use this for initialization
	void Start () {
		for(int i = 0; i < Boats.Length; i++){
			BoatsAlive[i] = true;
			BoatArray [i] = new BoatStr{ Outward1 = true, HaveTreashre1 = false, Second1 = 0, Count1 = 0 };
		}
	}
	
	// Update is called once per frame
	void Update () {
		Departure (0);
		TreasureGet (0);
	}

	// 右端から出発
	void Departure(int boatNum){
		if (BoatsAlive [boatNum]) {
			BoatMove move = Boats [boatNum].GetComponent<BoatMove> ();
			GameObject boat = Boats [boatNum];

			if (Outward) {			// 往路
				move.setXSpeed (StratSpeed);
			} else if (HaveTreasure) {		// 復路
				move.setXSpeed ((-StratSpeed) - (float)(Count * 0.005));
				Debug.Log ("Speed:" + move.xSpeed);
			} else {
				move.setXSpeed (0);
			}

			// 左端についた時の処理
			if (boat.transform.position.x < -3.7f) {
				Debug.Log ("stop");
				move.setXSpeed (0);
				Outward = false;
				// ボタンを押したら帰る
				if (Input.GetMouseButtonDown (0)) {
					boat.transform.position = new Vector3 (-3.6f, boat.transform.position.y, 0);
					boat.transform.Rotate (new Vector3 (0, 180, 0));
					Second = 0;
					HaveTreasure = true;
				}
			}		
	
			// 初期位置に戻る
			if (boat.transform.position.x >= 6) {
				boat.transform.Rotate (new Vector3 (0, 180, 0));
				boat.transform.position = new Vector3 (5, boat.transform.position.y, 0);
				HaveTreasure = false;
				Outward = true;
			}
		}
	}

	void setOutward(bool sOutward){
		Outward = sOutward;
	}

	// 財宝の取得及び速度設定
	void TreasureGet(int boatNum){
		GameObject boat = Boats [boatNum];

		if (!Outward && !HaveTreasure) {
			Second += Time.deltaTime;
			Count = (int)Second;
			Debug.Log ("Time:" + (int)Count);
		}
		// 6カウントすると初期位置に戻る
		if (Second >= 6f) {
			boat.transform.position = new Vector3 (5, boat.transform.position.y, 0);
			Second = 0;
			HaveTreasure = false;
			Outward = true;
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
}