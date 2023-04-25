using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DetectEnemy : MonoBehaviour
{
    int triggeredEnemy=0;
    
    Image cursorImage ;

    private void Start()
    {
        cursorImage = gameObject.GetComponent<Image>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (CheckedFirstTriggered())
            {
                PlayerController.Attack(true);
                ChangedCursorColors(Color.red);
            }
            triggeredEnemy++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            triggeredEnemy--;
            if (CheckedFirstTriggered())
            {
                PlayerController.Attack(false);
                ChangedCursorColors(Color.white);
            }
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
    private void ChangedCursorColors(Color _color)
    {
        cursorImage.color = _color; 
    }
}
