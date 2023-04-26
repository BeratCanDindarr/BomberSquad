using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Create/newPool",fileName ="newPool")]
public class PoolScriptableObjects : ScriptableObject
{
    [System.Serializable]
    public class pool
    {
        public string Name;
        public int Size;
        public GameObject Prefab;
        //public List<GameObject> Prefabs;
    }

    public string PoolName;
    public List<pool> pools;
}
