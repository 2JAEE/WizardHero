using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed = 1f;
    [SerializeField]
    private Player player;

    //...���� ����
    private GameObject prefab;
    private float health;
    private float maxHealth;
    private bool isLive;
    private Collider col;
    private Bullet bullet;
    private Animator anim;
    private Rigidbody rBody;
    private float force = 2f;
    private WaitForFixedUpdate wait;
    private GameObject txtDamge;
    public System.Action<float> onHit;
    public Transform damagePoint;

    void Awake()
    {
        this.anim = this.GetComponent<Animator>();
        this.rBody = this.GetComponent<Rigidbody>();
        this.col = this.GetComponent<Collider>();
        this.wait = new WaitForFixedUpdate();
        //this.txtDamge = GameObject.Find("Canvas/txtDamage");
    }

    //Player�� ���� �̵�
    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        //���� ����
        Vector3 dir = this.player.transform.position - this.transform.position;
        //player �ٶ󺸱�
        this.transform.LookAt(this.player.transform.position);
        //�̵��ϱ�
        this.transform.Translate(Vector3.forward * this.speed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        this.bullet = other.GetComponent<Bullet>();

        if (!other.CompareTag("Bullet")||!isLive)
        {
            return;
        }

        

        this.health -= this.bullet.damage;
        //Debug.LogFormat("<color=yellow>���� : {0}</color>",this.bullet.damage);
        //���ݹ���
        
        this.Hit(this.bullet.damage);

        this.StartCoroutine(HitBack());


        //this.txtDamge.transform.position = Camera.main.WorldToScreenPoint(this.transform.position);
        //this.txtDamge.SetActive(true);

        if (health > 0)
        {

        }
        else
        {
            this.isLive = false;
            this.col.enabled = false;
            this.anim.SetBool("Dead", true);
            //�ڷ�ƾ ����
            this.StartCoroutine(CoDie());
            //����ġ ����
            GameManager.instance.GetExp();
            //ų �� ����
            GameManager.instance.kill++;
            //Debug.LogFormat("<color=green>Kill:{0}</color>",GameManager.instance.kill);
        }
    }

    //���ݹ���
    public void Hit(float damage)
    {
        this.onHit(damage);
    }

    private void OnEnable()
    {
        this.isLive = true;
        this.player = GameManager.instance.player.GetComponent<Player>();
        this.health = maxHealth;
        this.anim.SetBool("Dead", false);
    }

    //�ʱ�ȭ
    public void Init(SpawnData data)
    {
        this.speed = data.speed;
        this.maxHealth = data.health;
        this.health = data.health;
    }

    //���� �ڷ�ƾ
    IEnumerator CoDie()
    {
        this.anim.SetBool("Dead", true);

        yield return new WaitForSecondsRealtime(0.3f);

        //pool�� �ٽ� �־��ֱ�
        GameManager.instance.pool.Release(this.gameObject);
    }

    //�ǰ� �ڷ�ƾ
    IEnumerator HitBack()
    {
        yield return this.wait;
        Vector3 dir = this.transform.position - this.player.transform.position;
        this.rBody.AddForce(dir.normalized * this.force, ForceMode.Impulse);
    }
}