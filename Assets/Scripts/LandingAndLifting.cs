using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class LandingAndLifting : MonoBehaviour
{
    public List<Vector3> BaseTransform;
    private GameManager gameManager;
    private Transform player;
    private BoxCollider collider;
    public delegate void playerBaseAnim(int PlayerLandingOrLifting);
    public static playerBaseAnim PlayerBaseAnim;

    private void Awake()
    {
        GameManager.CreatedPlayer += GetPlayerTransform;
        PlayerBaseAnim += PlayerSetTransform;
        BaseTransform[0] = gameObject.transform.position;
        BaseTransform[1] = new Vector3(gameObject.transform.position.x, 0.759f, gameObject.transform.position.z + 0.4f);
        //gameManager = GameManager.Instance;
        collider = gameObject.GetComponent<BoxCollider>();
        //collider.enabled = false;

    }


    
    public void GetPlayerTransform(Transform airPlaneTransform)
    {
        player = airPlaneTransform;
    }

    private void PlayerSetTransform(int PlayerLandingOrLifting)
    {
        player.transform.DOMove(BaseTransform[PlayerLandingOrLifting],2f).OnComplete(()=> CheckLanding(PlayerLandingOrLifting));
      
        

    }
    void CheckLanding(int isLanding)
    {
        if (isLanding== 0)
        {
            player.transform.DORotate(gameObject.transform.forward , 1f);
            collider.enabled = false;
            
        }
        else
        {
            
            StartCoroutine(ColiderActivate());
        }
    }
    IEnumerator ColiderActivate()
    {
        collider.enabled = false;
        yield return new WaitForSeconds(6f);
        collider.enabled = true;
    }

    private void OnDisable()
    {
        GameManager.CreatedPlayer -= GetPlayerTransform;
        PlayerBaseAnim -= PlayerSetTransform;
    }





}
