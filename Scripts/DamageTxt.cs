using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTxt : MonoBehaviour
{
    TextMeshPro textPro;

    public float textSpeed;
    public float alphaSpeed;
    public float destroyTime;

    void Awake()
    {
        this.textPro = GetComponent<TextMeshPro>();
    }

    private void Start()
    { 

    }

    public void Init(float damage)
    {
        this.textPro.text = damage.ToString();

        Invoke("DestroyDamageText", destroyTime);
    }

    private void Update()
    {
        transform.Translate(new Vector3(0, textSpeed * Time.deltaTime, 0));  //À§·Î ¿Ã¶ó°¡´Â
    }
   
}
