using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.XR;

public class Weapon : MonoBehaviour
{
    //무기id, 프리팹id, damage, 갯수, 속도
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float size;
    public float speed;

    private RectTransform rect;
    private PlayerPos playerPos;
    private Player player;
    private float timer;

    private void Awake()
    {
        //부모 오브젝트에 있는 컴포넌트 가져오기
        this.player = GameManager.instance.player;
        this.playerPos = GameManager.instance.playerPos;
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        //id 별로 관리
        switch (id)
        {
            case 0:
                //bullet(얼음 오브) 회전시키기
                this.transform.Rotate(Vector3.back * this.speed * Time.deltaTime);
                break;
            case 1:
                this.Create();
                break;
            case 2:
                this.timer += Time.deltaTime;

                if (this.timer > this.speed)
                {
                    //timer 초기화
                    this.timer = 0f;
                    //총알 발사   
                    this.Fire();
                }
                break;
        }
    }

    //id별로 초기화
    public void Init(ItemData data)
    {
        //...Basic Set
        this.name = "Weapon" + data.itemId;
        //부모 transform 설정
        this.transform.parent = this.playerPos.transform;
        this.transform.localPosition = Vector3.zero;

        //..Property Set
        this.id = data.itemId;
        this.damage = data.baseDamage;
        this.count = data.baseCount;
        this.size = data.baseSize;

        for (int i = 0; i < GameManager.instance.pool.prefabs.Length; i++)
        {
            //무기 프리팹과 오브젝트 풀의 프리팹 연동
            if (data.projectile == GameManager.instance.pool.prefabs[i])
            {
                this.prefabId = i;
                Debug.LogFormat("<color=red>prefabId : {0}</color>", this.prefabId);
                break;
            }
        }

        switch (id)
        {
            case 0:
                this.speed = 180f;
                this.Batch();
                break;
            case 1:
                break;
            case 2:
                //총알 발사 속도
                this.speed = 0.4f;
                break;
        }


        //player가 가지고 있는 모든 장비에 ApplyGear 적용
        //...특정 함수 호출을 모든 자식에게 방송하는 함수
        //2번째 인자값으로 'DontRequireReceiver' 추가 (꼭 receiver가 필요하지 않다)
        //this.player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    //...레벨업 메서드
    //레벨업 시 damage와 count 조절하기
    public void LevelUp(float damage, int count, float speed, float size)
    {
        this.damage = damage;
        this.count += count;
        this.speed = speed;
        this.size = size;

        //레벨 = 0 , 근접무기일 경우
        if (id == 0)
        {
            //배치해아함
            this.Batch();
        }
        else if(id == 1)
        {
            //아우라 사이즈 업
            this.RateUp(this.size);
        }
        //...BroadcastMessage 초기화
        //this.player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }


    //...근거리 bullet(삽) 배치하기
    void Batch()
    {
        for (int i = 0; i < this.count; i++)
        {
            //...bullet 게임오브젝트 생성 후
            //Weapon의 자식으로 넣기 위해 transform가져와 지역변수에 넣기
            Transform bullet;

            //...i가 childCount 범위 내라면
            if (i < this.transform.childCount)
            {
                //현재 child를 사용
                bullet = this.transform.GetChild(i);
            }
            else
            {
                //...i가 범위를 넘어서면
                //'모자란 수 만큼' 풀링으로 가져옴
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                //현재 transform을 부모로 설정
                bullet.parent = this.transform;
            }

            //...bullet위치 player의 위치로 초기화
            //levelup 했을때 위치 오류 방지
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            //...회전 각도(z축) 설정
            //count(갯수)만큼 회전각 나누어서 배치
            Vector3 rot = Vector3.forward * 360 * i / count;
            bullet.Rotate(rot);
            //Player로 부터 2f 거리만큼 떨어져서 위치
            bullet.Translate(bullet.up * 2f, Space.World);

            //bullet 초기화
            bullet.GetComponent<Bullet>().Init(this.damage, -1, Vector3.zero);  // -1 == 무한, 무한 관통
        }
    }

    //...원거리 Bullet(총알) 발사
    void Fire()
    {
        if (!this.player.scanner.nearestTarget)
        {
            return;
        }

        //가장 가까운 몬스터 위치
        Vector3 targetPos = this.player.scanner.nearestTarget.position;
        //...총알 방향 설정
        Vector3 dir = targetPos - transform.position;
        //현재 Vector의 방향은 유지, 크기는 1로 변환
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        //현재 transform을 부모로 설정
        bullet.parent = this.transform;
        bullet.position = this.transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        //bullet 초기화
        bullet.GetComponent<Bullet>().Init(this.damage, 0, dir);  // -1 == 무한, 무한 관통


        //for (int i = 0; i < this.count; i++)
        //{
        //    //...bullet 게임오브젝트 생성 후
        //    //Weapon의 자식으로 넣기 위해 transform가져와 지역변수에 넣기
        //    Transform bullet;

        //    //...i가 childCount 범위 내라면
        //    if (i < this.transform.childCount)
        //    {
        //        //현재 child를 사용
        //        bullet = this.transform.GetChild(i);
        //    }
        //    else
        //    {
        //        //...i가 범위를 넘어서면
        //        //'모자란 수 만큼' 풀링으로 가져옴
        //        bullet = GameManager.instance.pool.Get(prefabId).transform;
        //        //현재 transform을 부모로 설정
        //        bullet.parent = this.transform;

        //        //가장 가까운 몬스터 위치
        //        Vector3 targetPos = this.player.scanner.nearestTarget.position;
        //        //...총알 방향 설정
        //        Vector3 dir = targetPos - transform.position;
        //        //현재 Vector의 방향은 유지, 크기는 1로 변환
        //        dir = dir.normalized;

        //        bullet.position = this.transform.position;
        //        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        //        //bullet 초기화
        //        bullet.GetComponent<Bullet>().Init(this.damage, 0, dir);  // -1 == 무한, 무한 관통
        //    }
        //}
    }

    void RateUp(float i)
    {
        Transform bullet = GetComponent<Transform>();
        switch (id)
        {
            case 1:
                bullet.localScale = new Vector3(i,i,i);
                break;
        }    
    }

    void Create()
    {
        for (int i = 0; i < this.count; i++)
        {
            //...bullet 게임오브젝트 생성 후
            //Weapon의 자식으로 넣기 위해 transform가져와 지역변수에 넣기
            Transform bullet;

            //...i가 childCount 범위 내라면
            if (i < this.transform.childCount)
            {
                //현재 child를 사용
                bullet = this.transform.GetChild(i);
            }
            else
            {
                //...i가 범위를 넘어서면
                //'모자란 수 만큼' 풀링으로 가져옴
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                //현재 transform을 부모로 설정
                bullet.parent = this.transform;
                //bullet.transform.position = this.transform.position;
            }

            //Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
            //bullet.position = this.transform.position;
            //bullet 초기화
            bullet.GetComponent<Bullet>().Init(this.damage, -1, Vector3.zero);  // -1 == 무한, 무한 관통
        }
    }
}
