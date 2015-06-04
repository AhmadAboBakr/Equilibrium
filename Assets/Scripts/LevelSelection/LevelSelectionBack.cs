using UnityEngine;
using System.Collections;

public class LevelSelectionBack : MonoBehaviour {

	
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("mainMenuScene");
        }
	}
}
