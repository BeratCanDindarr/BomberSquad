using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlaneData : ScriptableObject
{
    [Header("AirPlane Property")]
    public string Name;
    public float FireRate;
    public bool IsPropellerActive;
    public float MovementSpeed;
    public float TurnSpeed;
    public GameObject Prefabs;
}
