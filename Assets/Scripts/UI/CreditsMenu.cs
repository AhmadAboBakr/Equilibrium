﻿using UnityEngine;
using System.Collections;

public class CreditsMenu : MonoBehaviour {

    public GameObject mainMenu;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }
	}
    public void Back()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
