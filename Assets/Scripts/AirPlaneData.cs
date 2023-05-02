using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Create/NewAirPlaneData",fileName ="AirPlaneData")]

public class AirPlaneData : ScriptableObject
{
    [System.Serializable]
    public class AirPlanes
    {
        [Header("AirPlane Property")]
        public string Name;
        public float FireRate;
        public bool IsPropellerActive;
        public float MovementSpeed;
        public float TurnSpeed;
        public int Health;
        public EnumsFolder.Plane AirPlanePrefabData;
        public EnumsFolder.Bombs AirPlaneBombData;
    }
    public string Name;
    public List<AirPlanes> airPlanes;
}
