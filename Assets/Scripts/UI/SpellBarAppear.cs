using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class SpellBarAppear : MonoBehaviour
{
	// Use this for initialization
    Vector2 start, end;
    public bool isClicked;
    
    
	void Start () 
    {
       
        
       
        start = GetComponent<RectTransform>().anchoredPosition;
        end = start - new Vector2(0, this.GetComponent<RectTransform>().rect.height);
        
	}
    
    void Update()
    {
        
        if (isClicked)
        {
            GetComponent<RectTransform>().anchoredPosition = Vector3.Slerp(start, end, .1f);
            
        }

        else
        {
            GetComponent<RectTransform>().anchoredPosition = Vector3.Slerp(end, start, .1f);
            
        }
    }
    public void rotateQuarterCircle()
    {

        isClicked = true;
        
    }
    public void returnQuarterCircle()
    {
       
        isClicked = false;
    }
    public void ButtonHandleExit()
    {
        isClicked = isClicked ^ true;

    }
}
