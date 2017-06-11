using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdsManager : MonoBehaviour {

    float speed = 0.1f;
    float counttime;

    const int BIRDS_MAX = 3;

    public Transform[] _birdsPos = new Transform[BIRDS_MAX];
    public bool[] enable = new bool[BIRDS_MAX];
    public bool[] waitFlg = new bool[BIRDS_MAX];
    public bool[] upFlg = new bool[BIRDS_MAX];
    // Use this for initialization
    void Start () {

        for(int i = 0; i < BIRDS_MAX; i++)
        {
            enable[i] = false;
            waitFlg[i] = false;
            upFlg[i] = false;
        }
	}

    // Update is called once per frame
    void Update()
    {

        counttime += Time.deltaTime;
        if (counttime > 1f)
        {
            counttime = 0;

            int index = Random.Range(0, 3);
            enable[index] = true;
                
        }

        for(int z = 0; z < BIRDS_MAX; z++)
        {
            if(enable[z] == true)
            {
                if (waitFlg[z] == false && upFlg[z] == false) {
                    Vector3 newPos = _birdsPos[z].position;
                    newPos.y -= speed;
                    _birdsPos[z].position = newPos;
                }

                if(_birdsPos[z].position.y < 0 && upFlg[z] == false)
                {
                    waitFlg[z] = true;
                
                }



                if (upFlg[z] == true && waitFlg[z] == false)
                {
                    Vector3 newPos = _birdsPos[z].position;
                    newPos.y += speed;
                    _birdsPos[z].position = newPos;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    waitFlg[z] = false;
                    upFlg[z] = true;
                }
            }


        }
    }
}
