using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Score
{
    public float treasurePoint;
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

        
        float totalPoint = 0;
        for (int i = 0; i < _boatMgr.BoatArray.Length; i++)
        {
            totalPoint += _boatMgr.BoatArray[i].Count;
        }

        _score.treasurePoint = totalPoint;
        _score.aliveBoat = _boatMgr.getBoatAlive();


        FindObjectOfType<FadeCtrl.FadeController>().StartFadeOut("Result");
        //SceneManager.LoadScene("Result");
    }
}
