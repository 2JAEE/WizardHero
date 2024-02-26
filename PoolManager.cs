using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

    [Header("#Prefabs")]
    //������ ������ ����
    public GameObject[] prefabs;
    //pool ����Ʈ(�� ������ ������ ���� ����)
    private List<GameObject>[] pools;

    void Awake()
    {
        //������ ������ŭ List����
        this.pools = new List<GameObject>[prefabs.Length];
        
        //for�� ������ �迭 ���� ������ List�� �ʱ�ȭ
        for(int i = 0; i < pools.Length; i++)
        {
            //List �ʱ�ȭ
            pools[i] = new List<GameObject>();
        }

        Debug.LogFormat("Ǯ�� �Ϸ� : {0}", this.pools.Length);
    }

    //item ��������(pool���� ����)
    public GameObject Get(int i)
    {
        //...���ӿ�����Ʈ �߿� ����
        GameObject select = null;  //�ϴ� �����

        //...������ pool�� ��� �ִ�(��Ȱ��ȭ��) ���ӿ�����Ʈ ����
        foreach(GameObject item in pools[i])
        {
            //...��Ȱ��ȭ��
            if (!item.activeSelf)
            {
                //...�߰��ϸ� --> select ������ �Ҵ��� Ȱ��ȭ
                select = item;
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
