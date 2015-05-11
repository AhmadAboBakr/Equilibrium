using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpellSelectionManager : MonoBehaviour {
    public GameObject scaleFactor;
    public float startScale, endScale, maxDistance,minDistance;
    public SpellRotator spellRotator;
    public int rotation;
    public SpellUpgrade[] spellUpgrades;
    public GameObject[] spellButtons;
    public bool isActivated;
    public Text SpellName, spellDescription, spellCost;
	// Use this for initialization
	void Start () 
    {
        SpellName = GameObject.FindGameObjectWithTag("UpgradeName").GetComponent<Text>();
        spellDescription = GameObject.FindGameObjectWithTag("UpgradeDescription").GetComponent<Text>();
        spellCost = GameObject.FindGameObjectWithTag("UpgradeCost").GetComponent<Text>();
        spellButtons = GameObject.FindGameObjectsWithTag("Spell");
        spellUpgrades = transform.GetComponentsInChildren<SpellUpgrade>();
        startScale = 0.03f;
        endScale = 0.14f;
        maxDistance = 364;
        minDistance = 1;
	}
	
	// Update is called once per frame
	void Update () {
       float distance = Vector2.Distance(this.transform.position, scaleFactor.transform.position);
       this.transform.localScale = new Vector3(1, 1, 1) * distance * (endScale - startScale) / maxDistance;
        if(isActivated)
        {
            foreach (var spellUpgrade in spellUpgrades)
            {
                spellUpgrade.gameObject.SetActive(true);
            }
        }
        else if(!isActivated)
        {
            foreach (var spellUpgrade in spellUpgrades)
            {
                spellUpgrade.gameObject.SetActive(false);
            }
        }
	}
    public void SetTargetRotation()
    {
        spellRotator.targetRotation = Quaternion.Euler(0, 0, rotation);
        foreach (var spellButton in spellButtons)
        {
            spellButton.GetComponent<SpellSelectionManager>().isActivated = false;
        }
        isActivated = true;
        foreach (var spellUpgrade in spellUpgrades)
        {
            spellUpgrade.gameObject.SetActive(true);
        }
        SpellName.text = this.GetComponent<Spell>().name;
        spellDescription.text = this.GetComponent<Spell>().description;
        spellCost.text = this.GetComponent<Spell>().cost.ToString()+" stars";
        BankTeller.instance.lastClicked = this.gameObject;
    }
    
}
