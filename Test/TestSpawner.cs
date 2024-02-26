using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    //Point들의 transform
    [Header("#Prefabs")]
    //spawnPoint 배열
    [SerializeField]
    private Transform[] pointTrans;
    private TestMonster TestMonster;
    //spawnData 배열
    public TestSpawnData[] testSpawnData;

    //..타이머
    private float timer;
    //..레벨
    private int level;

    void Start()
    {
        //현재 transform의 자식으로 붙여주기
        this.pointTrans = this.GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        //타이머 설정
        this.timer += Time.deltaTime;

        //레벨 = 게임시간/10
        //level -> int 형식, 나눈 나머지를 버리고 몫만 int로 변환
        this.level = Mathf.FloorToInt(GameManager.instance.gameTime / 10f);

        //레벨에 해당하는 프리팹이 spawnTime이 되면 몬스터 소환
        if (timer > testSpawnData[level].testSpawnTime)
        {
            timer = 0f;
            this.Spawn();
        }
    }

    //몬스터 생성 후 위치 할당 메서드
    public void Spawn()
    {
        //pool들 중에서 level에 따라 몬스터 호출
        GameObject monster = TestPoolManager.instance.GetMonster(level);
        //가져온 몬스터의 위치를 pointTrans[]에서 랜덤 배치
        monster.transform.position
            = this.pointTrans[Random.Range(1, this.pointTrans.Length)].transform.position;
        Debug.LogFormat("<color=yellow>level : {0}</color>", level);
        //몬스터 호출 및 초기화
        monster.GetComponent<TestMonster>().Init(testSpawnData[level]);
    }
}

[System.Serializable]
//...직렬화 해주기 - 개체 저장 또는 전송 가능
//하나의 스크립트 내에 여러개의 클래스 선언 가능
public class TestSpawnData
{
    public float testSpawnTime;
    public float speed;
    public float health;
}
