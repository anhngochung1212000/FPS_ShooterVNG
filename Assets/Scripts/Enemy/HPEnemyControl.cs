using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPEnemyControl : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public RectTransform hpValue;
    private float totalSize;
    private float currentSize;
    private Tweener tween;
    // Start is called before the first frame update
    void Start()
    {
        totalSize = hpValue.sizeDelta.x;
        currentSize = totalSize;
        gameObject.GetComponentInParent<EnemyControl>().onHpChange += OnHpChange;
    }

    void OnHpChange(int hp, int totalHP)
    {
        canvasGroup.alpha = 1;
        float value = (float)hp / (float)totalHP;
        currentSize = (value * totalSize);
        if (tween != null)
        {
            tween.Kill();
        }
        tween = hpValue.DOSizeDelta(new Vector2(currentSize, hpValue.sizeDelta.y), 0.1f).OnComplete(() =>
        {
            if (currentSize < 1)
            {
                canvasGroup.alpha = 0;
            }
            else
            {
                StopCoroutine("HideHPPanel");
                StartCoroutine("HideHPPanel");
            }

        });

    }
    IEnumerator HideHPPanel()
    {
        yield return new WaitForSeconds(1f);
        canvasGroup.alpha = 0;
    }
}
