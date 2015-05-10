using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class SpellRotator : MonoBehaviour {
    public Quaternion targetRotation;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 4);
	}
}
