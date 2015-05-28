using UnityEngine;
using System.Collections;
[System.Serializable]
public class Spell{

    public GameObject spellPrefab;
    public float manaCost;
    public float totalCooldown;
    public float currentCooldown;
    public string name;
    public string description;
    public int cost;
    public spell[] spellUpgrades;
    public bool isUnlocked;

    //void Start()
    //{
    //    string data = Bank.instance.saveFile.GetItem("Spell_" + this.name);
    //    if (data == null)
    //    {
    //        Bank.instance.saveFile.SaveItem("Spell_" + this.name, false.ToString());
    //        isUnlocked = false;
    //    }
    //    else if (data == "True")
    //    {
    //        isUnlocked = true;
    //    }
    //    else
    //    {
    //        isUnlocked = false;
    //    }
    //}
	
}
