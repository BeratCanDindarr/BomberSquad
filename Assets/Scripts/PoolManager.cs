using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PoolManager: MonoBehaviour
{
    public List<PoolScriptableObjects> pools;
    public List<List<List<GameObject>>>poolList = new List<List<List<GameObject>>>();

    public delegate GameObject returnObject(int PoolObjectNameID, int secondPoolObjecNameID);
    public static returnObject ReturnObject;
    public delegate void setObject(int PoolObjectNameID, int secondPoolObjecNameID, GameObject _object);
    public static setObject SetObject;
    private void Awake()
    {
        //StartPoolCreate();
        CreatePoolList();
        ReturnObject += ReturnGameObject;
        SetObject += SetGameObject;
    }

    private void CreatePoolList()
    {
        for (int i = 0; i < pools.Count; i++)
        {
            poolList.Add(new List<List<GameObject>>());
            for (int j = 0; j < pools[i].pools.Count; j++)
            {
                poolList[i].Add(new List<GameObject>());
                PoolAdd(i,j,pools[i].pools[j].Prefab, pools[i].pools[j].Size);

            }
        }
    }
    //private void StartPoolCreate()
    //{
    //    foreach (var poolObject in pools)
    //    {
    //        foreach (var obje in poolObject.pools)
    //        {
    //            for (int i = 0; i < obje.Size; i++)
    //            {
    //                Debug.Log(obje.Prefab);
    //                GameObject prefab = Instantiate(obje.Prefab, gameObject.transform.position, gameObject.transform.rotation);
    //                prefab.transform.parent = gameObject.transform;
    //                prefab.SetActive(false);
    //                obje.Prefabs.Add(prefab);
    //            }
    //        }
    //    }
    //}
    private void PoolAdd(int PoolObjectNameID, int secondPoolObjecNameID,GameObject obj,int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject prefab = Instantiate(obj, gameObject.transform.position, obj.transform.rotation);
            prefab.transform.parent = gameObject.transform;
            prefab.SetActive(false);
            poolList[PoolObjectNameID][secondPoolObjecNameID].Add(prefab);
        }
    }

    private GameObject ReturnGameObject(int PoolObjectNameID, int secondPoolObjecNameID)
    {
        
        //GameObject returnGameObject = pools[PoolObjectNameID].pools[secondPoolObjecNameID].Prefabs[0];
        GameObject returnGameObject = poolList[PoolObjectNameID][secondPoolObjecNameID][0];
        if (returnGameObject == null)
        {
            PoolAdd(PoolObjectNameID, secondPoolObjecNameID, pools[PoolObjectNameID].pools[secondPoolObjecNameID].Prefab,5);
        }
        //pools[PoolObjectNameID].pools[secondPoolObjecNameID].Prefabs.RemoveAt(0);
        poolList[PoolObjectNameID][secondPoolObjecNameID].RemoveAt(0);
        returnGameObject.SetActive(true);
        return returnGameObject;
    }

    private void SetGameObject(int PoolObjectNameID, int secondPoolObjecNameID, GameObject _object)
    {
        //pools[PoolObjectNameID].pools[secondPoolObjecNameID].Prefabs.Add(_object);
        _object.SetActive(false);
        poolList[PoolObjectNameID][secondPoolObjecNameID].Add(_object);
        
    }
    //private void PoolDestroy()
    //{
    //    foreach (var poolObject in pools)
    //    {
    //        foreach (var obje in poolObject.pools)
    //        {
    //            obje.Prefabs.Clear();
    //        }
    //    }
    //}
    private void OnDestroy()
    {
        //PoolDestroy();
        ReturnObject -= ReturnGameObject;
        SetObject -= SetGameObject;
    }
}
