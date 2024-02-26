using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum eInfoType 
    { 
        Exp, Level, Health, Damage, Time, LevelTxt
    }

    public eInfoType type;

    //..UI Ÿ��
    private Slider slider;
    private Text text;

    void Awake()
    {
        this.slider = this.GetComponent<Slider>();
        this.text = this.GetComponent<Text>();
    }


    void Update()
    {
        switch (type) 
        {
            case eInfoType.Exp:
                float curExp = GameManager.instance.exp;
                float nextExp 
                    = GameManager.instance.nextExp[Mathf.Min(GameManager.instance.level,GameManager.instance.nextExp.Length-1)];
                this.slider.value = curExp / nextExp;
                break;
            case eInfoType.Level:
                this.text.text = string.Format("���� {0}", GameManager.instance.level.ToString());
                break;
            case eInfoType.LevelTxt:
                this.text.text = string.Format("{0} ���� ����", GameManager.instance.level.ToString());
                break;
            case eInfoType.Health:
                float curHealth = GameManager.instance.health;
                float maxHealth = GameManager.instance.maxHealth;
                //�����̴� ��
                this.slider.value = curHealth/maxHealth;
                break;
            case eInfoType.Time:
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                //��
                int min = Mathf.FloorToInt(remainTime / 60);
                //�� (% : ������, D: ǥ���� �ڸ���)
                int sec = Mathf.FloorToInt(remainTime % 60);
                text.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;
            //case eInfoType.Damage:
            //    this.text.text = string.Format("{0}", GameManager.instance.bullet.damage.ToString());
            //    break;
        }
    }
}