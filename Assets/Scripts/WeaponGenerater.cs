using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeaponGenerator
{

    public List<WeaponJsonData> weaponJsonDatas { get; set; }

    public WeaponGenerator()
    {
        string json = @"[{
                            'bottom':0,
                         'upper':40,
                            'name': '鹹魚',
                            'injureDurabilityPerAttack':1,
                            'durabilityWeights': [
                              { 'prefix':'被咬一口的','value':10,'weight': 100, 'price': 1 , 'levelValue':1}, 
                              { 'prefix':'剛醃不久的','value':20,'weight': 90, 'price': 5, 'levelValue': 2}, 
                              { 'prefix':'放一段時間的','value':30,'weight': 80, 'price': 10, 'levelValue': 3},
                              { 'prefix':'冷凍的','value':40,'weight': 70, 'price': 15, 'levelValue': 4},
                              { 'prefix':'82年的','value':50,'weight': 60, 'price': 20, 'levelValue': 5}
                            ],
                            'attackWeights': [
                              { 'prefix':'辣的','value':11,'weight': 20, 'price': 20, 'levelValue': 5}, 
                              { 'prefix':'酸的','value':8,'weight': 30, 'price': 15, 'levelValue': 4}, 
                              { 'prefix':'苦的','value':7,'weight': 40, 'price': 10, 'levelValue': 3},
                              { 'prefix':'濕潤的','value':5,'weight': 50, 'price': 5, 'levelValue': 2},
                              { 'prefix':'腐爛的','value':3,'weight': 60, 'price': 1 , 'levelValue':1}
                            ]
                          },
                          {
                            'bottom':40,
                         'upper':999999999,
                            'name': '金槍魚',
                            'injureDurabilityPerAttack':1,
                            'durabilityWeights': [
                              { 'prefix':'壞掉的','value':20,'weight': 100, 'price': 40, 'levelValue': 3}, 
                              { 'prefix':'新鮮的','value':30,'weight': 90, 'price': 50, 'levelValue': 5}, 
                              { 'prefix':'剛進冰庫的','value':40,'weight': 80, 'price': 60, 'levelValue': 8},
                              { 'prefix':'冷凍的','value':50,'weight': 70, 'price': 70, 'levelValue': 11},
                              { 'prefix':'82年的','value':60,'weight': 60, 'price': 80, 'levelValue': 14}
                            ],
                            'attackWeights': [
                              { 'prefix':'辣的','value':50,'weight': 40, 'price': 80, 'levelValue': 14}, 
                              { 'prefix':'酸的','value':40,'weight': 60, 'price': 70, 'levelValue': 11}, 
                              { 'prefix':'苦的','value':30,'weight': 80, 'price': 60, 'levelValue': 8},
                              { 'prefix':'濕潤的','value':20,'weight': 100, 'price': 50, 'levelValue': 5},
                              { 'prefix':'腐爛的','value':6,'weight': 120, 'price': 40, 'levelValue': 3}
                            ]
                          }]";

        List<WeaponJsonData> weaponJsonDatas2 = JsonConvert.DeserializeObject<List<WeaponJsonData>>(json);

        this.weaponJsonDatas = weaponJsonDatas2;
    }
    public int FindLevel(int totalWeight)
    {
        if (totalWeight > 0 && totalWeight <= 100)
            return 2;

        if (totalWeight > 100 && totalWeight <= 130)
            return 1;

        if (totalWeight > 130 && totalWeight <= 999999)
            return 0;

        return 0;
    }
    public AttackWeight GetAttack(WeaponJsonData weaponJsonData, Price newPrice)
    {
        if (newPrice.money < 1)
            return new AttackWeight();
        List<AttackWeight> validAttackWeights = weaponJsonData.attackWeights.Where(w => w.price <= newPrice.money).ToList();
        int totalWeight = validAttackWeights.Sum(w => w.weight);
        int randomNum = Random.Range(1, totalWeight + 1);
        int cumulativeWeight = 0;

        foreach (AttackWeight attackWeight in validAttackWeights)
        {
            cumulativeWeight += attackWeight.weight;
            if (randomNum <= cumulativeWeight)
            {
                newPrice.money -= attackWeight.price;
                return attackWeight;
            }
        }

        return new AttackWeight();
    }

    public DurabilityWeight GetDurability(WeaponJsonData weaponJsonData, Price newPrice)
    {
        if (newPrice.money < 1)
            return new DurabilityWeight();
        List<DurabilityWeight> validDurabilityWeights = weaponJsonData.durabilityWeights.Where(w => w.price <= newPrice.money).ToList();
        int totalWeight = validDurabilityWeights.Sum(w => w.weight);
        int randomNum = Random.Range(1, totalWeight + 1);
        int cumulativeWeight = 0;

        foreach (DurabilityWeight durabilityWeight in validDurabilityWeights)
        {
            cumulativeWeight += durabilityWeight.weight;
            if (randomNum <= cumulativeWeight)
            {
                return durabilityWeight;
            }
        }

        return new DurabilityWeight();
    }

    public WeaponJsonData FindRange(int price)
    {
        foreach (WeaponJsonData weaponJsonData in weaponJsonDatas)
        {
            if (price > weaponJsonData.bottom && price <= weaponJsonData.upper)
            {
                return weaponJsonData;
            }
        }

        return weaponJsonDatas[0]; // 沒有符合條件的物件
    }

    public WeaponInfo Generate(int price)
    {
        Price newPrice = new Price();
        newPrice.money = price;
        WeaponJsonData weaponJsonData = FindRange(newPrice.money);
        AttackWeight attackWeight = GetAttack(weaponJsonData, newPrice);
        WeaponJsonData weaponJsonDataSecond = FindRange(newPrice.money);
        DurabilityWeight durabilityWeight = GetDurability(weaponJsonDataSecond, newPrice);
        WeaponInfo weaponInfo = new WeaponInfo();
        weaponInfo.isBroken = false;
        weaponInfo.weaponNumber = 0;
        weaponInfo.weaponName = weaponJsonData.name;
        weaponInfo.weaponSubtitle = attackWeight.prefix + durabilityWeight.prefix;
        weaponInfo.totalDurability = durabilityWeight.value;
        weaponInfo.injureDurabilityPerAttack = weaponJsonData.injureDurabilityPerAttack;
        weaponInfo.attack = attackWeight.value;
        weaponInfo.attackRange = 2;
        weaponInfo.attackDuration = 0.4f;
        weaponInfo.backOffPower = 8;
        weaponInfo.level = FindLevel(attackWeight.weight + durabilityWeight.weight);

        return weaponInfo;
    }
}

public class WeaponJsonData
{
    public int bottom { get; set; }
    public int upper { get; set; }
    public string name { get; set; }
    public int injureDurabilityPerAttack { get; set; }
    public List<DurabilityWeight> durabilityWeights { get; set; }
    public List<AttackWeight> attackWeights { get; set; }
}

public class Price
{
    public int money { get; set; }
}

public class DurabilityWeight
{
    public string prefix { get; set; }
    public int value { get; set; }
    public int weight { get; set; }
    public int price { get; set; }
    public int levelValue { get; set; }

    public DurabilityWeight()
    {
        this.prefix = "被咬一口的";
        this.value = 1;
    }
}

public class AttackWeight
{
    public string prefix { get; set; }
    public int value { get; set; }
    public int weight { get; set; }
    public int price { get; set; }
    public int levelValue { get; set; }

    public AttackWeight()
    {
        this.prefix = "鏽蝕的";
        this.value = 1;
    }
}
