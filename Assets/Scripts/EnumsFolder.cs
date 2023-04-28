using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumsFolder : MonoBehaviour
{
    public enum PoolObjectName
    {
        PARTICALS,
        BOMB,
        PLANE,
        COIN,
        BULLET,
        ENEMY,
        DEFAULT
    }
    public enum Partical
    {
        BOMBEXPLOSION,
        NUKE,
    }
    public enum Plane
    {
        PLANE1,
        
    }
    public enum Bombs
    {
        BOMB1,

    }
    public enum Coin
    {
        COIN1,
    }
    public enum Bullet
    {
        RIFLEMAN,

    }
    public enum Enemy 
    { 
        RIFLEMAN,
    }

    public enum PlaneLandingOrLiftingAnim
    {
        LANDING,
        LIFTING
    }
}
