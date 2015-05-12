using UnityEngine;
using System.Collections;

public class LossUI : MonoBehaviour {
    public static LossUI instance;
    
	// Use this for initialization
    void Awake()
    {
        instance = this;
        this.gameObject.SetActive(false);
        Debug.Log("this");
    }
	void Start()
    {
        
    }
    void OnEnable()
    {

    }

    public void BackToLevelSelect()
    {
        instance.gameObject.SetActive(false);
        Player.player.HealthPoints = Player.player.maxHealthPoints;
        Application.LoadLevel("Level Selection Screen");
    }

    public void Retry()
    {
        instance.gameObject.SetActive(false);
        Player.player.HealthPoints = Player.player.maxHealthPoints;
        Application.LoadLevel(Application.loadedLevel);
    }
}
