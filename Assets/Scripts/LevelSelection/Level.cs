using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public enum levelStatus
{
    locked,
    unlocked,
    destroyed
};
public enum Difficulty
{
    Easy, 
    Medium,
    Hard
};
public class Level : MonoBehaviour {
    
    public levelStatus status;
    public string description;
    public Difficulty difficulty;
    public string name;
    public Objective[] objectives;
    public SaveData saveFile;
    public List<Level> prerequisiteLevels;

    //public Text console;
    void Awake()
    {
        //console = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Text>();
        
        saveFile = new SaveData();
        saveFile.CreateSaveFile();
        string loadedLevel = saveFile.GetItem("level_" + name);
        if (loadedLevel == "Destroyed")
        {
            status = levelStatus.destroyed;
        }
    }
    void Start()
    {
        objectives = transform.GetComponentsInChildren<Objective>();
        
        for (int i = 0; i < objectives.Length; i++)
        {
            string state = saveFile.GetItem(name + i);
            if (state == "True")
                objectives[i].status = true;
            else
                objectives[i].status = false;
        }

        if(status == levelStatus.locked)
        {
            for (int i = 0; i < prerequisiteLevels.Count; i++)
            {
                if(prerequisiteLevels[i].status == levelStatus.destroyed)
                {
                    status = levelStatus.unlocked;
                }
            }
        }

       
        switch (status)
        {
            case levelStatus.locked:
                this.GetComponent<Image>().color = Color.black;
                break;
            case levelStatus.unlocked:
                this.GetComponent<Image>().color = Color.white;
                break;
            case levelStatus.destroyed:
                this.GetComponent<Image>().color = Color.red;
                break;
            default:
                break;
        }
    }
}
