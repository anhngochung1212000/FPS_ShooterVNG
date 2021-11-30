using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIInGame : MonoBehaviour
{
    [SerializeField] Text textBulletAmo;
    [SerializeField] Text textHpValue;
    [SerializeField] Image imageGun;
    [SerializeField] Image imageGunIcon;
    [SerializeField] Button buttonChangeWeapon;
    [SerializeField] Button buttonFire;
    [SerializeField] Button buttonReload;
    [SerializeField] Toggle toggleAutoFire;
    [SerializeField] Image imageBlood;
    [SerializeField] RectTransform panelResult;
    [SerializeField] Text textResult;
    Tweener tween;
    public float BloodAlpha
    {
        get { return imageBlood.color.a; }

        set
        {
            var newColor = imageBlood.color;
            newColor.a = value;
            imageBlood.color = newColor;
            if (value != 0)
                imageBlood.gameObject.SetActive(true);
            else
                PlayerControl.instance.isHurt = false;
        }
    }

    void Awake()
    {
        PlayerControl.onHPPlayerChange += OnHPPlayerChange;
        WeaponControl.onChangeWeapon += OnChangedWeapon;
        WeaponControl.onUpdateBulletAmount += UpdateBulletOfGun;
        MissionControl.onPlayerVictory += OnPlayerVictory;
        buttonReload.onClick.AddListener(OnButtonReloadClick);
        buttonChangeWeapon.onClick.AddListener(OnButtonChangeWeaponClick);
        toggleAutoFire.onValueChanged.AddListener(OnAutoFireClick);
    }

    void OnDestroy()
    {
        PlayerControl.onHPPlayerChange -= OnHPPlayerChange;
        WeaponControl.onChangeWeapon -= OnChangedWeapon;
        MissionControl.onPlayerVictory -= OnPlayerVictory;
        WeaponControl.onUpdateBulletAmount -= UpdateBulletOfGun;
        buttonChangeWeapon.onClick.RemoveListener(OnButtonChangeWeaponClick);
        buttonReload.onClick.RemoveListener(OnButtonReloadClick);
        toggleAutoFire.onValueChanged.RemoveListener(OnAutoFireClick);
    }

    void OnHPPlayerChange(int currentHP)
    {
        BloodAlpha += 0.8f;
        if (tween != null)
        {
            tween.Kill();
        }
        int d = int.Parse(textHpValue.text);
        tween = DOTween.To(() => d, x => d = x, currentHP, 0.1f).OnUpdate(() =>
        {
            textHpValue.text = d.ToString();
        }).SetEase(Ease.Linear);
        if (currentHP == 0)
             OnPlayerDead();
    }

    IEnumerator Start()
    {
        yield return new WaitUntil(() => WeaponControl.currentWeapon != null);
        textBulletAmo.text = WeaponControl.currentWeapon.dataBase.clipSizeBullet + "/" + WeaponControl.currentWeapon.dataBase.totalBullet;
        textHpValue.text = PlayerControl.instance.currentHP.ToString();
        BloodAlpha = 0;
    }

    void Update()
    {
        if (BloodAlpha > 0)
        {
            BloodAlpha = Mathf.Clamp01(BloodAlpha - 0.5f * Time.deltaTime);
        }
    }

    void OnAutoFireClick(bool check)
    {
        buttonFire.gameObject.SetActive(!check);
        PlayerControl.isAutoFire = check;
    }

    public void OnButtonFireClick(bool isFire)
    {
        PlayerControl.isAutoFire = isFire;
    }

    void OnButtonChangeWeaponClick()
    {
        WeaponControl.ChangeGun();
    }

    void OnButtonReloadClick()
    {
        WeaponControl.currentWeapon.ReLoadBullet();
    }

    void OnChangedWeapon(WeaponBehaviour weaponBehaviour)
    {
        UpdateBulletOfGun(weaponBehaviour);
        imageGun.sprite = Resources.Load<Sprite>("Images/" + weaponBehaviour.dataBase.imageName);
        imageGunIcon.sprite = Resources.Load<Sprite>("Images/" + weaponBehaviour.dataBase.imageName);
    }

    void UpdateBulletOfGun(WeaponBehaviour weaponBehaviour)
    {
        textBulletAmo.text = weaponBehaviour.countBulletCurent + "/"+ weaponBehaviour.dataBase.totalBullet;
       
    }

    void OnPlayerDead()
    {
        panelResult.gameObject.SetActive(true);
        textResult.text = "You lose!";
    }

    void OnPlayerVictory()
    {
        panelResult.gameObject.SetActive(true);
        textResult.text = "You win!";
    }
}
