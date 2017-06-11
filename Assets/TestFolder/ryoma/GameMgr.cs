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

    BoatManager _boatMgr;

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

	public void GameEnd()
    {
        _boatMgr = FindObjectOfType<BoatManager>();

        //_score.treasurePoint = _boatMgr;
        //_score.aliveBoat = _boatMgr.;

        SceneManager.LoadScene("Result");
    }
}
