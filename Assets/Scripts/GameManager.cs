using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GateHpText gateHpText;
    [SerializeField]
    private MoneyText moneyText;
    [SerializeField]
    private Megami megami;
    [SerializeField]
    private MonsterSpawnPointController monterSpawner;

    public int money;
    [SerializeField]
    private int curGateHp;
    [SerializeField]
    private int totalGateHp;

    void Start()
    {
        ResetUI();
    }

    void Update()
    {
        
    }

    public void AttackGate(int value = 1)
    {
        curGateHp -= value;
        gateHpText.RefreshGateHpUi(curGateHp,totalGateHp);
    }
    public void SetMoney(int value)
    {
        money += value;
        moneyText.RefreshMoney(money);
    }
    public void PrayOrPayTheMegami(int payMoney)
    {
        SetMoney(-payMoney);
        monterSpawner.MonsterHasSpawn();
    }

    public void ResetUI()
    {
        totalGateHp = 10;
        curGateHp = totalGateHp;
        gateHpText.RefreshGateHpUi(curGateHp,totalGateHp);

        moneyText.RefreshMoney(money);

        megami.SetSlide();
    }
}
