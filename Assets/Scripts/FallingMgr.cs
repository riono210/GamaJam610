using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingMgr : MonoBehaviour
{
    public Timer _time;

    const int FALLPOS_NUM = 3; 
    public Transform[] fallPos = new Transform[FALLPOS_NUM];
    float  createMargin = 1f;    
    public enum FallObj
    {
        LargeFish,
        MiddleFish,
        SmallFish,
        Dust,
        Mushroom,
        Obj_Num,
    }
    public GameObject[] prefabs = new GameObject[(int)FallObj.Obj_Num];


    public SharkControll[] sharkCS = new SharkControll[FALLPOS_NUM];

    void Start ()
    {
        StartCoroutine(CoGeneratFallingObject());


        for (int i = 0; i < FALLPOS_NUM; i++)
        {
            Vector3 newPos = fallPos[i].position;
            newPos.x = sharkCS[i].transform.position.x;
            fallPos[i].position = newPos;

        }
	}
	
	void Update ()
    {
       
	}

    IEnumerator CoGeneratFallingObject ()
    {
        while (true)
        {
            float progress = (Timer.LIMIT_TIME - _time.remainTime)/Timer.LIMIT_TIME*0.3f;

            yield return new WaitForSeconds(createMargin - progress);
            int posIndex = Random.Range(0, FALLPOS_NUM);
            Instantiate(prefabs[Random.Range(0, (int)FallObj.Obj_Num)], fallPos[posIndex].position, Quaternion.identity, null);
        }

        yield return null;
        
    }


}
