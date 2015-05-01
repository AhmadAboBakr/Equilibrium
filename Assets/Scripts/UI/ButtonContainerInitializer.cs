using UnityEngine;
using System.Collections;

public class ButtonContainerInitializer : MonoBehaviour {
    Vector2 start, end;
	// Use this for initialization
	void Start () {
        GetComponent<RectTransform>().anchoredPosition = -new Vector2(0, this.GetComponent<RectTransform>().rect.height) * .8f + GetComponent<RectTransform>().anchoredPosition;
        
	}
	
	// Update is called once per frame
	void Update () {

	}
}
