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
            effect.transform.position = gameObject.transform.position;
            effect.SetActive(true);
            StartCoroutine(WaitEndReturnObject());
            
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.LogError("Öldü");
        }
    }
    IEnumerator WaitEndReturnObject()
    {


        yield return new WaitForSeconds(0.2f);
        effect.SetActive(false);
        PoolManager.SetObject((int)EnumsFolder.PoolObjectName.PARTICALS, (int)EnumsFolder.Partical.BOMBEXPLOSION,effect);
        PoolManager.SetObject((int)EnumsFolder.PoolObjectName.BOMB, (int)bombID, effect);
        gameObject.SetActive(false);

    }
   
}
