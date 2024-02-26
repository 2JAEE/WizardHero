using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //�̱���
    public static GameManager instance;

    //...GameObject...
    public PoolManager pool;
    public Player player;
    public LevelUp levelUp;
    public Transform uiJoy;
    public PlayerPos playerPos;
    public UiResult uiResult;
    public int kill;
    public Bullet bullet;
    public Weapon weapon;
    //public GameObject txtDamage;

    //...GameControl...
    public float gameTime;
    public float maxGameTime;
    //�ð� ���� ���� ����
    public bool isLive;

    //...PlayerInfo...
    public float health;
    public float maxHealth =100;
    public float damage = 10f;
    public int level;
    public float exp;
    public int[] nextExp = { 5, 15, 30, 40, 50, 60, 70, 80, 100 };

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (!isLive)
        {
            return;
        }
        //..����(�÷���)�ð� = �ΰ��� �ð�
        this.gameTime += Time.deltaTime;

        //..���� �ð��� �ִ� ���ӽð��� �Ѿ��
        if(this.gameTime > this.maxGameTime)
        {
            //���� ���� �����(�ִ���� �ð��� �Ѿ�� �ʵ���)
            this.gameTime = this.maxGameTime;
            //���� �¸�
            this.GameVictory();
        }
    }

    public void StartGame()
    {
        //ü�� �ʱ�ȭ
        this.health = this.maxHealth;
        //�⺻ ���� ����
        this.levelUp.Select(1);
        //���ӽð� �ʱ�ȭ
        this.Resume();
    }

    public void GameOver()
    {
        this.StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        this.isLive = false;

        yield return new WaitForSeconds(0.5f);

        this.uiResult.gameObject.SetActive(true);
        this.uiResult.Lose();
        //�ð� ����
        Stop();
    }

    public void GameVictory()
    {
        this.StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        this.isLive = false;
        //this.monsterCleaner.SetActive(true);

        yield return new WaitForSeconds(1f);

        this.uiResult.gameObject.SetActive(true);
        this.uiResult.Win();
        //�ð� ����
        Stop();

    }

    //����ġ 
    public void GetExp()
    {
        //����ġ ����
        this.exp++;

        //���� ����ġ�� ��ǥ����ġ(��������ġ)�� ������ ���
        if(this.exp == this.nextExp[Mathf.Min(this.level,nextExp.Length - 1)])
        {
            //���� ����
            this.level++;
            //����ġ �ʱ�ȭ
            this.exp = 0;
            //������ â ���̱�
            this.levelUp.Show();
        }
    }

    //�ð� ����
    public void Stop()
    {
        this.isLive = false;
        //...TimeScale = ����Ƽ �ð� �ӵ�(����)
        Time.timeScale = 0;

        //���̽�ƽ �����
        this.uiJoy.localScale = Vector3.zero;
    }

    //�ð� �簳
    public void Resume()
    {
        this.isLive = true;
        Time.timeScale = 1;

        //���̽�ƽ ���̱�
        this.uiJoy.localScale = Vector3.one;
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
}