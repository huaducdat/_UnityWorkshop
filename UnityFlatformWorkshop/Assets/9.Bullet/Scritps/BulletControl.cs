using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    #region
    [SerializeField]
    private BaseBullet prefabBullet;
    private BaseBullet currentBullet;
    public Transform shotPos;

    public int currentBulletQuantity;

    



    #endregion



    public void InitBulletInControl(Vector3 DirecShot, Vector3 AngleShoot, float Velocity, float SpeedMoveTween, float Damage)
    {
        currentBullet = SimplePool.Spawn(prefabBullet, shotPos.position, Quaternion.identity);
        currentBullet.SetBulletControl(this);
        currentBullet.transform.SetParent(transform);
        currentBullet.SetBaseBulletVelocity(DirecShot, AngleShoot, Velocity, SpeedMoveTween);
        currentBullet.SetBulletDame(Damage);
        currentBulletQuantity++;
    }



    public virtual void OnEnableDeSpawn()
    {
        currentBulletQuantity--;
        if(currentBulletQuantity <= 0)
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
