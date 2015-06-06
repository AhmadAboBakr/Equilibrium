using UnityEngine;
using System.Collections;

public class PauseAndBack : MonoBehaviour {
    public static PauseAndBack instance;
    public bool pause = false;
    public GameObject PlayerControls;
	// Use this for initialization

	void Start () 
    {
	    if(!instance)
        {
            instance = this;
        }
        pause = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Back();
        //}
	}

    public void Pause()
    {
        if (!pause)
        {
            Time.timeScale = 0;
            pause = true;
        }
        else
        {
            Time.timeScale = 1;
            pause = false;
        }
        if (pause)
        {
            CastingArea.instance.gameObject.SetActive(false);
        }
        if (!pause)
        {
            CastingArea.instance.gameObject.SetActive(true);

        }
    }
    public void Back()
    {
        //PauseAndBack.instance.gameObject.SetActive(false);
        Player.player.HealthPoints = Player.player.maxHealthPoints;
        Application.LoadLevel("Level Selection Screen");
    }

}
