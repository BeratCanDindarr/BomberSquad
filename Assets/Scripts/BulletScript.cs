using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Deneme");
            other.GetComponent<IDamage>().Damage(10);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
