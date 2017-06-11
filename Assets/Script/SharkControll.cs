using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SharkControll : MonoBehaviour {
    public BoatManager _boatMgr;

    const string FISH_L = "FISH_L";
    const string FISH_M = "FISH_M";
    const string FISH_S = "FISH_S";
    const string DUST = "DUST";
    const string MASH = "MASH";
    const string SHIP = "Boat";

    public enum SharkStats{
		Sleeping,
		Hungry,
		Eating_ship,
		Eating_fish,
		Eating_dust,
		Eating_mushroom
	}

	public SharkStats _sharkStats;
	public float _hungryNum;
	public float _hunglyspeed = 0.1f;
	public float _hungryLine = 0.5f;

    [SerializeField]
    List<GameObject> EatList = new List<GameObject>();

    public Slider gauge;

    Animator anim;

    float _panicTimer;
    const float PANIC_TIME = 4f;

    // Use this for initialization
    void Start () {
		_hungryNum = Random.value * 0.8f + 0.2f;

        _boatMgr = FindObjectOfType<BoatManager>();
        anim = GetComponent<Animator>();

    }

    
	
	// Update is called once per frame
	void Update () {
		
		_hungryNum -= Time.deltaTime * _hunglyspeed;
        if (_hungryNum < 0) _hungryNum = 0;

        gauge.value = _hungryNum;


        //ステータスの評価
        switch (_sharkStats)
        {
            case SharkStats.Sleeping:
                if (_hungryNum < _hungryLine)
                {
                    _sharkStats = SharkStats.Hungry;
                }
                break;

            case SharkStats.Hungry:
                if (_hungryNum >= _hungryLine)
                {
                    _sharkStats = SharkStats.Sleeping;
                }
                break;

            case SharkStats.Eating_fish:
                break;

            case SharkStats.Eating_dust:
                break;

            case SharkStats.Eating_mushroom:
                _panicTimer -= Time.deltaTime;
                if (_panicTimer < 0)
                {
                    _panicTimer = 0;
                    _sharkStats = SharkStats.Hungry;
                }
                break;

        }
        anim.SetInteger("status", (int)_sharkStats);
	}


    void OnCollisionEnter(Collision otherCol)
    {


        const float RECOVERY_FISH_L = 0.30f;
        const float RECOVERY_FISH_M = 0.2f;
        const float RECOVERY_FISH_S = 0.15f;
        switch (otherCol.gameObject.tag)
        {
            case FISH_L:
                _hungryNum += RECOVERY_FISH_L;
                break;
            case FISH_M:
                _hungryNum += RECOVERY_FISH_M;

                break;
            case FISH_S:
                _hungryNum += RECOVERY_FISH_S;

                break;

            case DUST:
                _hungryNum -= RECOVERY_FISH_S;
                break;

            case MASH:
                _sharkStats = SharkStats.Eating_mushroom;
                _panicTimer = PANIC_TIME;
                break;

            case SHIP:

                EatList.Add(otherCol.gameObject);
                if (_sharkStats == SharkStats.Hungry)
                {
                    //start_eat
                }
                break;

        }
    }

    void OnCollisionExit(Collision otherCol)
    {


        switch (otherCol.gameObject.tag)
        {
            case FISH_L:
                break;
            case FISH_M:

                break;
            case FISH_S:

                break;

            case DUST:
                break;

            case MASH:
                break;

            case SHIP:
                if (_sharkStats == SharkStats.Hungry)
                {
                    if (EatList.Contains(otherCol.gameObject))
                    {
                        EatList.Remove(otherCol.gameObject);
                    }
                }
                break;

        }
    }

    void EatingShip()
    {
        if(_sharkStats == SharkStats.Hungry)
        {
            foreach (GameObject gobj in EatList)
            {
                _boatMgr.BoatDie(gobj.name);
            }
        }
    }


    void OnTriggerEnter(Collider other) {
		Debug.Log(other.name);
        const float RECOVERY_FISH_L = 0.30f;
        const float RECOVERY_FISH_M = 0.2f;
        const float RECOVERY_FISH_S = 0.15f;
        switch (other.tag)
        {
            case FISH_L:
                _hungryNum += RECOVERY_FISH_L;
                break;
            case FISH_M:
                _hungryNum += RECOVERY_FISH_M;

                break;
            case FISH_S:
                _hungryNum += RECOVERY_FISH_S;

                break;

            case DUST:
                break;

            case MASH:
                break;

            case SHIP:

                if (!EatList.Contains(other.gameObject)) {
                    EatList.Add(other.gameObject);
                }
                if (_sharkStats == SharkStats.Hungry)
                {
                    //start_eat
                }
                break;

        }
    }

    void OnTriggerExit(Collider otherCol)
    {


        switch (otherCol.gameObject.tag)
        {
            case FISH_L:
                break;
            case FISH_M:

                break;
            case FISH_S:

                break;

            case DUST:
                break;

            case MASH:
                break;

            case SHIP:
                if (_sharkStats == SharkStats.Hungry)
                {
                    if (EatList.Contains(otherCol.gameObject))
                    {
                        EatList.Remove(otherCol.gameObject);
                    }
                }
                break;

        }
    }

}
