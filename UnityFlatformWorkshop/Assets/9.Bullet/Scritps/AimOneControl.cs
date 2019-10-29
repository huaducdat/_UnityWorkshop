using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AimOneControl : BulletControl
{
    #region
    public Transform shootPos;
    public int numberLiner = 2;
    [SerializeField]
    private float distance = 0.2f;

    [SerializeField]
    private BaseBullet prefabBulletAim;
    private BaseBullet currentBulletAim;
    private Vector3 aimTarget;
    private Tween moveTweenAim;

    private int currentBulletAimQuantity;

    #endregion

    void AimShoot(float velocity, float speedMoveTween)
    {

        for(int i= 0; i < numberLiner; i++)
        {
            currentBulletAim.transform.position = new Vector3(shootPos.position.x, shootPos.position.y + distance * i, shootPos.position.z);
            Vector3 direction = aimTarget - currentBulletAim.transform.position;
            currentBulletAim.GetComponent<Rigidbody>().velocity = direction.normalized * velocity;
            moveTweenAim = currentBulletAim.transform.DOMove(direction, speedMoveTween).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);


            currentBulletAimQuantity++;
        }



    }

    void RayCast(Vector2 Direc, float Range)
    {
        RaycastHit2D hit2D = Physics2D.Raycast(shootPos.position, Direc.normalized * Range);

        if(hit2D.collider != null)
        {
            aimTarget = hit2D.transform.position;
        }


    }



    public override void OnEnableDeSpawn()
    {
        //base.OnEnableDeSpawn();
        currentBulletAimQuantity--;
        moveTweenAim.Kill();
        if (currentBulletAimQuantity <= 0)
        {
            SimplePool.Despawn(gameObject);
        }

    }












    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
