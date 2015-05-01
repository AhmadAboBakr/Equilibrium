using UnityEngine;
using System.Collections;
//using UnityEngine.UI;

public class MainMenuFunctions : MonoBehaviour {

	// Use this for initialization
    AsyncOperation async;
    public GameObject optionPanel;
    public GameObject creditsPanel;

	
	// Update is called once per frame
	void Update () 
    {
        if (async.isDone)
        {
            Debug.Log("Loading complete");
        }
	}
    public void PlayButton()
    {
        
        async.allowSceneActivation = true;
    }
    public void OptionsButton()
    {
        
        optionPanel.SetActive(true);
        gameObject.SetActive(false);
        
       
    }
    public void CreditsButton()
    {
        creditsPanel.SetActive(true);
        gameObject.SetActive(false);
    }
    IEnumerator Start()
    {
        async = Application.LoadLevelAsync(2);
        async.allowSceneActivation = false;
        yield return async;
        
    }
}
