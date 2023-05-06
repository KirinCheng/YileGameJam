using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnPointController : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private MonsterController monsterPrefab;
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private float spawnTimeInterval;

    private List<MonsterController> monsters = new List<MonsterController>();


    void Start()
    {
        
    }

    public void MonsterHasSpawn()
    {
        InvokeRepeating("SpawnMonster", spawnTimeInterval, spawnTimeInterval);
    }

    private void SpawnMonster()
    {
        MonsterController monster = Instantiate(monsterPrefab, transform);
        monster.transform.position = transform.position;
        monster.SetAction(MonsterDieCallback);
        monsters.Add(monster);
    }

    private void MonsterDieCallback(MonsterController monster)
    {
        gameManager.AttackGate();
        monsters.Remove(monster);
        DestroyImmediate(monster.gameObject);
    }
}
