using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    #region Visible
    [Header("Plane Movement")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float fireRate;
    [SerializeField] private float damage = 100;

    [Header("Bomb Name")]
    [SerializeField] private EnumsFolder.Bombs BombPrefabs;
    [Header("Joystick")]
    [SerializeField] private FloatingJoystick joystick;
    
    [Header("Plane Mesh")]
    [SerializeField]private GameObject planeBase;
    #endregion

    public int maxLevel;
    public int money = 0;
    #region Private 
    private GameManager gameManager;
    private Rigidbody playerRB;
    private bool isAttacking = false;
    private bool isAttackWaiting = false;
    private bool isLifting = false;
    private bool waitLandingAnim = false;
        #region movement 
        private float horizontal;
        private float vertical;
    #endregion

    [Header("Plane Properties Level")]
    [SerializeField]private int movementLevel =1;
    [SerializeField] private int fireRateLevel = 1;
    [SerializeField] private int damageLevel=1;

    #endregion
    private AirPlaneData.AirPlanes playerData;
    #region Delegate
    public delegate void attack(bool isActive);
    public delegate void outsideTheBase(bool isActive);
    public delegate void powerUp(string name);
    public delegate float getDamage();
    #endregion
    #region Static Event
    public static attack Attack;
    public static outsideTheBase OutsideTheBase;
    public static powerUp PowerUp;
    public static getDamage GetDamage;
    #endregion
    
    void Start()
    {
        //gameManager = GameManager.Instance;
        playerRB = GetComponent<Rigidbody>();
        Attack += AirPlaneAttack;
        LandingAndLifting.PlayerBaseAnim += PlayerAnimWaiting;
        OutsideTheBase += IsLifting;
        PowerUp += ChangeProperties;
        GetDamage += ReturnDamage;
        playerData = GameManager.ReturnAirPlaneData();
        GetPlayerData();
        CheckMoney();
        CheckUpgradePanel();
        damage = 100;
        maxLevel = 5;
    }

   

    private void GetPlayerData() 
    {

        movementSpeed = playerData.MovementSpeed;
        turnSpeed = playerData.TurnSpeed;
        BombPrefabs = playerData.AirPlaneBombData;
        fireRate = playerData.FireRate;
        joystick = GameManager.GetJoystick();
    }
    private void Update()
    {
        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;
        


    }
    private void CheckUpgradePanel()
    {
        int result = movementLevel * 10;

        if (movementLevel > maxLevel || money < result)
        {
            UIManager.UpgradeButtonChangeActive(0, false);
        }
        else
        {
            UIManager.UpgradeButtonChangeActive(0, true);
        }
        result = fireRateLevel * 10;
        if (fireRateLevel > maxLevel || money < result)
        {
            UIManager.UpgradeButtonChangeActive(1, false);
        }
        else
        {
            UIManager.UpgradeButtonChangeActive(1, true);
        }
        result = damageLevel * 10;
        if (damageLevel > maxLevel || money < result)
        {
            UIManager.UpgradeButtonChangeActive(2, false);
        }
        else
        {
            UIManager.UpgradeButtonChangeActive(2, true);
        }

    }
    
    private void FixedUpdate()
    {
        if (waitLandingAnim)
        {
            PlaneBaseAnim(0);   
            return;
        }
       
        if (isLifting )
        {
            
            MoveCharecter();
            if (horizontal != 0 || vertical != 0)
                rotateCharacter();
            else
                PlaneBaseAnim(0);
        }
        else
        {

            if (horizontal != 0 || vertical != 0)
            {
                LandingAndLifting.PlayerBaseAnim((int)EnumsFolder.PlaneLandingOrLiftingAnim.LIFTING);
                OutsideTheBase(true);
            }
        }
    }

    /*<summary> 
     * 
     * </summary>
     */
    private void MoveCharecter()
    {
      
        playerRB.velocity = transform.forward * movementSpeed * Time.deltaTime;
    }

    /*<summary> Code is airplane forward change and play the animation of airplane turns </summary>*/
    private void rotateCharacter()
    {
        RotateCheckPlaneAnim();
        transform.forward = Vector3.Lerp(transform.forward, new Vector3(horizontal,transform.forward.y,vertical), Time.fixedDeltaTime * turnSpeed);
    }
    /*<summary>
     * The code controls that which way the airplane turns
     * </summary>
     */
    private void RotateCheckPlaneAnim()
    {
        Vector3 targetPoint = new Vector3(horizontal, transform.forward.y, vertical);
        Vector3 delta = (targetPoint - gameObject.transform.forward).normalized;
        Vector3 cross = Vector3.Cross(delta, gameObject.transform.forward);
        if (cross != Vector3.zero)
        {
            if (cross.x > 0)
            {
                if (cross.y > 0)
                {
                    PlaneBaseAnim(-45);
                }
                else
                {
                    PlaneBaseAnim(45);
                }
            }
            else
            {
                if (cross.y > 0)
                {
                    PlaneBaseAnim(45);
                }
                else
                {
                    PlaneBaseAnim(-45);
                }
            }

        }
    }
    /*<summary>
     * Plane right or left rotate anim
     * </summary>
     * <param name= "angle"> Linearly rotate airplane to  the incoming paremeter </param>
     */
    private void PlaneBaseAnim(float angle)
    {
        planeBase.transform.localRotation = Quaternion.Slerp(planeBase.transform.localRotation, Quaternion.Euler(Vector3.forward * angle), Time.fixedDeltaTime);
    }

    private void AirPlaneAttack(bool isActive)
    {
        if (isAttackWaiting)
        {
            return;
        }
        isAttacking = isActive;
        if (isAttacking) 
            StartCoroutine(BombCreate());

    }
    IEnumerator BombCreate()
    {
        isAttackWaiting = true;
        yield return new WaitForSeconds(fireRate);
        
        SpawnBomb();
        isAttackWaiting = false;


    }
    private void SpawnBomb()
    {
        if (isAttacking)
        {
            GameObject bomb = PoolManager.ReturnObject((int)EnumsFolder.PoolObjectName.BOMB, (int)BombPrefabs);
            bomb.transform.position = gameObject.transform.position;
            bomb.SetActive(true);
            StartCoroutine(BombCreate());
        }
    }
    private void IsLifting(bool isActive)
    {
        isLifting = isActive;
        playerRB.isKinematic = !isActive;
        UIManager.UpgradePanel(!isActive);
    }
    private void PlayerAnimWaiting(int PlayerLandingOrLifting)
    {
        CheckUpgradePanel();
        CheckMoney();
        if (PlayerLandingOrLifting == 0)
        {
            StartCoroutine(WaitLandingAnim());
            
        }
    }
    IEnumerator WaitLandingAnim()
    {
        waitLandingAnim = true;
        yield return new WaitForSeconds(3f);
        waitLandingAnim = false;
    }
    private void ChangeProperties(string changedProperties)
    {
        switch (changedProperties)
        {
            case "Movement":
                GameManager.SetPlayerMoney(movementLevel*10 *-1);
                movementLevel++;
                movementSpeed++;
                break;
            case "FireRate":
                GameManager.SetPlayerMoney(fireRateLevel * 10 * -1);
                fireRateLevel++;
                fireRate -=   0.1f;
                break;
            case "Damage":
                GameManager.SetPlayerMoney(damageLevel * 10 * -1);
                damageLevel++;
                damage += damageLevel * 10;
                break;
            default:
                break;
        }
        CheckMoney();
        CheckUpgradePanel();
    }
    private float ReturnDamage()
    {
        return damage;
    }
    private void CheckMoney()
    {

        money = GameManager.GetMoney();
    }

    private void OnDestroy()
    {
        Attack -= AirPlaneAttack;
        LandingAndLifting.PlayerBaseAnim -= PlayerAnimWaiting;
        OutsideTheBase -= IsLifting;
        PowerUp -= ChangeProperties;
        GetDamage -= ReturnDamage;
    }
}
