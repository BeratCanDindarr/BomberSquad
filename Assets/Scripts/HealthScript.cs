using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour, IDamage
{
    [SerializeField]private float maxHealth;
    [SerializeField]private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    
    public void Damage(float damage)
    {
        currentHealth -= damage;
        CheckDeath(currentHealth);
    }

    public void CheckDeath(float health)
    {
        if (health <= 0)
        {
            DetectEnemy.ObjectDestroyed(gameObject);

            GameObject coin = PoolManager.ReturnObject((int)EnumsFolder.PoolObjectName.COIN, (int)EnumsFolder.Coin.Coin1);
            coin.transform.position = new Vector3(gameObject.transform.position.x+0.3f, gameObject.transform.position.y, gameObject.transform.position.z);
            Destroy(gameObject);

            
        }
    }
   

}
