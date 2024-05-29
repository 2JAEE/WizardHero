using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    [SerializeField]
    private Transform playerTrans;
    
    //Trigger 체크(충돌에서 벗어나면 실행)
    private void OnTriggerExit(Collider collision)
    {
        //..tag가 "Area" 아니면 함수 탈출
        if (!collision.CompareTag("Area"))
            return;

        //..player 위치, 현재 위치
        Vector3 playerPos = this.playerTrans.position;
        Vector3 myPos = this.transform.position;

        //..둘 사이의 거리 구하기(x,y)
        float dirX = playerPos.x - myPos.x;
        float dirZ = playerPos.z - myPos.z;

        //..거리값을 절대값으로 변환
        float diffX = Mathf.Abs(dirX);
        float diffZ = Mathf.Abs(dirZ);

        //..거리에 따라 값(방향) 지정하기(오른쪽 : 1, 왼쪽 : -1)
        dirX = dirX > 0 ? 1 : -1;
        dirZ = dirZ > 0 ? 1 : -1;


        //이동시키기
        //..switch ~ case : 변수의 값에 따라 다른 로직 실행
        switch (transform.tag)
        {
            case "Ground":
                //..대각선으로 이동시
                if (Mathf.Abs(diffX - diffZ) <= 0.1f)
                {
                    transform.Translate(Vector3.forward * dirZ * 400);
                    transform.Translate(Vector3.right * dirX * 400);
                    Debug.LogFormat("대각선 : {0}",Vector3.right * dirX * 400);
                }
                //..좌우로 이동시
                else if (diffX > diffZ)
                {
                    transform.Translate(Vector3.right * dirX * 400);
                }
                //..상하로 이동시
                else if (diffX < diffZ)
                {
                    transform.Translate(Vector3.forward * dirZ * 400);
                }
                break;
        }
    }
}
