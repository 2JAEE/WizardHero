using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiGame : MonoBehaviour
{

    public GameObject txtDamagePrefab;
    public Camera cam;
    public Canvas canvas;

    public void CreateDamageText(Vector3 worldPos, float damage)
    {
        var go = Instantiate(this.txtDamagePrefab, canvas.transform);

        var screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPos);

        var canvasRectTransform = this.canvas.GetComponent<RectTransform>();

        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPos, this.cam, out localPos);

        Debug.LogFormat("{0}, {1}, {2}", worldPos, screenPos, localPos);

        go.transform.localPosition = localPos;
    }
}
