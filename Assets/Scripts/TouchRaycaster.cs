using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRaycaster : MonoBehaviour {
    Camera mainCamera;
	// Use this for initialization
	void Start () {
        mainCamera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 30f, new Color(1f, 0, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObj = hit.collider.gameObject;
                if (hitObj.tag == SharkControll.FISH_L ||
                    hitObj.tag == SharkControll.FISH_M ||
                    hitObj.tag == SharkControll.FISH_S ||
                    hitObj.tag == SharkControll.DUST ||
                    hitObj.tag == SharkControll.MASH)
                {
                    hitObj.GetComponent<MoveObj>().AddForceObj();
                }
            }
        }
	}
}
