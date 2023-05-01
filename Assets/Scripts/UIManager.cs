using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI moneyPanel;
    public GameObject upgradePanels;

    public delegate void setMoney(int money);
    public static setMoney SetMoneyPanel;

    public List<Button> powerUpButtons;

    public delegate void upgradePanel(bool isActive);
    public static upgradePanel UpgradePanel;
    public delegate void upgradeButtonChangeActive(int number, bool isActive);
    public static upgradeButtonChangeActive UpgradeButtonChangeActive;
    
    private void Start()
    {
        SetMoneyPanel += ChangeMoneyPanel;
        UpgradePanel += IsUpgradePanelActive;
        UpgradeButtonChangeActive += ChangeUpgradeButtonVisible;


    }
    
    private void ChangeMoneyPanel(int money)
    {
        moneyPanel.text = money.ToString();
    }
    public void ChangePropertie(string name)
    {
        PlayerController.PowerUp(name);
    }

    private void IsUpgradePanelActive(bool isActive)
    {
        upgradePanels.SetActive(isActive);
    }
    private void ChangeUpgradeButtonVisible(int number, bool isActive)
    {
        powerUpButtons[number].interactable = isActive;

    }
    private void OnDisable()
    {
        SetMoneyPanel -= ChangeMoneyPanel;
        UpgradePanel -= IsUpgradePanelActive;
        UpgradeButtonChangeActive -= ChangeUpgradeButtonVisible;
    }

}
