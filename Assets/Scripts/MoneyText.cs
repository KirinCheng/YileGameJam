using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour
{
    [SerializeField]
    private Text moneyText;

    public void RefreshMoney(int getValue)
    {
        moneyText.text = getValue.ToString();
    }
}
