using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthSystem : MonoBehaviour, IDamage
{
    public Slider playerHealthBar;
    public GameObject smokeArea;

    private int regenerateHealtSize= 5;
    private AirPlaneData.AirPlanes activePlaneData;
    private float maxHealth = 100;
    private float currentHealth = 100;
    private GameObject effect;
    private bool isDamagedAirPlane = false;
    private bool refullHealth = false;
    private Camera camera;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ReturnPlayerData());
        LandingAndLifting.PlayerBaseAnim += LandingCheck;
        camera = Camera.main;
    }
    IEnumerator ReturnPlayerData()
    {
        yield return new WaitForSeconds(2f);
        activePlaneData = GameManager.ReturnAirPlaneData();
        maxHealth = activePlaneData.Health;
        currentHealth = maxHealth;
        playerHealthBar.maxValue = maxHealth;
        effect = PoolManager.ReturnObject((int)EnumsFolder.PoolObjectName.PARTICALS, (int)EnumsFolder.Partical.SMOKE);
        effect.transform.parent = smokeArea.transform;
        effect.transform.position = smokeArea.transform.position;
        effect.SetActive(false);
        SetHealthBar(currentHealth);
    }


    private void SetHealthBar(float health)
    {
        playerHealthBar.value = health;
    }
    // Update is called once per frame

    public void Damage(float damage)
    {
        currentHealth -= damage;
        SetHealthBar(currentHealth);
        CheckDeath(currentHealth);
    }
    

    private void LandingCheck(int isLanding)
    {
        if (isLanding == 0)
        {
            refullHealth = true;
        }
        else
        {
            refullHealth = false;
        }
    }
    private void Update()
    {
        if (refullHealth && currentHealth < maxHealth)
        {
            currentHealth += Time.deltaTime * regenerateHealtSize;
            SetHealthBar(currentHealth);
            CheckDeath(currentHealth);
        }
        playerHealthBar.transform.LookAt(camera.transform);

    }
    public void CheckDeath(float health)
    {
        if (health < 50 && !isDamagedAirPlane)
        {
            effect.SetActive(true);
            isDamagedAirPlane = true;
        }
        else if ( health >= 50 && isDamagedAirPlane)
        {
            effect.SetActive(false);
            isDamagedAirPlane = false;
        }
        if (health <= 0)
        {
            //Death Anim
        }
    }
    private void OnDisable()
    {
        LandingAndLifting.PlayerBaseAnim -= LandingCheck;
    }
}
