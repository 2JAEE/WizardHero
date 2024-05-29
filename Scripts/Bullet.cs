using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Rigidbody rBody;

    //..데미지
    public float damage;
    //..관통력
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

        //..충돌할 때 마다 관통력 감소
        per--;

        if (this.per == -1)
        {
            //..속도 제거
            this.rBody.velocity = Vector3.zero;

            //..오브젝트 비활성화
            this.gameObject.SetActive(false);
        }
    }
}
