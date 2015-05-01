using UnityEngine;
using System.Collections;
[ExecuteInEditMode]	
public class RealTimeRotatingObject : MonoBehaviour
{
    void Update()
    {
        this.transform.up = this.transform.position;
	}
}
