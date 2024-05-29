using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed = 1f;
    [SerializeField]
    private Player player;

    //...몬스터 정보
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

    //Player를 향해 이동
    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        //방향 설정
        Vector3 dir = this.player.transform.position - this.transform.position;
        //player 바라보기
        this.transform.LookAt(this.player.transform.position);
        //이동하기
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
        //Debug.LogFormat("<color=yellow>몬스터 : {0}</color>",this.bullet.damage);
        //공격받음
        
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
            //코루틴 실행
            this.StartCoroutine(CoDie());
            //경험치 증가
            GameManager.instance.GetExp();
            //킬 수 증가
            GameManager.instance.kill++;
            //Debug.LogFormat("<color=green>Kill:{0}</color>",GameManager.instance.kill);
        }
    }

    //공격받음
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

    //초기화
    public void Init(SpawnData data)
    {
        this.speed = data.speed;
        this.maxHealth = data.health;
        this.health = data.health;
    }

    //죽음 코루틴
    IEnumerator CoDie()
    {
        this.anim.SetBool("Dead", true);

        yield return new WaitForSecondsRealtime(0.3f);

        //pool에 다시 넣어주기
        GameManager.instance.pool.Release(this.gameObject);
    }

    //피격 코루틴
    IEnumerator HitBack()
    {
        yield return this.wait;
        Vector3 dir = this.transform.position - this.player.transform.position;
        this.rBody.AddForce(dir.normalized * this.force, ForceMode.Impulse);
    }
}
