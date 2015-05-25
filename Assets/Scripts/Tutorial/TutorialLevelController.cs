using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TutorialLevelController : MonoBehaviour
{
    public static TutorialLevelController instance; 
    public List<GameObject> stages;
    int currentState = 0;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        foreach (var stage in stages)
        {
            stage.gameObject.SetActive(false);
        }
        stages[currentState].SetActive(true);

    }
    public void StartNextSegment()
    {
        stages[currentState++].SetActive(false);
        stages[currentState].SetActive(true);
    }

}
