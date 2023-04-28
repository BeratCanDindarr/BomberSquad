using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    Transform plane;
    bool shooting = false;
    bool outSidePlane = false;
    Animator anim;
    private void Start()
    {
        PlayerController.OutsideTheBase += PlayerOutSide;
        anim = gameObject.GetComponent<Animator>();
    }
    private void GetPlayerData()
    {
        plane = GameManager.Instance.airPlanePrefab.transform;
    }

    private void Update()
    {
        if (outSidePlane == false)
        {
            return;
        }
        float distance = Vector3.Distance(plane.position,transform.position);
        if (distance>2)
        {
            return;
        }
        if (distance <= 2 && !shooting)
        {

            transform.LookAt(new Vector3(plane.position.x,0f,plane.position.z));
            StartCoroutine(ShootAnim());
        }
        
    }
    IEnumerator ShootAnim()
    {
        shooting = true;
        yield return new WaitForSeconds(5f);
        anim.SetTrigger("Shoot");
        shooting = false;
    }
    public void Shoot()
    {
        GameObject bullet = PoolManager.ReturnObject((int)EnumsFolder.PoolObjectName.BULLET, (int)EnumsFolder.Bullet.RIFLEMAN);
        bullet.transform.position = transform.position;
        bullet.transform.LookAt(plane);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward*500);
    }
    private void PlayerOutSide(bool isActive)
    {
        GetPlayerData();
        outSidePlane = isActive;
    }
    private void OnDisable()
    {
        PlayerController.OutsideTheBase -= PlayerOutSide;
    }
}
