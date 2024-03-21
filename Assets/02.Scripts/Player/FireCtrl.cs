using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//1.총알 프리팹 2. 발사위치 3. 총소리 오디오소스 오디오 클립
public class FireCtrl : MonoBehaviour
{
    
    [SerializeField] private Transform firePos;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip fireClip;
    [SerializeField] private ParticleSystem[] muzzleFlash;
    [SerializeField] private ParticleSystem CartridgeEjectEffect;
    [SerializeField] private Image magazineImg;
    [SerializeField] private Text magazineText;
    [SerializeField] private Animator animator;
    [SerializeField] private Player_Mecanim player_Mecanim;
    private readonly int hashReload = Animator.StringToHash("ReloadTrigger");
    private readonly int hashFire = Animator.StringToHash("FireTrigger");
    private int remaingBullet = 0;
    private readonly int maxBullet = 10;
    private float timePrev;
    private float fireRate = 0.1f; //발사 간격 시간 
    private Player_Mecanim player;
    private bool isReloding = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        player_Mecanim = GetComponent<Player_Mecanim>();
        firePos = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Transform>();
        player = GetComponent<Player_Mecanim>();
        source = GetComponent<AudioSource>();
        magazineImg = GameObject.Find("Panel-Magazine").transform.GetChild(2).GetComponent<Image>();
        magazineText = GameObject.Find("Panel-Magazine").transform.GetChild(0).GetComponent<Text>();
        for (int i = 0; i < 2; i++)
            muzzleFlash[i] = firePos.GetComponentsInChildren<ParticleSystem>()[i];
        for(int i=0; i<2; i++)
         muzzleFlash[i].Stop();
     
        CartridgeEjectEffect = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
        fireClip = Resources.Load<AudioClip>("Sounds/p_ak_1");
      
        CartridgeEjectEffect.Stop();
        remaingBullet = maxBullet;
        remaingBullet = Mathf.Clamp(remaingBullet, 0, 10);
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
             if(!player.isRun &&!isReloding)
             {
                --remaingBullet;
                 Fire();
                if(remaingBullet ==0)
                {
                    StartCoroutine(Reloading());
                }
               
            }
                 
          
        }
        else if(Input.GetMouseButtonUp(0))
        {

            for (int i = 0; i < 2; i++)
                muzzleFlash[i].Stop();
            CartridgeEjectEffect.Stop();
        }
    }
    void Fire()
    {
        //if (player_Mecanim.v > 0) return;
        var _bullet = PoolingManager.P_Instance.GetPlayerBullet();
        _bullet.transform.position = firePos.position;
        _bullet.transform.rotation = firePos.rotation;
        _bullet.gameObject.SetActive(true);


        source.PlayOneShot(fireClip,1.0f);
        
        animator.SetTrigger(hashFire);

        for (int i=0; i<2; i++)
          muzzleFlash[i].Play();
        CartridgeEjectEffect.Play();
        magazineImg.fillAmount = (float)remaingBullet / (float)maxBullet;
        
        magazineTextShow();
    }
    void magazineTextShow()
    {
        magazineText.text = "<color=#ff0000>" + remaingBullet.ToString() + "</color>" + "/" + maxBullet.ToString();
    }
    IEnumerator Reloading()
    {
        magazineTextShow();
        isReloding = true;
        animator.SetTrigger(hashReload);
        for (int i = 0; i < 2; i++)
            muzzleFlash[i].Stop();
        CartridgeEjectEffect.Stop();
        yield return new WaitForSeconds(1.5f);
        magazineImg.fillAmount = 1.0f;
        isReloding = false;
        remaingBullet = maxBullet;
        magazineTextShow();

    }
    
}
