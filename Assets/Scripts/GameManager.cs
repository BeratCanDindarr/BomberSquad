using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public static GameManager Instance;
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
    private AirPlaneData.AirPlanes activeAirPlaneData;
    #endregion
    public Transform BaseLocation;
    public GameObject airPlanePrefab;
    public Cinemachine.CinemachineVirtualCamera cinemachineCamera;
    
    
    #region PlayerController
    public FloatingJoystick joystick;
    #endregion

    #region Properties
    private int money=0;
    public int Money { get { return money; } set { money = value; } }
    #endregion

    #region Delegates
    public delegate void createdPlayer(Transform player);
    public static createdPlayer CreatedPlayer;
    public delegate AirPlaneData.AirPlanes returnAirPlaneData();
    public static returnAirPlaneData ReturnAirPlaneData;
    public delegate FloatingJoystick getJoystick();
    public static getJoystick GetJoystick;
    public delegate int getMoney();
    public static getMoney GetMoney;
    public delegate void setMoney(int _money);
    public static setMoney SetPlayerMoney;
    public delegate Transform getPlayerPrefab();
    public static getPlayerPrefab GetPlayerPrefab;
    #endregion
    public LandingAndLifting lifting;

    void Awake()
    {
        //Instance = this;
        SetPlayerPlane((int)EnumsFolder.Plane.PLANE1);
    }
    private void Start()
    {
        PlayerSpawn();
        ReturnAirPlaneData += ReturnPlayerData;
        GetJoystick += ReturnJoystick;
        GetMoney += ReturnMoney;
        SetPlayerMoney += SetMoney;
        GetPlayerPrefab += ReturnPlayer;
    }

    private void PlayerSpawn()
    {
        airPlanePrefab=PoolManager.ReturnObject((int)EnumsFolder.PoolObjectName.PLANE, (int)airPlanePrefabData);
        airPlanePrefab.transform.position = BaseLocation.position;
        CameraController.AddCameraFollow(airPlanePrefab);
        //lifting.GetPlayerTransform(airPlanePrefab.transform);
        CreatedPlayer(airPlanePrefab.transform);
    }
    
    void SetPlayerPlane(int EnumsFolderPlaneName)
    {
        activeAirPlaneData = airPlaneData[0].airPlanes[EnumsFolderPlaneName];
        planename = activeAirPlaneData.Name;
        fireRate = activeAirPlaneData.FireRate;
        isPropellerActive = activeAirPlaneData.IsPropellerActive;
        movementSpeed = activeAirPlaneData.MovementSpeed;
        turnSpeed = activeAirPlaneData.TurnSpeed;
        airPlanePrefabData = activeAirPlaneData.AirPlanePrefabData;
        airPlaneBombData = activeAirPlaneData.AirPlaneBombData;
    }
    AirPlaneData.AirPlanes ReturnPlayerData()
    {

        return activeAirPlaneData;
    }
    
    private void SetMoney(int _money)
    {
        money += _money;
        UIManager.SetMoneyPanel(Money);
    }
    public int ReturnMoney()
    {
        return Money;
    }
    private FloatingJoystick ReturnJoystick()
    {
        return joystick;
    }
    private Transform ReturnPlayer()
    {
        return airPlanePrefab.transform;
    }
    private void OnDestroy()
    {
        ReturnAirPlaneData -= ReturnPlayerData;
        GetJoystick -= ReturnJoystick;
        GetMoney -= ReturnMoney;
        SetPlayerMoney -= SetMoney;
        GetPlayerPrefab -= ReturnPlayer;
    }

}
