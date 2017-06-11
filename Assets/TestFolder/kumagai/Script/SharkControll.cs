using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SharkControll : MonoBehaviour {
    public BoatManager _boatMgr;

    public const string FISH_L = "FISH_L";
    public const string FISH_M = "FISH_M";
    public const string FISH_S = "FISH_S";
    public const string DUST = "DUST";
    public const string MASH = "MASH";
    public const string SHIP = "Boat";

    const float RECOVERY_FISH_L = 0.40f;
    const float RECOVERY_FISH_M = 0.30f;
    const float RECOVERY_FISH_S = 0.20f;

    public enum SharkStats{
		Sleeping,
		Hungry,
		Eating_ship,
		Eating_fish,
		Eating_dust,
		Eating_mushroom
	}

	public SharkStats _sharkStats;
	float _hungryNum;
	float _hunglyspeed = 0.04f;
	float _hungryLine = 0.3f;

    [SerializeField]
    List<GameObject> EatList = new List<GameObject>();

    public Slider gauge;

    Animator anim;

    float _panicTimer;
    const float PANIC_TIME = 4f;

    float _eatFishTime;
    const float EAT_FISH_TIME = 0.5f;
    // Use this for initialization
    void Start () {
		_hungryNum = Random.value * 0.6f + 0.4f;

        _boatMgr = FindObjectOfType<BoatManager>();
        anim = GetComponent<Animator>();

    }

    
	
	// Update is called once per frame
	void Update () {
		
		_hungryNum -= Time.deltaTime * _hunglyspeed;
        if (_hungryNum < 0) _hungryNum = 0;
        if (_hungryNum > 1f) _hungryNum = 1f;
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

                EatingShip();


                break;

            case SharkStats.Eating_fish:
                _eatFishTime -= Time.deltaTime;
                if (_eatFishTime < 0)
                {
                    _eatFishTime = 0;
                    _sharkStats = SharkStats.Hungry;
                }
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
        //Debug.Log("collision"+otherCol.gameObject.name);


        switch (otherCol.gameObject.tag)
        {
            case FISH_L:
                _hungryNum += RECOVERY_FISH_L;
                Destroy(otherCol.gameObject);

                if (_sharkStats != SharkStats.Eating_mushroom) {
                    _eatFishTime = EAT_FISH_TIME;
                    _sharkStats = SharkStats.Eating_fish;
                }
                break;
            case FISH_M:
                _hungryNum += RECOVERY_FISH_M;
                Destroy(otherCol.gameObject);

                if (_sharkStats != SharkStats.Eating_mushroom)
                {
                    _eatFishTime = EAT_FISH_TIME;
                    _sharkStats = SharkStats.Eating_fish;
                }
                break;
            case FISH_S:
                _hungryNum += RECOVERY_FISH_S;
                Destroy(otherCol.gameObject);
                if (_sharkStats != SharkStats.Eating_mushroom)
                {
                    _eatFishTime = EAT_FISH_TIME;
                    _sharkStats = SharkStats.Eating_fish;
                }
                break;

            case DUST:
                if (_sharkStats != SharkStats.Eating_mushroom)
                {
                    _hungryNum -= RECOVERY_FISH_S;
                }
                Destroy(otherCol.gameObject);

                break;

            case MASH:
                _sharkStats = SharkStats.Eating_mushroom;
                _hungryNum += RECOVERY_FISH_M;
                _panicTimer = PANIC_TIME;
                Destroy(otherCol.gameObject);

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

                if (EatList.Contains(otherCol.gameObject))
                {
                    EatList.Remove(otherCol.gameObject);
                }
                
                break;

        }
    }

    void EatingShip()
    {
        if (_sharkStats == SharkStats.Hungry)
        {
            if (EatList.Count != 0) {
                GameObject tmp = new GameObject();
                foreach (GameObject gobj in EatList)
                {
                    if (_boatMgr.gameObject != null)
                    {
                        tmp = gobj;
                        _boatMgr.BoatDie(gobj.name);
                    }

                }
                if(tmp != null) EatList.Remove(tmp);
            }
        }
    }

    
    void OnTriggerEnter(Collider other) {
		Debug.Log("Trigger"+other.name);

        switch (other.tag)
        {
            /*
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
                */
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

                if (EatList.Contains(otherCol.gameObject))
                {
                    EatList.Remove(otherCol.gameObject);
                }
                
                break;

        }
    }

}
