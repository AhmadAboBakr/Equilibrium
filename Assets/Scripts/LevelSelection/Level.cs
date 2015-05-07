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
    void Awake()
    {
        saveFile = new SaveData();
        saveFile.CreateSaveFile();
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
