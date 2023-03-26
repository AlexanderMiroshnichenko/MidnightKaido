using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    private void Awake()
    {
        UpdateMoney();
    }
    public PlayerData playerData;
    public TMP_Text moneyText;
    public void UpdateMoney()
    {
        moneyText.text = playerData.playerMoney.ToString();
    }

}
