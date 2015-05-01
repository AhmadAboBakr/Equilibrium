using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FirstTutorialText : MonoBehaviour {

    public float timer = 0;
    public GameObject button;
    AsyncOperation async;
	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () 
    {

        timer += Time.deltaTime;
        if (timer > 7.0f)
        {
            this.GetComponent<Text>().text = "While moving in one direction, tap on the opposite one to jump";
        }
        if(timer > 13.0f)
        {
            button.gameObject.SetActive(true);
        }
	}
    public void goToLevel()
    {
        async.allowSceneActivation = true;
    }
    IEnumerator Start()
    {
        async = Application.LoadLevelAsync(3);
        async.allowSceneActivation = false;
        yield return async;

    }
}
