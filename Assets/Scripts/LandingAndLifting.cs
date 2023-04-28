using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class LandingAndLifting : MonoBehaviour
{
    public List<Vector3> BaseTransform;
    private GameManager gameManager;
    private Transform player;

    public delegate void playerBaseAnim(int PlayerLandingOrLifting);
    public static playerBaseAnim PlayerBaseAnim;

    private void Awake()
    {
        GameManager.CreatedPlayer += GetPlayerTransform;
        PlayerBaseAnim += PlayerSetTransform;
        BaseTransform[0] = gameObject.transform.position;
        gameManager = GameManager.Instance;
        
    }


    
    public void GetPlayerTransform(Transform airPlaneTransform)
    {
        player = airPlaneTransform;
    }

    private void PlayerSetTransform(int PlayerLandingOrLifting)
    {
        player.transform.DOMove(BaseTransform[PlayerLandingOrLifting],2f);
    }

    private void OnDisable()
    {
        GameManager.CreatedPlayer -= GetPlayerTransform;
    }





}
