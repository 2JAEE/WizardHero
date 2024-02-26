using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    //�̱���
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
            //�̵�
            this.transform.Translate(Vector3.forward * this.speed * Time.deltaTime);
            //ȸ��
            this.transform.rotation = Quaternion.LookRotation(dir);
            //�̵� �ִϸ��̼�
            this.anim.SetInteger("State", 1);
        }
        else
        {
            //���� �ִϸ��̼�
            this.anim.SetInteger("State", 0);
        }

        //this.hpBar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 0, 0));
    }

    //Input Action�� �����ص� Action���� �̵��ϰ� ��
    void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        //value�� 0�� �ƴϸ� (input���� �ް� �ִٸ�)
        if (value != null) 
        {
            //Vector2 ���� Vector3�� ��ȯ
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

        //�÷��̾��� hp�� �� ������
        if(GameManager.instance.health < 0)
        {
            //�ִϸ��̼� ����
            this.anim.SetBool("IsDie", true);
            //���ӿ��� ����
            GameManager.instance.GameOver();
        }
    }
}