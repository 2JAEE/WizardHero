using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Gear : MonoBehaviour
{
    public ItemData.eItemType type;
    public float rate;

    public void Init(ItemData data)
    {
        //...Basic Set
        name = "Gear" + data.itemId;
        //�θ� transform ����
        this.transform.parent = GameManager.instance.player.transform;
        this.transform.localPosition = Vector3.zero;

        //...Property Set
        type = data.itemType;
        rate = data.damages[0];

        //...��� �����Ǹ� Gear�� ����� �����Ŵ
        this.ApplyGear();
    }

    //...������
    public void LevelUp(float rate)
    {
        //rate �� ����
        this.rate = rate;

        //...��� ���Ӱ� �߰� �ǰų� ������ �� �� �������� �Լ� ȣ��
        //�÷��̾��� weapon�鿡�� �ٽ� ����
        this.ApplyGear();
    }


    void ApplyGear()
    {
        switch (type)
        {
            case ItemData.eItemType.Shoe:
                this.SpeedUp();
                break;
            case ItemData.eItemType.Shield:
                this.RateUp();
                break;
            case ItemData.eItemType.Heal:
                Heal();
                Debug.LogFormat("hp : {0}", GameManager.instance.player.health);
                break;
        }
    }

    //...���� ����
    void RateUp()
    {
        GameManager.instance.damage -= GameManager.instance.damage * this.rate;
    }

    //...player�� �̵��ӵ� ����
    void SpeedUp()
    {
        float speed = 5f;
        GameManager.instance.player.speed += speed * this.rate;
    }

    void Heal()
    {
        GameManager.instance.player.health += GameManager.instance.player.health * this.rate;
    }
}
