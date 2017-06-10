using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingMgr : MonoBehaviour
{

    const int FALLPOS_NUM = 3; 
    public Transform[] fallPos = new Transform[FALLPOS_NUM];
    float  createMargin = 0.4f;    
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


    void Start ()
    {
        StartCoroutine(CoGeneratFallingObject());

	}
	
	void Update ()
    {
       
	}

    IEnumerator CoGeneratFallingObject ()
    {
        while (true)
        {
            yield return new WaitForSeconds(createMargin);
            int posIndex = Random.Range(0, FALLPOS_NUM);
            Instantiate(prefabs[Random.Range(0, (int)FallObj.Obj_Num)], fallPos[posIndex].position, Quaternion.identity, null);
        }

        yield return null;
        
    }


}
