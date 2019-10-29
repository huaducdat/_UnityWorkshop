using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CricleControl : BulletControl
{
    public Transform shootPos;
    public int numLineBullet;
    [SerializeField]
    private BaseBullet prefabBullet;
    private BaseBullet currentBullet;

    private Tween rotateTween;
   

    void CricleChoot(Vector3 DirecShoot, Vector3 AngleShoot, float Velocity, float MoveTweenSpeed, float Damage, float rotateSpeed, float RotateTweenSpeed)
    {
        for(int i = 0; i < numLineBullet; i++)
        {
            currentBullet = SimplePool.Spawn(prefabBullet, shootPos.position, Quaternion.identity);
            DirecShoot.x = Mathf.Cos(360 / numLineBullet * i);
            DirecShoot.y = Mathf.Sin(360 / numLineBullet * i);
            InitBulletInControl(DirecShoot, AngleShoot, Velocity, MoveTweenSpeed, Damage);

        }

        rotateTween = transform.DORotate(new Vector3(0, 0, rotateSpeed), RotateTweenSpeed).SetEase(Ease.OutElastic).SetLoops(-1, LoopType.Incremental);

    }

    public override void OnEnableDeSpawn()
    {
        currentBulletQuantity--;
        rotateTween.Kill();
        if (currentBulletQuantity <= 0)
        {
            SimplePool.Despawn(gameObject);
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
