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
	public BoatStr[] BoatArray = new BoatStr[BOATS_NUM];
	// 出発できるボートの番号の配列
	int[] DepartureBoat= new int[BOATS_NUM];
	// 制限時間
	float GameTime = 0;

	int StageBoat = 0;

	int DereatNum;

	// DepartureBoatに追加する時の比較
	int Adder = 0,Change = 0 ;

	public struct BoatStr
	{
		public bool Outward;       // 往路
		public bool HaveTreashre;  // 復路
		public bool Go;            // 発射  
		public float Second;       // 秒
		public int Count;          // 財宝カウント

		public BoatStr (bool outward, bool haveTreashre, bool go, float second, int count)
		{
			Outward = outward;
			HaveTreashre = haveTreashre;
			Go = go;
			Second = second;
			Count = count;
		}
	}

	// Use this for initialization
	void Start () {
		DepartureBoat [0] = 1;
		for(int i = 0; i < Boats.Length; i++){
			BoatsAlive[i] = true;
			BoatArray [i] = new BoatStr{ Outward = true, HaveTreashre = false,Go = false, Second = 0, Count = 0 };
		}
	}
	
	// Update is called once per frame
	void Update () {
		BoatControl ();

		Debug.Log ("stage:" + StageBoat);
	}

	// 右端から出発
	public void Departure(int boatNum){

		if (BoatsAlive [boatNum]) {
			BoatMove move = Boats [boatNum].GetComponent<BoatMove> ();
			GameObject boat = Boats [boatNum];

			GameObject mouseCheck;

			if (BoatArray[boatNum].Outward) {			// 往路
				move.setXSpeed (StratSpeed);
				if (!BoatArray [boatNum].Go) {
					BoatArray [boatNum].Go = true;
					StageBoat += 1;
				}
			} else if (BoatArray[boatNum].HaveTreashre) {		// 復路
				move.setXSpeed ((-StratSpeed) - (float)(BoatArray[boatNum].Count * 0.005));
				//Debug.Log ("Speed:" + move.xSpeed);
			} else {
				move.setXSpeed (0);
			}

			// 左端についた時の処理
			if (boat.transform.position.x < -3.7f) {
				//Debug.Log ("stop");
				move.setXSpeed (0);
				BoatArray[boatNum].Outward = false;
				// 船をクリックしたら帰る
				mouseCheck =  getClickObject();
				if (mouseCheck != null && mouseCheck == boat) {
					boat.transform.position = new Vector3 (-3.6f, boat.transform.position.y, 0);
					boat.transform.Rotate (new Vector3 (0, 180, 0));
					BoatArray [boatNum].Second = 0;
					BoatArray [boatNum].HaveTreashre = true;
				}
			}		
	
			// 初期位置に戻る
			if (boat.transform.position.x >= 6) {
				boat.transform.Rotate (new Vector3 (0, 180, 0));
				boat.transform.position = new Vector3 (5, boat.transform.position.y, 0);
				BoatArray[boatNum].HaveTreashre = false;
				BoatArray[boatNum].Outward = true;
				BoatArray [boatNum].Go = false;
				BoatsAlive[boatNum] = true;
				StageBoat -= 1;
			}
		}
	}

	// 財宝の取得及び速度設定
	void TreasureGet(int boatNum){
		GameObject boat = Boats [boatNum];

		if (!BoatArray[boatNum].Outward && !BoatArray[boatNum].HaveTreashre) {
			BoatArray[boatNum].Second += Time.deltaTime;
			BoatArray[boatNum].Count = (int)BoatArray[boatNum].Second;
			//Debug.Log ("Count:" + BoatArray[boatNum].Count);
		}
		// 6カウントすると初期位置に戻る(死亡)
		if (BoatArray[boatNum].Second >= 6f) {
			boat.transform.position = new Vector3 (5, boat.transform.position.y, 0);
			BoatArray[boatNum].Second = 0;
			BoatArray[boatNum].HaveTreashre = false;
			BoatArray[boatNum].Outward = true;
			BoatsAlive [boatNum] = false;
			DepartureBoat [boatNum] = 0;
			StageBoat -= 1;
			DereatNum = boatNum;
		}
	}
		
	// サメに食べられた時の処理
	public void BoatDie(string boatName){
		for (int i = 0; i < Boats.Length; i++) {
			if (Boats [i].name == boatName && BoatsAlive[i]) {
				//Die
				Boats[i].GetComponent<SpriteRenderer>().enabled = false;
				enabled = false;

				BoatsAlive [i] = false;
				DepartureBoat [i] = 0;
				StageBoat -= 1;
				DereatNum = i;
			}
		}
	}

	void BoatControl (){
		// 経過時間を24で割ったもの
		float timeSplit;


		if (GameTime <= 120) {
			GameTime += Time.deltaTime;
			timeSplit = GameTime / 24;
			Adder = (int)timeSplit;
			//Debug.Log ("Time:" + (int)GameTime);
			//Debug.Log ("add:" + Adder);

			// 比較して違っていたら配列に追加
			if (Change != Adder || (Change == 0 && Adder == 0)) {
				DepartureBoat [Adder] = Adder;
			}
		}
		Change = Adder;
	
		for (int i = 0; i <= Adder; i++) {
			Departure (i);
			TreasureGet (i);
		}
			
		// 終了時
		if (GameTime == 120) {
			// しょりを書く
		}
	}
	

	public int getBoatAlive(){
		int remain = 0;
		for (int i = 0; i < 5; i++) {
			if (BoatsAlive [i] == true) {
				remain++;
			}
		}
		return remain;
	}

	// 左クリックしたオブジェクトを取得する関数(2D)
	private GameObject getClickObject() {
		GameObject result = null;
		// 左クリックされた場所のオブジェクトを取得
		if (Input.GetMouseButtonDown (0)) {
			Vector2 tapPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Collider2D collition2d = Physics2D.OverlapPoint (tapPoint);
			if (collition2d) {
				result = collition2d.transform.gameObject;
			}
		}
		return result;
	}

	void AddBoat(float startTime){
		float endTime = GameTime;
		int[] addArray;

		if (StageBoat == 0 && (endTime - startTime) >= 0.1f) {
			for (int i = 0; i > DepartureBoat.Length; i++) {
				if (DepartureBoat [i] == DereatNum) {
					addArray = new int[DereatNum - 1];

				}


			}

		}
	}
}