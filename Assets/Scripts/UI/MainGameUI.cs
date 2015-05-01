using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class MainGameUI : MonoBehaviour {
    
	// Use this for initialization
	void Start () 
    {
        //Set target object to the main player
        EventTrigger.Entry function=new EventTrigger.Entry();
        function.callback=new EventTrigger.TriggerEvent();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
