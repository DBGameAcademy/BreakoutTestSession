using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolObjectItem
{
    public enum ePoolItem
    {
        Ball,
        Block,
        PowerUp,
        Paddle,
        WallImpact,
        BlockBreak,
        Bullet,
    }
    public ePoolItem PoolItemKey;
    public int NumberToPool;
    public bool ExtendPoolIfEmpty;
    public PoolObject ObjectToPool;
}

public class PoolManager : MonoSingleton<PoolManager>
{
    //this is the list that contains all the objects PREFABS that we want to pool
    public List<PoolObjectItem> itemsToPool;
        
    //Dictionary containig a key for each PoolObject and the list of PoolObject that might be used.
    private Dictionary<PoolObjectItem.ePoolItem, List<PoolObject>> poolDictionary = new Dictionary<PoolObjectItem.ePoolItem, List<PoolObject>>();

    private void Start()
    {
        if (itemsToPool.Count == 0)
        {
            Debug.LogError("Please make sure that you set the list of poolable objects.");
        }

        // populate the pool dictionary with items
        foreach (PoolObjectItem item in itemsToPool)
        {
            if (item == null)
            {
                Debug.LogWarning("PoolObjectItem object: " + item.ToString() + " is null.");
            }
            else
            {
                if (poolDictionary.ContainsKey(item.PoolItemKey) == false)
                {
                    List<PoolObject> newPoolObjectList = new List<PoolObject>();
                    if (item.ObjectToPool != null)
                    {
                        for (int i = 0; i < item.NumberToPool; i++)
                        {
                            InstantiateNewPoolObject(item.ObjectToPool, newPoolObjectList);
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Attempted to instantiate new object for " + item.PoolItemKey + " but there is no gameobject prefab");
                    }
                    
                    poolDictionary.Add(item.PoolItemKey, newPoolObjectList);
                }
                else
                {
                    Debug.LogWarning("Attempted to add existing pool item key to dictionary of pool items, pool item keys should be unique!");
                }
            }
        }
    }

    public PoolObject GetObjectFromPool(PoolObjectItem.ePoolItem poolKey)
    {
        if (poolDictionary.ContainsKey(poolKey))
        {
            //Get the list
            List<PoolObject> PoolObjectList = poolDictionary[poolKey];

            for (int i = 0; i < PoolObjectList.Count; i++)
            {
                if (PoolObjectList[i].active == false)
                    return PoolObjectList[i];
            }

            //Now, spawn a new one, if we don't find anyone
            foreach(PoolObjectItem item in itemsToPool)
            {
                if (item.PoolItemKey == poolKey && item.ExtendPoolIfEmpty)
                {
                    return InstantiateNewPoolObject(item.ObjectToPool, PoolObjectList);
                }
            }

            // nothing in pool and we don't want to create a new one so return null
            Debug.LogWarning("Failed to return pool item " + poolKey + " as there are none in the pool and we do not create any more");
            return null;
        }
        else
        {
            // no entry has been added for this type of pool item
            Debug.LogWarning("Failed to return pool item from " + poolKey + " as it is unused in the dictionary of pool items");
            return null;
        }
    }


    private PoolObject InstantiateNewPoolObject(PoolObject poolObjectToSpawn, List<PoolObject> poolListToAdd)
    {
        //New poolObject
        PoolObject newPoolObject = (PoolObject)Instantiate(poolObjectToSpawn, Vector3.zero, Quaternion.identity);

        // start disabled
        newPoolObject.DisablePoolObject();

        //use this as the transform to keep pool objects organised.
        newPoolObject.transform.SetParent(transform);

        //make sure there is a pool object script on the new object
        newPoolObject.gameObject.AddComponent<PoolObject>();

        //Add to the pool list
        poolListToAdd.Add(newPoolObject);

        return newPoolObject;
    }


    public void ReturnObjectToPool(GameObject objectToReturn)
    {
        // do we have an actual gameobject
        if (objectToReturn == null)
        {
            Debug.LogError("Attempted to return null object to pool, this is not allowed!");
            return;
        }

        // and is it a pooled/able object
        PoolObject poolObject = objectToReturn.GetComponent<PoolObject>();
        if (poolObject == null)
        {
            Debug.LogError("Attempted to return object that isn't from a pool, this is not allowed!");
            return;
        }

        //return to the dictionary of pools
        poolDictionary[poolObject.poolKey].Add(poolObject);

        // parent to this for storage in hierachy
        poolObject.transform.SetParent(transform);

        //disable the object
        poolObject.DisablePoolObject();
    }
}