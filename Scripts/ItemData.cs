using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item",menuName ="Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum eItemType 
    {
        Orb,Aura,Ice_Bolt,Heal,Shoe,Shield
    }

    [Header("# item Info")]
    public eItemType itemType;
    public string itemName;
    public int itemId;
    public Sprite bgIcon;
    public Sprite itemIcon;

    [Header("# level Data")]
    [TextArea]
    public string[] descs;
    public float baseDamage;
    public int baseCount;
    public float baseSize;
    public float baseSpeed;
    public float coolTime;
    public float runTime;
    public float[] damages;
    public int[] counts;
    public float[] sizes;
    public float[] speeds;

    //¹«±â Á¤º¸
    [Header("# Weapon")]
    public GameObject projectile;
}
