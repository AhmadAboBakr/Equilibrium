using UnityEngine;
using System.Collections;

public class LossUI : MonoBehaviour {
    public static LossUI instance;
    
	// Use this for initialization
    void Awake()
    {
        instance = this;
    }
	void Start()
    {
        this.gameObject.SetActive(false);
    }
    void OnEnable()
    {

    }

    public void BackToLevelSelect()
    {
        instance.gameObject.SetActive(false);
        Application.LoadLevel("Level Selection Screen");
    }

    public void Retry()
    {
        instance.gameObject.SetActive(false);
        Application.LoadLevel(Application.loadedLevel);
    }
}
