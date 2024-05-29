using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //싱글톤
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
    //시간 정지 여부 변수
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
        //..게임(플레이)시간 = 인게임 시간
        this.gameTime += Time.deltaTime;

        //..게임 시간이 최대 게임시간을 넘어가면
        if(this.gameTime > this.maxGameTime)
        {
            //둘을 같게 만들기(최대게임 시간을 넘어가지 않도록)
            this.gameTime = this.maxGameTime;
            //게임 승리
            this.GameVictory();
        }
    }

    public void StartGame()
    {
        //체력 초기화
        this.health = this.maxHealth;
        //기본 무기 지급
        this.levelUp.Select(1);
        //게임시간 초기화
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
        //시간 멈춤
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
        //시간 멈춤
        Stop();

    }

    //경험치 
    public void GetExp()
    {
        //경험치 증가
        this.exp++;

        //쌓인 경험치가 목표경험치(다음경험치)와 같아질 경우
        if(this.exp == this.nextExp[Mathf.Min(this.level,nextExp.Length - 1)])
        {
            //레벨 증가
            this.level++;
            //경험치 초기화
            this.exp = 0;
            //레벨업 창 보이기
            this.levelUp.Show();
        }
    }

    //시간 정지
    public void Stop()
    {
        this.isLive = false;
        //...TimeScale = 유니티 시간 속도(배율)
        Time.timeScale = 0;

        //조이스틱 숨기기
        this.uiJoy.localScale = Vector3.zero;
    }

    //시간 재개
    public void Resume()
    {
        this.isLive = true;
        Time.timeScale = 1;

        //조이스틱 보이기
        this.uiJoy.localScale = Vector3.one;
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
}
