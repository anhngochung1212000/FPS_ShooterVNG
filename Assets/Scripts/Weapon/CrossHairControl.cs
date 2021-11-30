using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHairControl : MonoBehaviour
{
    public RectTransform crossHairContain;
    public float restingWalkSize;
    public float maxWalkSize;
    public float maxFireSize;
    public float speedWalk;
    public float speedFire;
    public float startCurrentSize;
    [HideInInspector] public float currentSize;
    public Image[] lstTransform;
    public bool isWalk;
    public bool isFire;
    public CanvasGroup group;
    public GameObject notTarget;
    public bool isHide;

    public void OnHideCrossHair()
    {
        isHide = true;
        StopCoroutine("CrossHairAvaible");
        StartCoroutine("CrossHairAvaible", false);
    }

    public void OnShowCrossHair()
    {
        isHide = false;
        StopCoroutine("CrossHairAvaible");
        StartCoroutine("CrossHairAvaible", true);
    }

    IEnumerator CrossHairAvaible(bool flag)
    {
        yield return new WaitForSeconds(0.1f);
        crossHairContain.gameObject.SetActive(flag);
    }

    public void SetColorCrossHair(Color c)
    {
        for (int i = 0; i < lstTransform.Length; i++)
        {
            lstTransform[i].color = c;
        }
    }
    public void SetCurrentSize()
    {
        currentSize = startCurrentSize;
    }

    void Update()
    {
        if (isFire)
        {
            currentSize = Mathf.Lerp(currentSize, maxFireSize, Time.deltaTime * speedFire);
            group.alpha = 1;
        }
        else if (isWalk)
        {
            currentSize = Mathf.Lerp(currentSize, maxWalkSize, Time.deltaTime * speedWalk);
        }
        else
        {
            currentSize = Mathf.Lerp(currentSize, restingWalkSize, Time.deltaTime * speedWalk);

        }
        if (!isFire && group.alpha > 0)
            group.alpha = Mathf.Lerp(group.alpha, 0, Time.deltaTime * 10);
        crossHairContain.sizeDelta = new Vector2(currentSize, currentSize);
    }
}
