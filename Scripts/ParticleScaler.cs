using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScaler : MonoBehaviour
{
    private RectTransform rect;

    void Awake()
    {
        this.rect = this.GetComponent<RectTransform>();
    }

    public void SizeUp(float size)
    {
        this.rect.localScale = new Vector3(size, size, size);
    }
}
