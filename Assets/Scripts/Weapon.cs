using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected SpriteRenderer weaponSpriteRenderer;
    [SerializeField]
    protected Sprite weaponLv1;
    [SerializeField]
    protected Sprite weaponLv2;
    [SerializeField]
    protected Sprite weaponLv3;

    public void SetSprite(int level)
    {
        Sprite weaponSprite = weaponLv1;
        switch (level)
        {
            case 0:
                weaponSprite = weaponLv1;
                break;
            case 1:
                weaponSprite = weaponLv2;
                break;
            case 2:
                weaponSprite = weaponLv3;
                break;
        }
        weaponSpriteRenderer.sprite = weaponSprite;
    }
}
