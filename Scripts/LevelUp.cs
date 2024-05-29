using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    private RectTransform rect;
    private Item[] items;

    void Awake()
    {
        this.rect = this.GetComponent<RectTransform>();
        //활성,비활성 모두 -> (true)
        this.items = this.GetComponentsInChildren<Item>(true);
    }

    //기본무기 지급
    public void Select(int i)
    {
        this.items[i].OnClick();
    }

    //레벨업 창 보이기
    public void Show()
    {
        this.Next();
        this.rect.localScale = Vector3.one;
        //시간 정지
        GameManager.instance.Stop();
    }

    //레벨업 창 숨기기
    public void Hide()
    {
        this.rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
    }

    //아이템 랜덤 생성
    void Next()
    {
        //...1. 모든 아이템 비활성화
        foreach (Item item in this.items)
        {
            item.gameObject.SetActive(false);
        }

        //...2. 그 중에서 랜덤으로 3개의 아이템 활성화
        //길이가 3인 배열 선언
        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);

            //각 배열의 요소 값이 겹치지 않게 조건 부여
            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
            {
                break;
            }
        }

        //for문 돌며 해당 아이템 활성화 시켜주기
        for (int i = 0; i < ran.Length; i++)
        {
            Item ranItem = items[ran[i]];
            //...3. 만렙 아이템의 경우 소비 아이템으로 대체
            if (ranItem.level == ranItem.data.descs.Length)
            {
                //소비아이템 활성화
                items[4].gameObject.SetActive(true);
            }
            else
            {
                //선택된 아이템 활성화
                ranItem.gameObject.SetActive(true);
            }
        }
    }
}
