using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonster : MonoBehaviour
{
    [SerializeField]
    private Player player;

    //...���� ����
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

    //Player�� ���� �̵�
    void Update()
    {
        //���� ����
        Vector3 dir = this.player.transform.position - this.transform.position;
        //player �ٶ󺸱�
        this.transform.LookAt(this.player.transform.position);
        //�̵��ϱ�
        this.transform.Translate(Vector3.forward * this.speed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Weapon"))
        {
            return;
        }
        Debug.Log("�浹");
        Debug.LogFormat("������ :{0}",this.bullet.damage);
        this.health -= this.bullet.damage;
        
        if (health > 0)
        {

        }
        else
        {
            //����
            Debug.Log("Destroy");
            this.anim.SetBool("Dead", true);
            //pool�� �ٽ� �־��ֱ�
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
