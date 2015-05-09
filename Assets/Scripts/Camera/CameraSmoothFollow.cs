using UnityEngine;
using System.Collections;

public class CameraSmoothFollow : MonoBehaviour {
    float interpVelocity;
    public GameObject target;
    public Vector3 offset;
    Vector3 targetPos;
    public float dampTime = 15f;
    // Use this for initialization
    void Start()
    {
        targetPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;
            Vector3 targetDirection = (target.transform.position - posNoZ);
            interpVelocity = targetDirection.magnitude * dampTime;
            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);
            transform.up = Vector3.Lerp(transform.up, target.transform.up + offset, 0.25f) ;

        }
    }
}
