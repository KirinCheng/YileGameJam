using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnnouncementText : MonoBehaviour
{
    [SerializeField]
    private GameObject backImage;
    [SerializeField]
    private TMP_Text tmpText;
    [SerializeField]
    private Timer timer;


    public void Announce(string text, float duration)
    {
        backImage.SetActive(true);
        tmpText.text = text;
        timer.TimerCountDown(duration, End);
    }
    private void End()
    {
        backImage.SetActive(false);        
    }


    public void Close()
    {
        timer.StopTimer();
        End();
    }
}
