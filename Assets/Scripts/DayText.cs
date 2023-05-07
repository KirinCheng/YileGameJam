using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text dayText;

    public void RefreshDay(int dayValue)
    {
        dayText.text = "Day. " + dayValue.ToString();
    }
}
