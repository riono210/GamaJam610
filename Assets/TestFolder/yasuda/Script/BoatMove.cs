using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMove : MonoBehaviour {

	// x,y座標
	float ySin;
	public float xSpeed;

	// 振動数，振幅
	public float Frequency, Amplitude;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ySin = Mathf.Sin (Time.frameCount*Frequency) * Amplitude;
		//Debug.Log ("ysin" + ySin);

	}
	void FixedUpdate(){
		Move ();
	}

	// 移動
	void Move(){
		transform.position += new Vector3 (xSpeed,ySin,0);
	}

	public void setXSpeed(float speed){
		xSpeed = speed;
	}
}
