using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public EnumsFolder.Bombs bombID;
    private GameObject effect;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject,0.5f);
            effect = PoolManager.ReturnObject((int)EnumsFolder.PoolObjectName.PARTICALS, (int)EnumsFolder.Partical.BOMBEXPLOSION);
            effect.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+0.1f, gameObject.transform.position.z);
            
            
            StartCoroutine(WaitEndReturnObject());
            
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            var damageScript = other.gameObject.GetComponent<IDamage>();
            if (damageScript != null)
            {
                damageScript.Damage(100);
            }
        }
    }
    IEnumerator WaitEndReturnObject()
    {


        yield return new WaitForSeconds(0.2f);
        
        PoolManager.SetObject((int)EnumsFolder.PoolObjectName.PARTICALS, (int)EnumsFolder.Partical.BOMBEXPLOSION,effect);
        PoolManager.SetObject((int)EnumsFolder.PoolObjectName.BOMB, (int)bombID, effect);
        gameObject.SetActive(false);

    }
   
}
