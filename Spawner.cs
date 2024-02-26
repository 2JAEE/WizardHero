using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    //Point���� transform
    [Header("#Prefabs")]
    //spawnPoint �迭
    [SerializeField]
    private Transform[] pointTrans;
    //spawnData �迭
    public SpawnData[] spawnData;

    public System.Action<Vector3, float> onCreateDamageText;

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

        //���� : ���ӽð�/10
        //level -> int ����, ���� �������� ������ �� int�� ��ȯ
        this.level = Mathf.FloorToInt(GameManager.instance.gameTime / 12f);

        //spawnData[]���� ������ �ش��ϴ� spawnTime�� �Ǹ� ���� ��ȯ
        if(timer > spawnData[level].spawnTime)
        {
            timer = 0f;
            this.Spawn();
        }
    }

    //���� ���� �� ��ġ �Ҵ� �޼���
    public void Spawn()
    {
        //pool�� �߿��� level�� ���� ���� ȣ��
        GameObject monsterGo = GameManager.instance.pool.Get(level);
        //������ ������ ��ġ�� pointTrans[]���� ���� ��ġ
        monsterGo.transform.position 
            = this.pointTrans[Random.Range(1, this.pointTrans.Length)].transform.position;
        //Debug.LogFormat("<color=yellow>level : {0}</color>", level);
        //���� ȣ�� �� �ʱ�ȭ
        var monster = monsterGo.GetComponent<Monster>();
        monster.onHit = (damage) =>
        {
            
            this.onCreateDamageText(monsterGo.transform.position, damage);
        };
        monsterGo.GetComponent<Monster>().Init(spawnData[level]);
    }
}

[System.Serializable]
//...����ȭ ���ֱ� - ��ü ���� �Ǵ� ���� ����
//�ϳ��� ��ũ��Ʈ ���� �������� Ŭ���� ���� ����
public class SpawnData
{
    public float spawnTime;
    public int Type;
    public int health;
    public float speed;
}