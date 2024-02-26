using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    //Point���� transform
    [Header("#Prefabs")]
    //spawnPoint �迭
    [SerializeField]
    private Transform[] pointTrans;
    private TestMonster TestMonster;
    //spawnData �迭
    public TestSpawnData[] testSpawnData;

    //..Ÿ�̸�
    private float timer;
    //..����
    private int level;

    void Start()
    {
        //���� transform�� �ڽ����� �ٿ��ֱ�
        this.pointTrans = this.GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        //Ÿ�̸� ����
        this.timer += Time.deltaTime;

        //���� = ���ӽð�/10
        //level -> int ����, ���� �������� ������ �� int�� ��ȯ
        this.level = Mathf.FloorToInt(GameManager.instance.gameTime / 10f);

        //������ �ش��ϴ� �������� spawnTime�� �Ǹ� ���� ��ȯ
        if (timer > testSpawnData[level].testSpawnTime)
        {
            timer = 0f;
            this.Spawn();
        }
    }

    //���� ���� �� ��ġ �Ҵ� �޼���
    public void Spawn()
    {
        //pool�� �߿��� level�� ���� ���� ȣ��
        GameObject monster = TestPoolManager.instance.GetMonster(level);
        //������ ������ ��ġ�� pointTrans[]���� ���� ��ġ
        monster.transform.position
            = this.pointTrans[Random.Range(1, this.pointTrans.Length)].transform.position;
        Debug.LogFormat("<color=yellow>level : {0}</color>", level);
        //���� ȣ�� �� �ʱ�ȭ
        monster.GetComponent<TestMonster>().Init(testSpawnData[level]);
    }
}

[System.Serializable]
//...����ȭ ���ֱ� - ��ü ���� �Ǵ� ���� ����
//�ϳ��� ��ũ��Ʈ ���� �������� Ŭ���� ���� ����
public class TestSpawnData
{
    public float testSpawnTime;
    public float speed;
    public float health;
}
