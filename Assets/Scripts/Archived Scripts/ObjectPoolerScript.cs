﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolerScript : MonoBehaviour {

    public static ObjectPoolerScript current;
    public GameObject pooledObject;
    public int pooledAmmount = 20;
    public bool willGrow = true;

    public List<GameObject> pooledObjects;


    void Awake()
    {
        current = this;
    }

	void Start () 
    {
	    pooledObjects = new List<GameObject>();

        for (int i = 0; i < pooledAmmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
	}

    public GameObject getPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
	
	
}
