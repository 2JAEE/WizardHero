using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    //싱글톤
    public static Player instance;

    public Scanner scanner;
    private Vector3 dir;
    public float speed;
    private Animator anim;
    public float health;
    public float maxHealth = 100f;
    //private GameObject hpBar;
    public Weapon[] weapons;
    public Transform stickPoint;

    private void Awake()
    {
        this.anim = this.GetComponent<Animator>();
        this.scanner = this.GetComponent<Scanner>();
        //this.hpBar = GameObject.Find("Canvas/Hp");
        this.health = this.maxHealth;
    }

    void Update()
    {
        if(dir != Vector3.zero)
        {
            //이동
            this.transform.Translate(Vector3.forward * this.speed * Time.deltaTime);
            //회전
            this.transform.rotation = Quaternion.LookRotation(dir);
            //이동 애니메이션
            this.anim.SetInteger("State", 1);
        }
        else
        {
            //정지 애니메이션
            this.anim.SetInteger("State", 0);
        }

        //this.hpBar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 0, 0));
    }

    //Input Action에 설정해둔 Action으로 이동하게 함
    void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        //value가 0이 아니면 (input값을 받고 있다면)
        if (value != null) 
        {
            //Vector2 값을 Vector3로 변환
            dir = new Vector3(input.x,0,input.y);
            //Debug.Log(dir);
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (!other.CompareTag("Monster"))
        {
            return;
        }
        //GameManager.instance.health -= Time.deltaTime * GameManager.instance.damage;
        GameManager.instance.health -= GameManager.instance.damage;
        Debug.LogFormat("hp : {0}",GameManager.instance.health);

        //플레이어의 hp가 다 닳으면
        if(GameManager.instance.health < 0)
        {
            //애니메이션 실행
            this.anim.SetBool("IsDie", true);
            //게임오버 실행
            GameManager.instance.GameOver();
        }
    }
}
