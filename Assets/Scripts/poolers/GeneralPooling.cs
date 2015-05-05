using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GeneralPooling : MonoBehaviour {
    List<GameObject> pooledObjects;
    public GameObject [] objectsToPool;
    public int numberOfObjects;
    void Start () {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < numberOfObjects; ++i)
        {
            int random =Random.Range(0,objectsToPool.Length);
            Debug.Log(random);
            var  pooledObject= GameObject.Instantiate(objectsToPool[random]) as GameObject;
            pooledObjects.Add(pooledObject);
            pooledObject.transform.parent = this.transform;
            pooledObject.SetActive(false);
        }
	}
	virtual public GameObject CreateObject(Vector3 position,Quaternion angle){
        GameObject pooledObject;
        if(pooledObjects.Count>0){
            pooledObject = pooledObjects[0];
            pooledObjects.Remove(pooledObject);
            pooledObject.SetActive(true);
            //pooledObject.transform.parent = null;
            pooledObject.transform.position = position;
            pooledObject.transform.rotation = angle;
        }
        else
        {
            pooledObject = GameObject.Instantiate(objectsToPool[Random.Range(0, objectsToPool.Length)]) as GameObject;
        }
        return pooledObject;
    }
    virtual public GameObject ReturnObjectToPool(GameObject pooledObject)
    {
        pooledObject.transform.parent=this.transform;
        pooledObject.SetActive(false);
        pooledObjects.Add(pooledObject);
        return pooledObject;
    }
}
