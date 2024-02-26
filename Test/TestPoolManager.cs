using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPoolManager : MonoBehaviour
{
    //싱글톤
    public static TestPoolManager instance;

    [Header("#Prefabs")]
    
    public GameObject[] prefabs;
    public TestMonster testMonster;
    //pool 리스트(각 프리팹 종류에 따라 관리)
    private List<GameObject>[] pools;

    public int maxMonsters = 30;

    void Awake()
    {
        instance = this;
        //프리팹 종류만큼 List생성
        this.pools = new List<GameObject>[prefabs.Length];

        //for문 돌려서 배열 안의 각각의 List들 초기화
        for (int i = 0; i < pools.Length; i++)
        {
            //List 초기화
            pools[i] = new List<GameObject>();
        }
    }

    //monster 가져오기(pool에서 빼기)
    public GameObject GetMonster(int i)
    {
        //...게임오브젝트 중에 선택
        GameObject select = null;  //일단 비워둠

        //...선택한 pool의 놀고 있는(비활성화된) 게임오브젝트 접근
        foreach (GameObject monster in pools[i])
        {
            //...비활성화면
            if (!monster.activeSelf)
            {
                //...발견하면 --> select 변수에 할당후 활성화
                select = monster;
                select.SetActive(true);
                break;
            }
        }
        //...못 찾았으면(전부 활성화상태면)
        if (!select)
        {
            select = Instantiate(prefabs[i], transform);
            pools[i].Add(select);
        }
        return select;
    }

    //풀에 반환하기
    public void Release(GameObject monsterGo)
    {
        monsterGo.SetActive(false);
        monsterGo.transform.SetParent(this.transform);
    }
}
