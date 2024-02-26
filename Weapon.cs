using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.XR;

public class Weapon : MonoBehaviour
{
    //����id, ������id, damage, ����, �ӵ�
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
        //�θ� ������Ʈ�� �ִ� ������Ʈ ��������
        this.player = GameManager.instance.player;
        this.playerPos = GameManager.instance.playerPos;
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        //id ���� ����
        switch (id)
        {
            case 0:
                //bullet(���� ����) ȸ����Ű��
                this.transform.Rotate(Vector3.back * this.speed * Time.deltaTime);
                break;
            case 1:
                this.Create();
                break;
            case 2:
                this.timer += Time.deltaTime;

                if (this.timer > this.speed)
                {
                    //timer �ʱ�ȭ
                    this.timer = 0f;
                    //�Ѿ� �߻�   
                    this.Fire();
                }
                break;
        }
    }

    //id���� �ʱ�ȭ
    public void Init(ItemData data)
    {
        //...Basic Set
        this.name = "Weapon" + data.itemId;
        //�θ� transform ����
        this.transform.parent = this.playerPos.transform;
        this.transform.localPosition = Vector3.zero;

        //..Property Set
        this.id = data.itemId;
        this.damage = data.baseDamage;
        this.count = data.baseCount;
        this.size = data.baseSize;

        for (int i = 0; i < GameManager.instance.pool.prefabs.Length; i++)
        {
            //���� �����հ� ������Ʈ Ǯ�� ������ ����
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
                //�Ѿ� �߻� �ӵ�
                this.speed = 0.4f;
                break;
        }


        //player�� ������ �ִ� ��� ��� ApplyGear ����
        //...Ư�� �Լ� ȣ���� ��� �ڽĿ��� ����ϴ� �Լ�
        //2��° ���ڰ����� 'DontRequireReceiver' �߰� (�� receiver�� �ʿ����� �ʴ�)
        //this.player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    //...������ �޼���
    //������ �� damage�� count �����ϱ�
    public void LevelUp(float damage, int count, float speed, float size)
    {
        this.damage = damage;
        this.count += count;
        this.speed = speed;
        this.size = size;

        //���� = 0 , ���������� ���
        if (id == 0)
        {
            //��ġ�ؾ���
            this.Batch();
        }
        else if(id == 1)
        {
            //�ƿ�� ������ ��
            this.RateUp(this.size);
        }
        //...BroadcastMessage �ʱ�ȭ
        //this.player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }


    //...�ٰŸ� bullet(��) ��ġ�ϱ�
    void Batch()
    {
        for (int i = 0; i < this.count; i++)
        {
            //...bullet ���ӿ�����Ʈ ���� ��
            //Weapon�� �ڽ����� �ֱ� ���� transform������ ���������� �ֱ�
            Transform bullet;

            //...i�� childCount ���� �����
            if (i < this.transform.childCount)
            {
                //���� child�� ���
                bullet = this.transform.GetChild(i);
            }
            else
            {
                //...i�� ������ �Ѿ��
                //'���ڶ� �� ��ŭ' Ǯ������ ������
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                //���� transform�� �θ�� ����
                bullet.parent = this.transform;
            }

            //...bullet��ġ player�� ��ġ�� �ʱ�ȭ
            //levelup ������ ��ġ ���� ����
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            //...ȸ�� ����(z��) ����
            //count(����)��ŭ ȸ���� ����� ��ġ
            Vector3 rot = Vector3.forward * 360 * i / count;
            bullet.Rotate(rot);
            //Player�� ���� 2f �Ÿ���ŭ �������� ��ġ
            bullet.Translate(bullet.up * 2f, Space.World);

            //bullet �ʱ�ȭ
            bullet.GetComponent<Bullet>().Init(this.damage, -1, Vector3.zero);  // -1 == ����, ���� ����
        }
    }

    //...���Ÿ� Bullet(�Ѿ�) �߻�
    void Fire()
    {
        if (!this.player.scanner.nearestTarget)
        {
            return;
        }

        //���� ����� ���� ��ġ
        Vector3 targetPos = this.player.scanner.nearestTarget.position;
        //...�Ѿ� ���� ����
        Vector3 dir = targetPos - transform.position;
        //���� Vector�� ������ ����, ũ��� 1�� ��ȯ
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        //���� transform�� �θ�� ����
        bullet.parent = this.transform;
        bullet.position = this.transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        //bullet �ʱ�ȭ
        bullet.GetComponent<Bullet>().Init(this.damage, 0, dir);  // -1 == ����, ���� ����


        //for (int i = 0; i < this.count; i++)
        //{
        //    //...bullet ���ӿ�����Ʈ ���� ��
        //    //Weapon�� �ڽ����� �ֱ� ���� transform������ ���������� �ֱ�
        //    Transform bullet;

        //    //...i�� childCount ���� �����
        //    if (i < this.transform.childCount)
        //    {
        //        //���� child�� ���
        //        bullet = this.transform.GetChild(i);
        //    }
        //    else
        //    {
        //        //...i�� ������ �Ѿ��
        //        //'���ڶ� �� ��ŭ' Ǯ������ ������
        //        bullet = GameManager.instance.pool.Get(prefabId).transform;
        //        //���� transform�� �θ�� ����
        //        bullet.parent = this.transform;

        //        //���� ����� ���� ��ġ
        //        Vector3 targetPos = this.player.scanner.nearestTarget.position;
        //        //...�Ѿ� ���� ����
        //        Vector3 dir = targetPos - transform.position;
        //        //���� Vector�� ������ ����, ũ��� 1�� ��ȯ
        //        dir = dir.normalized;

        //        bullet.position = this.transform.position;
        //        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        //        //bullet �ʱ�ȭ
        //        bullet.GetComponent<Bullet>().Init(this.damage, 0, dir);  // -1 == ����, ���� ����
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
            //...bullet ���ӿ�����Ʈ ���� ��
            //Weapon�� �ڽ����� �ֱ� ���� transform������ ���������� �ֱ�
            Transform bullet;

            //...i�� childCount ���� �����
            if (i < this.transform.childCount)
            {
                //���� child�� ���
                bullet = this.transform.GetChild(i);
            }
            else
            {
                //...i�� ������ �Ѿ��
                //'���ڶ� �� ��ŭ' Ǯ������ ������
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                //���� transform�� �θ�� ����
                bullet.parent = this.transform;
                //bullet.transform.position = this.transform.position;
            }

            //Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
            //bullet.position = this.transform.position;
            //bullet �ʱ�ȭ
            bullet.GetComponent<Bullet>().Init(this.damage, -1, Vector3.zero);  // -1 == ����, ���� ����
        }
    }
}