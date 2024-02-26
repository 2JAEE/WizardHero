using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //따라다닐 게임오브젝트
    public GameObject playerGo;

    void Update()
    {
        //따라다닐 게임오브젝트 위치를 x,y값으로 설정
        this.transform.position =
            new Vector3(this.playerGo.transform.position.x, this.transform.position.y, this.playerGo.transform.position.z);
    }
}