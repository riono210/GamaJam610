using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Score
{
    public int treasurePoint;
    public int aliveBoat;
}

[DisallowMultipleComponent]
public class GameMgr : SingletonMonoBehaviour<GameMgr> {

    public Score _score;
    

	void Awake()
	{
		//このクラスのinstanceが2つ存在する場合、新しく作られたほうをDestroyする
		if (Instance != null && Instance != this)
		{
			Debug.Log("Destroy SingletonInstance");
			Destroy(this.gameObject);
			return;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	// Use this for initialization
	void Start () {

	}

	
}
