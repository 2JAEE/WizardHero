using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiResult : MonoBehaviour
{ 

    public GameObject[] titles;

    public Text txtKill;
    public Text txtTime;

    public Text txtKill1;
    public Text txtTime1;

    void Aawke()
    {
        this.titles = this.GetComponent<GameObject[]>();
        this.txtKill = this.GetComponent<Text>();
        this.txtTime = this.GetComponent<Text>();

        this.txtKill1 = this.GetComponent<Text>();
        this.txtTime1 = this.GetComponent<Text>();

    }

    void Start()
    {
        this.txtKill.text = string.Format("Ã³Ä¡ÇÑ Àû : {0}",GameManager.instance.kill.ToString());
        this.txtKill1.text = string.Format("Ã³Ä¡ÇÑ Àû : {0}", GameManager.instance.kill.ToString());
        float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
        //ºÐ
        int min = Mathf.FloorToInt(remainTime / 60);
        int sec = Mathf.FloorToInt(GameManager.instance.gameTime);
        this.txtTime.text = string.Format("{0:D2}:{1:D2}",min,GameManager.instance.gameTime.ToString());
        this.txtTime1.text = string.Format("{0:D2}:{1:D2}", min,sec);
    }

    public void Win()
    {
        this.titles[1].SetActive(true);
    }

    public void Lose()
    {
        this.titles[0].SetActive(true);
    }
}
