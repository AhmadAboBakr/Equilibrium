using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BankTeller : MonoBehaviour {
    public GameObject lastClicked;
    public static BankTeller instance;
    public Text currentMoneyzText;
	// Use this for initialization
	void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
    }
    void Start () 
    {
        UpdateCurrentMoneyz();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MakeTransaction()
    {
        if(lastClicked.CompareTag("Spell"))
        {
            if(Bank.instance.BuyUpgrade(lastClicked.GetComponent<Spell>().cost)&&!lastClicked.GetComponent<Spell>().isUnlocked)
            {
                lastClicked.GetComponent<Spell>().isUnlocked = true;
                Bank.instance.saveFile.SaveItem("Spell_"+ lastClicked.GetComponent<Spell>().name, true.ToString());
                UpdateCurrentMoneyz();
            }
            else
            {
                //insufficient funds
                Debug.Log("Insufficient logs");
            }
        }
        else if (lastClicked.CompareTag("SpellUpgrade") && !lastClicked.GetComponent<SpellUpgrade>().isUnlocked)
        {
            if (Bank.instance.BuyUpgrade(lastClicked.GetComponent<SpellUpgrade>().cost))
            {
                lastClicked.GetComponent<SpellUpgrade>().isUnlocked = true;
                Bank.instance.saveFile.SaveItem("SpellUpgrade_"+ lastClicked.GetComponent<SpellUpgrade>().name,true.ToString());
                UpdateCurrentMoneyz();
            }
            else
            {
                //insufficient funds
                Debug.Log("Insufficient logs");
            }
        }
    }
    public void UpdateCurrentMoneyz()
    {
        currentMoneyzText.text = Bank.instance.currentMoneyz + " Fragments";
    }
}