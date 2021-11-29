using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CrossHairControl : MonoBehaviour
{
    [SerializeField] RectTransform[] rects;
    [SerializeField] Vector2[] sideMoves;
    List<Vector2> ogrinPoss = new List<Vector2>();
    public float delta;

    void Awake()
    {
        for (int i = 0; i < rects.Length; i++)
        {
            ogrinPoss.Add(rects[i].anchoredPosition);
        }
    }

    public void UpdateCrossHair(float damping)
    {
        for (int i = 0; i < rects.Length; i++)
        {
            rects[i].anchoredPosition = ogrinPoss[i] + sideMoves[i] * damping * delta;
        }

    }
}
