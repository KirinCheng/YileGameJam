using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnPointController : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private MonsterController monsterPrefab;
    [SerializeField]
    private WeaponInfoSetting monsterWeaponInfo;
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private float spawnTimeDefaultInterval;
    private float spawnTimeInterval;

    private List<MonsterController> monsters = new List<MonsterController>();

    public void MonsterHasSpawn()
    {
        InvokeRepeating("SpawnMonster", spawnTimeInterval, spawnTimeInterval);
    }
    public void MonsterStopSpawn()
    {
        spawnTimeInterval = spawnTimeDefaultInterval;
        CancelInvoke();
    }

    private void SpawnMonster()
    {
        MonsterController monster = Instantiate(monsterPrefab, transform);
        monster.transform.position = transform.position;
        monster.SetAction(MonsterDieCallback);
        monster.GetWeapon(monsterWeaponInfo.weaponInfo);
        monsters.Add(monster);
    }

    private void MonsterDieCallback(MonsterController monster)
    {
        if(monster.dropMoney > 0)
            gameManager.SetMoney(monster.dropMoney);
        else
            gameManager.AttackGate();
        monsters.Remove(monster);
        DestroyImmediate(monster.gameObject);
    }

    public void MonsterSpawnerReset()
    {
        foreach (var monster in monsters)
        {
            DestroyImmediate(monster.gameObject);
        }
        monsters = new List<MonsterController>();
        MonsterStopSpawn();
    }

    public void ForceStop()
    {
        CancelInvoke();
        foreach (var monster in monsters)
        {
            monster.ForceStopByManager();
        }
    }
}
