using UnityEngine;
using System.Collections;


public class SaveTest : MonoBehaviour
{

	// Use this for initialization
	void Start () {
        SaveData s = new SaveData();
        s.CreateSaveFile();
        s.SaveItem("s1", "30");
        print(s.GetItem("s1"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
