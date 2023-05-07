using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private GateHpText gateHpText;
    [SerializeField]
    private MoneyText moneyText;
    [SerializeField]
    private DayText dayText;
    [SerializeField]
    private Megami megami;
    [SerializeField]
    private MonsterSpawnPointController monterSpawner;
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private AnnouncementText announcement;
    [SerializeField]
    private GotWeaponView gotWeaponView;

    private WeaponGenerator weaponGenerator = new WeaponGenerator();

    public int day;
    public int money;
    [SerializeField]
    private int curGateHp;
    [SerializeField]
    private int totalGateHp;

    private void Start()
    {
        ResetUI();
        ResetGame();
    }

    void Update()
    {
        
    }

    public void AttackGate(int value = 1)
    {
        curGateHp -= value;
        gateHpText.RefreshGateHpUi(curGateHp,totalGateHp);
        if (curGateHp <= -1)
            DayEnd();
    }
    public void SetMoney(int value)
    {
        money += value;
        moneyText.RefreshMoney(money);
    }
    public void PrayOrPayTheMegami(int payMoney)
    {
        SetMoney(-payMoney);
        var weaponInfo = weaponGenerator.Generate(payMoney);
        player.GetWeapon(weaponInfo);
        var weaponName = weaponInfo.weaponSubtitle + weaponInfo.weaponName;
        gotWeaponView.Show(weaponName, GameStartComplete);
    }
    private void GameStartComplete()
    {
        string dayString = "Day." + day ;
        CallAnnounce(dayString, 3);
        monterSpawner.MonsterHasSpawn();
    }

    public void CallAnnounce(string text,float duration)
    {
        announcement.Announce(text,duration);
    }

    public void DayEnd()
    {
        CallAnnounce("村莊爆開了 你的城門耐久比0還低", 5);
        player.ForceStopByManager();
        monterSpawner.ForceStop();
        timer.TimerCountDown(5,()=> {
            announcement.Close();
            ResetUI();
            ResetGame();
        });
    }

    public void ResetUI()
    {
        day++;
        dayText.RefreshDay(day);
        totalGateHp = 10;
        curGateHp = totalGateHp;
        gateHpText.RefreshGateHpUi(curGateHp,totalGateHp);
        if (money <= 0)
            money = 1;
        moneyText.RefreshMoney(money);
        megami.SetSlide(money);
        megami.Show();
        announcement.Close();
        gotWeaponView.Hide();
    }
    public void ResetGame()
    {
        player.ResetPlayer();
        monterSpawner.MonsterSpawnerReset();
    }
}
