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
	int[] DepartureBoat= new int[BOATS_NUM-1] ;
	// 制限時間
	float GameTime = 0;

	int StageBoat = 0;

	// DepartureBoatに追加する時の比較
	int Adder = 0,Change = 0 ;

	public struct BoatStr
	{
		public bool Outward;       // 往路
		public bool HaveTreashre;  // 復路
		public bool Go;             // 発射  
		public float Second;       // 秒
		public float Count;        // 財宝カウント

		public BoatStr (bool outward, bool haveTreashre, bool go, float second, float count)
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
		for(int i = 0; i < Boats.Length; i++){
			BoatsAlive[i] = true;
			BoatArray [i] = new BoatStr{ Outward = true, HaveTreashre = false,Go = true, Second = 0, Count = 0 };
		}
	}
	
	// Update is called once per frame
	void Update () {
		BoatControl ();
	}

	// 右端から出発
	public void Departure(int boatNum){

		if (BoatsAlive [boatNum]) {
			BoatMove move = Boats [boatNum].GetComponent<BoatMove> ();
			GameObject boat = Boats [boatNum];

			GameObject mouseCheck;

			if (BoatArray[boatNum].Outward) {			// 往路
				move.setXSpeed (StratSpeed);
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
				BoatsAlive[boatNum] = true;
			}
		}
	}

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
			DepartureBoat [boatNum] = 0;
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
			Debug.Log ("Time:" + (int)GameTime);
			Debug.Log ("add:" + Adder);

			Departure (0);
			TreasureGet (0);

			if (Change != Adder) {
				DepartureBoat [Adder - 1] = Adder;
				for (int i = 0; i < 5; i++) {
					//Debug.Log ("list:" + DepartureBoat[i]);
				}
			}
		}
		Change = Adder;
	
		foreach (int num in DepartureBoat) {
			Departure (num);
			TreasureGet (num);
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
}