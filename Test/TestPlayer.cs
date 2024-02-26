using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayer : MonoBehaviour
{
    //�̱���
    public static Player instance;

    private Vector3 dir;
    public float speed;
    private Animator anim;
    public float health;
    public float maxHealth = 100f;

    private void Awake()
    {
        this.anim = this.GetComponent<Animator>();
        this.health = this.maxHealth;
    }

    void Update()
    {
        if (dir != Vector3.zero)
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
    }

    //Input Action�� �����ص� Action���� �̵��ϰ� ��
    void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        //value�� 0�� �ƴϸ� (input���� �ް� �ִٸ�)
        if (value != null)
        {
            //Vector2 ���� Vector3�� ��ȯ
            dir = new Vector3(input.x, 0, input.y);
            //Debug.Log(dir);
        }
    }
}
