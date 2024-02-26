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
        //GetComponentsInchildren���� 2��° ������ ��������(1��° : �ڱ��ڽ�)
        this.bg = this.GetComponentsInChildren<Image>()[1];
        this.icon = this.GetComponentsInChildren<Image>()[2];

        //������ �־��ֱ�
        this.icon.sprite = data.itemIcon;
        this.bg.sprite = data.bgIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        this.txtLevel = texts[0];
        this.txtName = texts[1];
        this.txtDesc = texts[2];

        //�̸� �־��ֱ�
        this.txtName.text = data.itemName;
    }

    //Ȱ��ȭ�� ����
    void OnEnable()
    {
        //���� 1���� ����
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
                //..count �� ������ --> �����
                txtDesc.text = string.Format(data.descs[level], data.damages[level] * 100, data.counts[level]);
                break;
            case ItemData.eItemType.Heal:
            case ItemData.eItemType.Shoe:
            case ItemData.eItemType.Shield:
                //��� --> �����
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
                    //Weapon �ʱ�ȭ
                    this.weapon.Init(data);
                    this.weapon.transform.Rotate(new Vector3(90, 0, 0));
                }
                else
                {
                    //...ó�� ������ �������� �������� Ƚ���� ���
                    //���ο� �����̸� damage �÷������
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;
                    float nextSpeed = data.baseSpeed;

                    //damages�� ��з� ���̹Ƿ� ������ ���� ������
                    nextDamage += data.baseDamage * data.damages[level];
                    nextCount += data.counts[level];
                    nextSpeed += data.baseSpeed * data.speeds[level];

                    //Weapon ������
                    weapon.LevelUp(nextDamage, nextCount,nextSpeed,0);
                }
                //level ����
                level++;
                break;
            case ItemData.eItemType.Ice_Bolt:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    this.weapon = newWeapon.AddComponent<Weapon>();
                    //Weapon �ʱ�ȭ
                    this.weapon.Init(data);
                }
                else
                {
                    //...ó�� ������ �������� �������� Ƚ���� ���
                    //���ο� �����̸� damage �÷������
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    //damages�� ��з� ���̹Ƿ� ������ ���� ������
                    nextDamage += data.baseDamage * data.damages[level];
                    nextCount += data.counts[level];

                    //Weapon ������
                    weapon.LevelUp(nextDamage, nextCount,0,0);
                }
                //level ����
                level++;
                break;
            case ItemData.eItemType.Aura:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    this.weapon = newWeapon.AddComponent<Weapon>();
                    //Weapon �ʱ�ȭ
                    this.weapon.Init(data);
                }
                else
                {
                    //...ó�� ������ �������� �������� Ƚ���� ���
                    //���ο� �����̸� damage �÷������
                    float nextDamage = data.baseDamage;
                    float nextSize = data.baseSize;

                    //damages�� ��з� ���̹Ƿ� ������ ���� ������
                    nextDamage += data.baseDamage * data.damages[level];
                    nextSize += data.baseSize * data.sizes[level];

                    //Weapon ������
                    weapon.LevelUp(nextDamage, 0, 0, nextSize);
                }
                //level ����
                level++;
                break;
            case ItemData.eItemType.Shoe:
            case ItemData.eItemType.Shield:
            case ItemData.eItemType.Heal:
                if (level == 0)
                {
                    GameObject newGear = new GameObject();
                    this.gear = newGear.AddComponent<Gear>();
                    //Gear �ʱ�ȭ
                    this.gear.Init(data);
                }
                else
                {
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);
                }
                break;
           
        }

        //...���� ���� �ѱ��� �ʰ� ���� �߰�
        //Desc�� �迭����:5 �� �Ѿ�� Button�� interactable ��Ȱ��ȭ(�����ϰ�)
        if (level == data.descs.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}