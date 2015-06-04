using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class InGameObjectiveUI : MonoBehaviour {
    public Text[] objectivesText;
    public Image[] objectivesImages;
    public static InGameObjectiveUI instance;
    public SaveData saveFile;


    void Awake()
    {
        instance = this;
        instance.gameObject.SetActive(false);
        saveFile = new SaveData();
        saveFile.CreateSaveFile();

        for (int i = 0; i < GameManager.instance.objectives.Length; i++)
        {
            string save = saveFile.GetItem(Application.loadedLevelName + i);

            if (save == "True")
            {
                GameManager.instance.objectives[i].status = true;
            }
            else
            {
                GameManager.instance.objectives[i].status = false;
            }
        }
    }
    void OnEnable()
    {
        if (GameManager.instance)
        {
            for (int i = 0; i < GameManager.instance.objectives.Length; i++)
            {
                GameManager.instance.objectives[i].checkObjective();
                if(GameManager.instance.objectives[i].checkObjective())
                {
                    //objectivesImages[i].color = Color.green;
                }
                else
                {
                    objectivesImages[i].color = Color.red;
                }
            }
            saveObjectiveStatus();
        }

    }

	void Start () 
    {
        

	}
	
	// Update is called once per frame
	void Update () 
    {
	   // GameManager.instance.gameObject.transform.GetChild[0]
        for (int i = 0; i < objectivesText.Length; i++)
        {
            objectivesText[i].text = GameManager.instance.objectives[i].getDescription();
        }
	}

    public void GoToLevelSelect()
    {
        saveFile.SaveItem("level_" + Application.loadedLevelName,"Destroyed");
        instance.gameObject.SetActive(false);
        Application.LoadLevel("Level Selection Screen");
    }

    public void Retry()
    {
        
        instance.gameObject.SetActive(false);
        Application.LoadLevel(Application.loadedLevel);
    }

    public void saveObjectiveStatus()
    {
        for (int i = 0; i < GameManager.instance.objectives.Length; i++)
			{
                saveFile.SaveItem(Application.loadedLevelName + i, GameManager.instance.objectives[i].status.ToString()); 
			}
        
    }
}

  
