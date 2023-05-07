using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateHpText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text gateHpText;

    public void RefreshGateHpUi(int cur, int total)
    {
        gateHpText.text = "城門耐久: " + cur.ToString() + " / " + total.ToString();
    }
}
