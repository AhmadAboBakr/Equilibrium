using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameBackToLevelSelection : MonoBehaviour {
    public GameObject panel; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(panel.active == false && !GameManager.instance.isEnabled)
            {
                panel.SetActive(true);
                PauseAndBack.instance.Pause();
            }
        }
	}
    public void Back()
    {
        PauseAndBack.instance.Back();
    }
    public void Cancel()
    {
        panel.SetActive(false);
        PauseAndBack.instance.Pause();
    }
}
