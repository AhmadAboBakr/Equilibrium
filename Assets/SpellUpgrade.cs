using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SpellUpgrade : MonoBehaviour {
    public string name;
    public string description;
    public int cost;
    public int level;
    public bool isUnlocked;
    public Text upgradeName, upgradeDescription, upgradeCost;
	// Use this for initialization
	void Start () 
    {
        string data = Bank.instance.saveFile.GetItem("SpellUpgrade_" + this.name);
        if (data == null)
        {
            Bank.instance.saveFile.SaveItem("SpellUpgrade_" + this.name,false.ToString());
            isUnlocked = false;
        }
        else if(data == "True")
        {
            isUnlocked = true;
        }
        else
        {
            isUnlocked = false;
        }

        
        upgradeName = GameObject.FindGameObjectWithTag("UpgradeName").GetComponent<Text>();
        upgradeDescription = GameObject.FindGameObjectWithTag("UpgradeDescription").GetComponent<Text>();
        upgradeCost = GameObject.FindGameObjectWithTag("UpgradeCost").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void showInfo()
    {
        
            upgradeName.text = this.name;
            upgradeDescription.text = this.description;
            upgradeCost.text = this.cost.ToString() + " stars";
            BankTeller.instance.lastClicked = this.gameObject;
    }
}