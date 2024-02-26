using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPoolManager : MonoBehaviour
{
    //�̱���
    public static TestPoolManager instance;

    [Header("#Prefabs")]
    
    public GameObject[] prefabs;
    public TestMonster testMonster;
    //pool ����Ʈ(�� ������ ������ ���� ����)
    private List<GameObject>[] pools;

    public int maxMonsters = 30;

    void Awake()
    {
        instance = this;
        //������ ������ŭ List����
        this.pools = new List<GameObject>[prefabs.Length];

        //for�� ������ �迭 ���� ������ List�� �ʱ�ȭ
        for (int i = 0; i < pools.Length; i++)
        {
            //List �ʱ�ȭ
            pools[i] = new List<GameObject>();
        }
    }

    //monster ��������(pool���� ����)
    public GameObject GetMonster(int i)
    {
        //...���ӿ�����Ʈ �߿� ����
        GameObject select = null;  //�ϴ� �����

        //...������ pool�� ��� �ִ�(��Ȱ��ȭ��) ���ӿ�����Ʈ ����
        foreach (GameObject monster in pools[i])
        {
            //...��Ȱ��ȭ��
            if (!monster.activeSelf)
            {
                //...�߰��ϸ� --> select ������ �Ҵ��� Ȱ��ȭ
                select = monster;
                select.SetActive(true);
                break;
            }
        }
        //...�� ã������(���� Ȱ��ȭ���¸�)
        if (!select)
        {
            select = Instantiate(prefabs[i], transform);
            pools[i].Add(select);
        }
        return select;
    }

    //Ǯ�� ��ȯ�ϱ�
    public void Release(GameObject monsterGo)
    {
        monsterGo.SetActive(false);
        monsterGo.transform.SetParent(this.transform);
    }
}
