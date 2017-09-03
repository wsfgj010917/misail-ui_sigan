using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour {

	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
        else if (Input.anyKeyDown) { SceneManager.LoadScene(1); }
	}
}
