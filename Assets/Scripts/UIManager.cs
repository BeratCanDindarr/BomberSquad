using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI moneyPanel;

    public delegate void setMoney(int money);
    public static setMoney SetMoneyPanel;

    private void Start()
    {
        SetMoneyPanel += ChangeMoneyPanel;
    }
    private void ChangeMoneyPanel(int money)
    {
        moneyPanel.text = money.ToString();
    }
    private void OnDisable()
    {
        SetMoneyPanel -= ChangeMoneyPanel;
    }

}
