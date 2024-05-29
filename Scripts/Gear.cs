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
        //부모 transform 설정
        this.transform.parent = GameManager.instance.player.transform;
        this.transform.localPosition = Vector3.zero;

        //...Property Set
        type = data.itemType;
        rate = data.damages[0];

        //...장비가 생성되면 Gear의 기능을 적용시킴
        this.ApplyGear();
    }

    //...레벨업
    public void LevelUp(float rate)
    {
        //rate 값 갱신
        this.rate = rate;

        //...장비가 새롭게 추가 되거나 레벨업 할 때 로직적용 함수 호출
        //플레이어의 weapon들에게 다시 적용
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

    //...피해 감쇄
    void RateUp()
    {
        GameManager.instance.damage -= GameManager.instance.damage * this.rate;
    }

    //...player의 이동속도 증가
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
