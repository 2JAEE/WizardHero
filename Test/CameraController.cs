using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //����ٴ� ���ӿ�����Ʈ
    public GameObject playerGo;

    void Update()
    {
        //����ٴ� ���ӿ�����Ʈ ��ġ�� x,y������ ����
        this.transform.position =
            new Vector3(this.playerGo.transform.position.x, this.transform.position.y, this.playerGo.transform.position.z);
    }
}