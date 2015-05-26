using UnityEngine;
using System.Collections;

public class TutorialStage : MonoBehaviour {
    public GameObject CompanioinUIElement;
    void OnEnable()
    {
        Debug.Log(CompanioinUIElement);
        if (CompanioinUIElement)
        {
            CompanioinUIElement.SetActive(true);
        }
    }
    void OnDisable()
    {
        if (CompanioinUIElement)
        {
            CompanioinUIElement.SetActive(false);
        }


    }
    void OnTriggerEnter2D()
    {
        TutorialLevelController.instance.StartNextSegment();
    }
}
