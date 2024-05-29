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
        //scanRange 내에 해당하는 targetLayer의 위치를 배열에 저장
        targets = Physics.OverlapSphere(transform.position, scanRange,targetLayer);
        nearestTarget = GetNearest();
    }

    //가장 가까운 target 위치 반환 메서드
    Transform GetNearest()
    {
        Transform result = null;
        float dis = 100;
        foreach (Collider target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDis = Vector3.Distance(myPos, targetPos);

            //더 가까운 위치를 갱신
            if (curDis < dis)
            {
                dis = curDis;
                result = target.transform;
            }
        }

        //가장 가까운 target위치 반환
        return result;
        
    }
}
