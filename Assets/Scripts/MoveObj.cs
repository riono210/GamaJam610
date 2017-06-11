using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObj : MonoBehaviour
{
    public float speed_y = 0.11f;
    public Rigidbody rg;

    float EndPosZ = 15.63f;
    float StartPosZ = 0;

    bool touchFlg = false;

    void Start ()
    {
        speed_y = 0.095f + Random.value * 0.05f;
        rg = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        if (!touchFlg) {
            transform.Translate(0, -speed_y, 0);
        }
        if(transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }

        if (touchFlg)
        {
            Vector3 newScale = transform.localScale;
            newScale *= 0.984f;
            transform.localScale = newScale;

        }


        /*if (Input.GetMouseButtonDown(0))
        {
            rg.AddForce(0, 1f, 5f, ForceMode.Impulse);
        }*/

        /*if (Input.GetMouseButtonDown(0)) {
            Debug.Log(Input.mousePosition);
            //Debug.Log(Vector3.Distance(this.transform.position, Input.mousePosition));
            Vector3.Distance(transform.position, Input.mousePosition);
        }*/
    }

    public void AddForceObj()
    {
        if (!touchFlg)
        {
            //rg.AddForce(0, 3f, 10f, ForceMode.Impulse);
            rg.velocity = new Vector3(0, 9f, 10f);
            rg.useGravity = true;
            touchFlg = true;
        }
    }
       
}
