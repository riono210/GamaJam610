using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObj : MonoBehaviour
{
    public float speed = 0.1f;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        transform.Translate(0, -speed, 0);

        if(transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }


	}
}
