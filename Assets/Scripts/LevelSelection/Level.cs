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

    void Start()
    {

        objectives = transform.GetComponentsInChildren<Objective>();
        
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
