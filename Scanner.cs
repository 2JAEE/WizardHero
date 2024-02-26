using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    private Collider[] targets;
    public Transform nearestTarget;

    private void FixedUpdate()
    {
        //scanRange ���� �ش��ϴ� targetLayer�� ��ġ�� �迭�� ����
        targets = Physics.OverlapSphere(transform.position, scanRange,targetLayer);
        nearestTarget = GetNearest();
    }

    //���� ����� target ��ġ ��ȯ �޼���
    Transform GetNearest()
    {
        Transform result = null;
        float dis = 100;
        foreach (Collider target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDis = Vector3.Distance(myPos, targetPos);

            //�� ����� ��ġ�� ����
            if (curDis < dis)
            {
                dis = curDis;
                result = target.transform;
            }
        }

        //���� ����� target��ġ ��ȯ
        return result;
        
    }
}