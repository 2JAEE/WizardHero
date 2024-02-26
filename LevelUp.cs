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
        //Ȱ��,��Ȱ�� ��� -> (true)
        this.items = this.GetComponentsInChildren<Item>(true);
    }

    //�⺻���� ����
    public void Select(int i)
    {
        this.items[i].OnClick();
    }

    //������ â ���̱�
    public void Show()
    {
        this.Next();
        this.rect.localScale = Vector3.one;
        //�ð� ����
        GameManager.instance.Stop();
    }

    //������ â �����
    public void Hide()
    {
        this.rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
    }

    //������ ���� ����
    void Next()
    {
        //...1. ��� ������ ��Ȱ��ȭ
        foreach (Item item in this.items)
        {
            item.gameObject.SetActive(false);
        }

        //...2. �� �߿��� �������� 3���� ������ Ȱ��ȭ
        //���̰� 3�� �迭 ����
        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);

            //�� �迭�� ��� ���� ��ġ�� �ʰ� ���� �ο�
            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
            {
                break;
            }
        }

        //for�� ���� �ش� ������ Ȱ��ȭ �����ֱ�
        for (int i = 0; i < ran.Length; i++)
        {
            Item ranItem = items[ran[i]];
            //...3. ���� �������� ��� �Һ� ���������� ��ü
            if (ranItem.level == ranItem.data.descs.Length)
            {
                //�Һ������ Ȱ��ȭ
                items[4].gameObject.SetActive(true);
            }
            else
            {
                //���õ� ������ Ȱ��ȭ
                ranItem.gameObject.SetActive(true);
            }
        }
    }
}