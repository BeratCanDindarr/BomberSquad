using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DetectEnemy : MonoBehaviour
{
    int triggeredEnemy=0;
    public List<GameObject> triggeredEnemyList;
    Image cursorImage ;


    public delegate void objectDestroy(GameObject obj);
    public static objectDestroy ObjectDestroyed;
    private void Start()
    {
        ObjectDestroyed += ObjectDestroy;
        cursorImage = gameObject.GetComponent<Image>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            checkedTriggeredEnemy(true, Color.red);
            triggeredEnemy++;
            triggeredEnemyList.Add(other.gameObject);
        }
        if (other.gameObject.CompareTag("Coin"))
        {
            GameManager.Instance.Money += 10;
            GameManager.Instance.SetMoney();
            PoolManager.SetObject((int)EnumsFolder.PoolObjectName.COIN,(int)EnumsFolder.Coin.COIN1,other.gameObject);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            triggeredEnemy--;
            triggeredEnemyList.Remove(other.gameObject);
            checkedTriggeredEnemy(false,Color.white);
        }
    }
    private bool CheckedFirstTriggered()
    {
        bool checkedTriggeredEnemey = false;
        if (triggeredEnemy == 0)
        {

            checkedTriggeredEnemey = true;
        }
        return checkedTriggeredEnemey;
    }
    private void ObjectDestroy(GameObject obj)
    {
        if (CheckList(obj))
        {

            triggeredEnemyList.Remove(obj);
            triggeredEnemy--;
            checkedTriggeredEnemy(false,Color.white);
        }
    }
    private bool CheckList(GameObject enemy)
    {
        bool finded = false;
        for (int i = 0; i < triggeredEnemyList.Count; i++)
        {
            if (enemy == triggeredEnemyList[i])
            {
                finded = true;
            }
        }
        return finded;
    }
    void checkedTriggeredEnemy(bool actived, Color color)
    {
        if (CheckedFirstTriggered())
        {
            PlayerController.Attack(actived);
            ChangedCursorColors(color);
        }
    }
    private void ChangedCursorColors(Color _color)
    {
        cursorImage.color = _color; 
    }
    private void OnDisable()
    {
        ObjectDestroyed -= ObjectDestroy;
    }
}
