using UnityEngine;
using System.Collections;

public class InGameRotatingObject : MonoBehaviour {

    void Update()
    {
        this.transform.up = this.transform.position;
    }
}
