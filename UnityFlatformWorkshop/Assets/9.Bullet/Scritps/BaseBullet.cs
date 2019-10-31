using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BaseBullet : MonoBehaviour
{


    #region
    public float _gravityScale = 0;
    private BaseBullet baseBullet;
    private BaseBullet currentBullet;

    public Transform shotPos;

    public string groudTag = "Ground";
    public string hitTag = "";

    public float bulletDamage = 1f;

    Tween moveTween;

    private BulletControl bulletControl;



    #endregion


    private Vector3 lastPos;
    private void OnEnable()
    {
        lastPos = currentBullet.transform.position;
    }

    private void Update()
    {
        if (lastPos != currentBullet.transform.position)
        {
            float angle = Vector3.SignedAngle(Vector3.right, currentBullet.transform.position - lastPos, Vector3.forward);
            currentBullet.transform.localEulerAngles = new Vector3(0, 0, angle);
        }

        lastPos = currentBullet.transform.position;
    }










    public void SetBulletGravity()
    {
        currentBullet.GetComponent<Rigidbody2D>().gravityScale = _gravityScale;
    }


    public void SetBulletControl(BulletControl controlBase)
    {
        bulletControl = controlBase;
    }

    public void SetBulletDame(float BulletDame)
    {
        bulletDamage = BulletDame;
    }





    void InitBaseBullet()
    {
        currentBullet = SimplePool.Spawn(baseBullet, shotPos.position, Quaternion.identity);     
    }

    public void SetBaseBulletVelocity(Vector3 direction, Vector3 angleShot, float velocityBullet, float speedMoveTween)
    {
        currentBullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * velocityBullet;
        float shotAngle = Vector3.SignedAngle(direction, angleShot, Vector3.forward);
        currentBullet.transform.localEulerAngles = new Vector3(0, 0, shotAngle);
        currentBullet.transform.localScale = direction;

        moveTween = currentBullet.transform.DOLocalMove(angleShot, speedMoveTween).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }


    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        if(collision2D.CompareTag(groudTag))
        {
            SimplePool.Despawn(gameObject);
            if(bulletControl != null)
            {
                bulletControl.OnEnableDeSpawn();
            }
            if (moveTween != null) moveTween.Kill();
        }
        if(collision2D.CompareTag(hitTag))
        {
            IActor health = collision2D.GetComponent<IActor>();
            if (health != null)
            {
                health.TakeDame(bulletDamage);
            }
            SimplePool.Despawn(gameObject);

            if (bulletControl != null)
            {
                bulletControl.OnEnableDeSpawn();
            }
            if (moveTween != null) moveTween.Kill();
        }
    }

  













    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
