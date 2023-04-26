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

    [Header("Bomb Name")]
    [SerializeField] private EnumsFolder.Bombs BombPrefabs;
    [Header("Joystick")]
    [SerializeField] private FloatingJoystick joystick;
    
    [Header("Plane Mesh")]
    [SerializeField]private GameObject planeBase;
    #endregion

    #region Private 
    private GameManager gameManager;
    private Rigidbody playerRB;
    private bool isAttacking = false;
        #region movement 
        private float horizontal;
        private float vertical;
        #endregion
    #endregion

    #region Delegate
    public delegate void attack(bool actived);
    #endregion
    #region Static Event
    public static attack Attack;
    #endregion
    
    void Start()
    {
        gameManager = GameManager.Instance;
        playerRB = GetComponent<Rigidbody>();
        Attack += AirPlaneAttack;
        GetPlayerData();
    }

    void GetPlayerData() 
    {
        movementSpeed = gameManager.movementSpeed;
        turnSpeed = gameManager.turnSpeed;
        BombPrefabs = gameManager.airPlaneBombData;
        fireRate = gameManager.fireRate;
        joystick = gameManager.joystick;
    }
    private void Update()
    {
        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;   
    }
    
    private void FixedUpdate()
    {
        MoveCharecter();
        if (horizontal != 0 || vertical != 0)
            rotateCharacter();
        else
            PlaneBaseAnim(0);
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

    private void AirPlaneAttack(bool actived)
    {
        isAttacking = actived;
        if (isAttacking) 
            StartCoroutine(BombCreate());

    }
    IEnumerator BombCreate()
    {
        
        yield return new WaitForSeconds(fireRate);
        
        SpawnBomb();
        
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

    private void OnDestroy()
    {
        Attack -= AirPlaneAttack;
    }
}
