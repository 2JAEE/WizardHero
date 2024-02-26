using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Rigidbody rBody;

    //..������
    public float damage;
    //..�����
    public int per;

    void Awake()
    {
        this.rBody = this.GetComponent<Rigidbody>();
    }

    public void Init(float damage,int per, Vector3 dir)
    {
        this.damage = damage;
        //Debug.LogFormat("Bullet :{0}", this.damage);
        this.per = per;

        if(per > -1)
        {
            this.rBody.velocity = dir * 15f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(this.per == -1 || !other.CompareTag("Monster"))
        {
            return;
        }

        //..�浹�� �� ���� ����� ����
        per--;

        if (this.per == -1)
        {
            //..�ӵ� ����
            this.rBody.velocity = Vector3.zero;

            //..������Ʈ ��Ȱ��ȭ
            this.gameObject.SetActive(false);
        }
    }
}