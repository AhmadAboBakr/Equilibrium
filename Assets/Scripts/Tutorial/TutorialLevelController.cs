using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TutorialLevelController : MonoBehaviour
{
    public static TutorialLevelController instance; 
    public List<GameObject> stages;
    int currentState = 0;
    AsyncOperation async;

    void Awake()
    {
        instance = this;
    }
    IEnumerator Start()
    {
        foreach (var stage in stages)
        {
            stage.gameObject.SetActive(false);
        }
        stages[currentState].SetActive(true);
        async = Application.LoadLevelAsync("Level Selection Screen");
        async.allowSceneActivation = false;
        yield return async;
    }

    public void StartNextSegment()
    {
        if (currentState < stages.Count-1) {
            stages[currentState++].SetActive(false);
            stages[currentState].SetActive(true);
        }
        else
        {
            async.allowSceneActivation = true;
        }
    }

}
