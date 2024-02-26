using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;


public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;
    public Gear gear;

    private Text txtName;
    private Text txtDesc;
    private Text txtLevel;
    private Image icon;
    private Image bg;

    void Awake()
    {
        //GetComponentsInchildren에서 2번째 값으로 가져오기(1번째 : 자기자신)
        this.bg = this.GetComponentsInChildren<Image>()[1];
        this.icon = this.GetComponentsInChildren<Image>()[2];

        //아이콘 넣어주기
        this.icon.sprite = data.itemIcon;
        this.bg.sprite = data.bgIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        this.txtLevel = texts[0];
        this.txtName = texts[1];
        this.txtDesc = texts[2];

        //이름 넣어주기
        this.txtName.text = data.itemName;
    }

    //활성화시 동작
    void OnEnable()
    {
        //레벨 1부터 시작
        txtLevel.text = string.Format("Lv.{0}", (level + 1).ToString());
        switch (data.itemType) 
        {
            case ItemData.eItemType.Orb:
                txtDesc.text = string.Format(data.descs[level], data.damages[level] * 100, data.counts[level],data.speeds[level] * 100);
                break;
            case ItemData.eItemType.Aura:
                txtDesc.text = string.Format(data.descs[level], data.damages[level] * 100, data.sizes[level] * 100);
                break;
            case ItemData.eItemType.Ice_Bolt:
                //..count 뺀 나머지 --> 백분율
                txtDesc.text = string.Format(data.descs[level], data.damages[level] * 100, data.counts[level]);
                break;
            case ItemData.eItemType.Heal:
            case ItemData.eItemType.Shoe:
            case ItemData.eItemType.Shield:
                //모두 --> 백분율
                txtDesc.text = string.Format(data.descs[level],data.damages[level]*100);
                break;
        }

    }

    public void OnClick()
    {
        switch (data.itemType) 
        {
            case ItemData.eItemType.Orb:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    this.weapon = newWeapon.AddComponent<Weapon>();
                    //Weapon 초기화
                    this.weapon.Init(data);
                    this.weapon.transform.Rotate(new Vector3(90, 0, 0));
                }
                else
                {
                    //...처음 이후의 레벨업은 데미지와 횟수를 계산
                    //새로운 무기이면 damage 올려줘야함
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;
                    float nextSpeed = data.baseSpeed;

                    //damages가 백분률 값이므로 곱해준 다음 더해줌
                    nextDamage += data.baseDamage * data.damages[level];
                    nextCount += data.counts[level];
                    nextSpeed += data.baseSpeed * data.speeds[level];

                    //Weapon 레벨업
                    weapon.LevelUp(nextDamage, nextCount,nextSpeed,0);
                }
                //level 증가
                level++;
                break;
            case ItemData.eItemType.Ice_Bolt:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    this.weapon = newWeapon.AddComponent<Weapon>();
                    //Weapon 초기화
                    this.weapon.Init(data);
                }
                else
                {
                    //...처음 이후의 레벨업은 데미지와 횟수를 계산
                    //새로운 무기이면 damage 올려줘야함
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    //damages가 백분률 값이므로 곱해준 다음 더해줌
                    nextDamage += data.baseDamage * data.damages[level];
                    nextCount += data.counts[level];

                    //Weapon 레벨업
                    weapon.LevelUp(nextDamage, nextCount,0,0);
                }
                //level 증가
                level++;
                break;
            case ItemData.eItemType.Aura:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    this.weapon = newWeapon.AddComponent<Weapon>();
                    //Weapon 초기화
                    this.weapon.Init(data);
                }
                else
                {
                    //...처음 이후의 레벨업은 데미지와 횟수를 계산
                    //새로운 무기이면 damage 올려줘야함
                    float nextDamage = data.baseDamage;
                    float nextSize = data.baseSize;

                    //damages가 백분률 값이므로 곱해준 다음 더해줌
                    nextDamage += data.baseDamage * data.damages[level];
                    nextSize += data.baseSize * data.sizes[level];

                    //Weapon 레벨업
                    weapon.LevelUp(nextDamage, 0, 0, nextSize);
                }
                //level 증가
                level++;
                break;
            case ItemData.eItemType.Shoe:
            case ItemData.eItemType.Shield:
            case ItemData.eItemType.Heal:
                if (level == 0)
                {
                    GameObject newGear = new GameObject();
                    this.gear = newGear.AddComponent<Gear>();
                    //Gear 초기화
                    this.gear.Init(data);
                }
                else
                {
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);
                }
                break;
           
        }

        //...레벨 개수 넘기지 않게 로직 추가
        //Desc의 배열길이:5 를 넘어가면 Button의 interactable 비활성화(투명하게)
        if (level == data.descs.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}