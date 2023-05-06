using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateHpText : MonoBehaviour
{
    [SerializeField]
    private Text gateHpText;

    public void RefreshGateHpUi(int cur, int total)
    {
        gateHpText.text = cur.ToString() + " / " + total.ToString();
    }
}
