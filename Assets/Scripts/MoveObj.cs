using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObj : MonoBehaviour
{
    public float speed_y = 0.1f;
    public Rigidbody rg;
    

	void Start ()
    {
        rg = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        transform.Translate(0, -speed_y, 0);

        if(transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }

        /*if (Input.GetMouseButtonDown(0))
        {
            rg.AddForce(0, 1f, 5f, ForceMode.Impulse);
        }*/
        

        Debug.Log(Vector3.Distance(this.transform.position, Input.mousePosition));
        Vector3.Distance(this.transform.position, Input.mousePosition);
    }

    public void AddForceObj()
    {
        if (transform.position.y < 0f)
        {
            rg.AddForce(0, 3f, 10f, ForceMode.Impulse);
        }
    }
       
}
