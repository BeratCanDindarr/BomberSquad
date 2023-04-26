using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("AirPlane Properties")]
    
    public List<AirPlaneData> airPlaneData;
    #region Active Player Data
    [Header("Active Player Plane Data")]
    public string planename;
    public float fireRate;
    public bool isPropellerActive;
    public float movementSpeed;
    public float turnSpeed;
    public EnumsFolder.Plane airPlanePrefabData;
    public EnumsFolder.Bombs airPlaneBombData;
    #endregion

    #region PlayerController
    public FloatingJoystick joystick;
    #endregion

    #region Properties
    private int money=0;
    public int Money { get { return money; } set { money = value; } }
    #endregion

    void Awake()
    {
        Instance = this;
        SetPlayerPlane((int)EnumsFolder.Plane.PLANE1);
        
    }
    void SetPlayerPlane(int EnumsFolderPlaneName)
    {
        var dataFile = airPlaneData[0].airPlanes[EnumsFolderPlaneName];
        planename = dataFile.Name;
        fireRate = dataFile.FireRate;
        isPropellerActive = dataFile.IsPropellerActive;
        movementSpeed = dataFile.MovementSpeed;
        turnSpeed = dataFile.TurnSpeed;
        airPlanePrefabData = dataFile.AirPlanePrefabData;
        airPlaneBombData = dataFile.AirPlaneBombData;
    }
    public void SetMoney()
    {
        UIManager.SetMoneyPanel(Money);
    }

   
}
