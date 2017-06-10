using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour {

    public string titleSceneName;
    public string gameMainSceneName;
    public string resultSceneName;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
           // Debug.Log(SceneManager.GetActiveScene().name);
            if (SceneManager.GetActiveScene().name == titleSceneName)
            {
                SceneManager.LoadScene(gameMainSceneName);
            }

            if (SceneManager.GetActiveScene().name == gameMainSceneName)
            {
                SceneManager.LoadScene(resultSceneName);
            }

            if (SceneManager.GetActiveScene().name == resultSceneName)
            {
                SceneManager.LoadScene(titleSceneName);
            }
        }

    }
}
