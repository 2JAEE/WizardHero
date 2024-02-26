using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    [SerializeField]
    private Transform playerTrans;
    
    //Trigger üũ(�浹���� ����� ����)
    private void OnTriggerExit(Collider collision)
    {
        //..tag�� "Area" �ƴϸ� �Լ� Ż��
        if (!collision.CompareTag("Area"))
            return;

        //..player ��ġ, ���� ��ġ
        Vector3 playerPos = this.playerTrans.position;
        Vector3 myPos = this.transform.position;

        //..�� ������ �Ÿ� ���ϱ�(x,y)
        float dirX = playerPos.x - myPos.x;
        float dirZ = playerPos.z - myPos.z;

        //..�Ÿ����� ���밪���� ��ȯ
        float diffX = Mathf.Abs(dirX);
        float diffZ = Mathf.Abs(dirZ);

        //..�Ÿ��� ���� ��(����) �����ϱ�(������ : 1, ���� : -1)
        dirX = dirX > 0 ? 1 : -1;
        dirZ = dirZ > 0 ? 1 : -1;


        //�̵���Ű��
        //..switch ~ case : ������ ���� ���� �ٸ� ���� ����
        switch (transform.tag)
        {
            case "Ground":
                //..�밢������ �̵���
                if (Mathf.Abs(diffX - diffZ) <= 0.1f)
                {
                    transform.Translate(Vector3.forward * dirZ * 400);
                    transform.Translate(Vector3.right * dirX * 400);
                    Debug.LogFormat("�밢�� : {0}",Vector3.right * dirX * 400);
                }
                //..�¿�� �̵���
                else if (diffX > diffZ)
                {
                    transform.Translate(Vector3.right * dirX * 400);
                }
                //..���Ϸ� �̵���
                else if (diffX < diffZ)
                {
                    transform.Translate(Vector3.forward * dirZ * 400);
                }
                break;
        }
    }
}