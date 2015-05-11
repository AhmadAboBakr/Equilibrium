using UnityEngine;
using System.Collections;

public class Bank : MonoBehaviour {
    public SaveData saveFile;
    public int currentMoneyz;
    public static Bank instance;
    void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
        saveFile = new SaveData();
        saveFile.CreateSaveFile();
        string data = saveFile.GetItem("Money");
        if (data == null)
        {
            saveFile.SaveItem("Money", "0");
            currentMoneyz = 0;
        }
        else
        {
            currentMoneyz = int.Parse(data);
        }
    }
	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	public void Update () 
    {
	
	}

    public bool BuyUpgrade(int cost)
    {
        if (currentMoneyz >= cost)
        {
            currentMoneyz -= cost;
            saveFile.SaveItem("Money", currentMoneyz.ToString());
            return true;
        }
        return false;
    }
}
