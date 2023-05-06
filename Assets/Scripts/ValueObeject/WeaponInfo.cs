using UnityEngine;

[CreateAssetMenu(fileName = "WeaponInfoSetting", menuName = "Create Data Weapon")]
public class WeaponInfoSetting : ScriptableObject
{
    public WeaponInfo[] weaponInfo;
}

[System.Serializable]
public class WeaponInfo
{
    [Header("武器編號")]
    public int weaponNumber;
    [Header("名稱")]
    public string weaponName;
    [Header("圖片")]
    public Sprite weaponSprite;
    [Header("詞綴")]
    public string weaponSubtitle;
    [Header("耐久")]
    public int totalDurability;
    [Header("每次攻擊消耗耐久")]
    public int injureDurabilityPerAttack;
    [Header("攻擊力")]
    public int attack;
    [Header("攻擊範圍")]
    public int attackRange;
    [Header("擊退力")]
    public int backOffPower;
}
