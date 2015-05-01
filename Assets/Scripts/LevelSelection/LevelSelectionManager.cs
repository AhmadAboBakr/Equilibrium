using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelectionManager : MonoBehaviour {
    public Text name;
    public Text description;
    public Text objective1;
    public Text objective2;
    public Text objective3;
    public Image objectiveStatus1;
    public Image objectiveStatus2;
    public Image objectiveStatus3;
    public Text difficulty;
    
	public void loadLevel()
    {
        if (name.text != "LOCKED")
        {
            Application.LoadLevel(name.text);
        }
    }
    public void ShowLevelInfo(int levelID)
    {
        var level = this.transform.GetChild(levelID).GetComponent<Level>();
        if (level.status == levelStatus.locked)
        {
            name.text = "LOCKED";
            description.text = "LOCKED";
            difficulty.text = "LOCKED";
            difficulty.color = Color.white;
            objective1.text = "LOCKED";
            objective2.text = "LOCKED";
            objective3.text = "LOCKED";
            objectiveStatus1.color = Color.black;
            objectiveStatus2.color = Color.black;
            objectiveStatus3.color = Color.black;
        }
        else
        {
            switch (level.difficulty)
            {
                case Difficulty.Easy:
                    difficulty.color = Color.green;
                    break;
                case Difficulty.Medium:
                    difficulty.color = Color.yellow;
                    break;
                case Difficulty.Hard:
                    difficulty.color = Color.red;
                    break;
                default:
                    break;
            }
            name.text = level.name;
            description.text = level.description;
            difficulty.text = level.difficulty.ToString();
            objective1.text = level.objectives[0].getDescription();
            objective2.text = level.objectives[1].getDescription();
            objective3.text = level.objectives[2].getDescription();
            objectiveStatus1.color = level.objectives[0].status ? Color.green : Color.red;
            objectiveStatus2.color = level.objectives[1].status ? Color.green : Color.red;
            objectiveStatus3.color = level.objectives[2].status ? Color.green : Color.red;
        }
        
    }
}
