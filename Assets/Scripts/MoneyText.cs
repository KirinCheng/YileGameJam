using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text moneyText;

    public void RefreshMoney(int getValue)
    {
        moneyText.text = getValue.ToString() + "$";
    }
}
