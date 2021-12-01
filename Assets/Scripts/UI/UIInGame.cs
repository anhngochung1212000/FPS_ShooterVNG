using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIInGame : MonoBehaviour
{
    [SerializeField] Text textBulletAmo;
    [SerializeField] Text textHpValue;
    [SerializeField] Text textEnemyCount;
    [SerializeField] Image imageGun;
    [SerializeField] Image imageGunIcon;
    [SerializeField] Button buttonChangeWeapon;
    [SerializeField] Button buttonFire;
    [SerializeField] Button buttonReload;
    [SerializeField] Button buttonRePlay;
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
        }
    }

    void Awake()
    {
        PlayerControl.onHPPlayerChange += OnHPPlayerChange;
        WeaponControl.onChangeWeapon += OnChangedWeapon;
        WeaponControl.onUpdateBulletAmount += UpdateBulletOfGun;
        MissionControl.onPlayerVictory += OnPlayerVictory;
        MissionControl.onEnemyDead += OnEnemyDead;
        buttonReload.onClick.AddListener(OnButtonReloadClick);
        buttonChangeWeapon.onClick.AddListener(OnButtonChangeWeaponClick);
        buttonRePlay.onClick.AddListener(OnButtonReplayClick);
        toggleAutoFire.onValueChanged.AddListener(OnAutoFireClick);

    }

    void OnDestroy()
    {
        PlayerControl.onHPPlayerChange -= OnHPPlayerChange;
        WeaponControl.onChangeWeapon -= OnChangedWeapon;
        MissionControl.onPlayerVictory -= OnPlayerVictory;
        WeaponControl.onUpdateBulletAmount -= UpdateBulletOfGun;
        MissionControl.onEnemyDead -= OnEnemyDead;
        buttonChangeWeapon.onClick.RemoveListener(OnButtonChangeWeaponClick);
        buttonReload.onClick.RemoveListener(OnButtonReloadClick);
        buttonRePlay.onClick.RemoveListener(OnButtonReplayClick);
        toggleAutoFire.onValueChanged.RemoveListener(OnAutoFireClick);
    }

    void OnHPPlayerChange(int currentHP)
    {
        int d = int.Parse(textHpValue.text);

        if(d <= currentHP)
        {
            textHpValue.text = PlayerControl.Instance.currentHP.ToString();
            return;
        }

        BloodAlpha += 0.8f;
        if (tween != null)
        {
            tween.Kill();
        }
        
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
        textHpValue.text = PlayerControl.Instance.currentHP.ToString();
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

    void OnEnemyDead(int count,int total)
    {
        textEnemyCount.text = "Kill Enemy: " + count + "/" + total;
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

    void OnButtonReplayClick()
    {
        GameManager.ReloadGameData();
        panelResult.gameObject.SetActive(false);
        textEnemyCount.text = "Kill Enemy: " + 0 + "/" + MissionControl.EnemyDead;
    }
}
