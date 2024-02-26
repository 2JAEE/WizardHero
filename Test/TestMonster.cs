using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonster : MonoBehaviour
{
    [SerializeField]
    private Player player;

    //...몬스터 정보
    public float health;
    public float maxHealth;
    public float speed;
    public float spawnTime;
    public int spriteType;


    [SerializeField]
    private Bullet bullet;
    private Animator anim;

    void Awake()
    {
        this.anim = this.GetComponent<Animator>();
    }

    //Player를 향해 이동
    void Update()
    {
        //방향 설정
        Vector3 dir = this.player.transform.position - this.transform.position;
        //player 바라보기
        this.transform.LookAt(this.player.transform.position);
        //이동하기
        this.transform.Translate(Vector3.forward * this.speed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Weapon"))
        {
            return;
        }
        Debug.Log("충돌");
        Debug.LogFormat("데미지 :{0}",this.bullet.damage);
        this.health -= this.bullet.damage;
        
        if (health > 0)
        {

        }
        else
        {
            //죽음
            Debug.Log("Destroy");
            this.anim.SetBool("Dead", true);
            //pool에 다시 넣어주기
            TestPoolManager.instance.Release(this.gameObject);
        }
    }

    private void OnEnable()
    {
        this.player = GameManager.instance.player.GetComponent<Player>();
        this.health = maxHealth;
        this.anim.SetBool("Dead", false);
    }

    public void Init(TestSpawnData data)
    {
        this.speed = data.speed;
        this.maxHealth = data.health;
        this.health = data.health;
    }
}
