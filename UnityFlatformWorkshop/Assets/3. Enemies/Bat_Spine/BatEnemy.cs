using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Spine;

public class BatEnemy : BaseEnemySpine
{
    
    public float step = 10f;
    private bool isRun = false;
    public float deltaRun = 3f;
    public Transform shotPos;
    public GameObject bulletPrefab;
    public float speedShot = 100f;
    //public float CDShootTime = 1.5f;
    private float tempDeltaRun = 0;
    private int count = 0;
    //Tween moveY;
    //private float yPos;
    public float deltaDistance = 3;
    private float tempStep;
    private bool isRunAni;
    private int countAni;

    protected override void Awake()
    {
        base.Awake();
        //YPos();
    }

    protected override void OnEnable()
    {
      

    }
    protected override void Start()
    {
        base.Start();
        SetIdle();
    }

    protected override void Update()
    {
        FindPlayer();
       
        
    }


    //void YPos()
    //{
    //    yPos = transform.position.y;
    //}



    protected override void OnAnimationComplete(TrackEntry entry)
    {
        switch (currentAction)
        {
            case EnemyAction.Hit:
                SetRun();
                break;
            case EnemyAction.Hurt:
                SetIdle();
                break;
            case EnemyAction.Run:
                //SetIdle();
                break;
            case EnemyAction.Idle:
                break;

        }

        if (string.CompareOrdinal(entry.animation.name, die) == 0)
        {
            DeSpawnOnDead();
        }
    }




    protected override void FindPlayer()
    {


        if (transform.position.y <= player.transform.position.y + 0.5f)
        {
            transform.position += new Vector3(0, step / 5 * Time.deltaTime);
        }
        else if(transform.position.y > player.transform.position.y)
        {



            float distance = Vector3.Distance(player.transform.position, transform.position);

            //if (distance > rangeFind)
            //{
                tempStep += step / 7 * Time.deltaTime;

                if (tempStep < deltaDistance)
                {
                    TurnRight();
                    transform.position += new Vector3(step / 7 * Time.deltaTime, 0, 0);
                }
                if (tempStep >= deltaDistance)
                {
                    TurnLeft();
                    transform.position -= new Vector3(step / 7 * Time.deltaTime, 0, 0);
                }

                if (tempStep >= 2 * deltaDistance) tempStep = 0;
            //}

            //Debug.Log(distance);
            if (distance <= rangeFind && distance > rangeRun && !isRun && count == 0)
            {
                isRunAni = true;
                countAni++;

                if (player.transform.position.x < transform.position.x)
                {
                    TurnLeft();
                }
                else if (player.transform.position.x > transform.position.x)
                {
                    TurnRight();
                }
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step / 5 * Time.deltaTime);


                if (!wasFire)
                {
                    wasFire = true;
                    Fire();

                }

            }


            if (countAni >= 2) countAni = 2;
            if (isRunAni && countAni == 1)
            {
                SetRun();
            }










            if (distance <= rangeRun)
            {
                isRun = true;

            }

            if (isRun)
            {
                Vector3 dir = transform.position - player.transform.position;
                transform.position += dir.normalized * step / 2 * Time.deltaTime;
                tempDeltaRun += step / 2 * Time.deltaTime;
                if (player.transform.position.x < transform.position.x) TurnRight();
                if (player.transform.position.x > transform.position.x) TurnLeft();

            }

            if (tempDeltaRun >= deltaRun)
            {
                isRun = false;
                tempDeltaRun = 0;
                count = 1;
            }





            if (!isRun && count == 1)
            {


                if (player.transform.position.x < transform.position.x)
                {
                    TurnLeft();
                }
                else if (player.transform.position.x > transform.position.x)
                {
                    TurnRight();
                }
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step / 5 * Time.deltaTime);

                if (!wasFire)
                {
                    wasFire = true;
                    Fire();

                }


            }







            //CDTime Fire
            if (wasFire)
            {
                tempTime += Time.deltaTime;

            }
            if (tempTime >= timeFireCD)
            {
                wasFire = false;
                tempTime = 0;

            }

        }
    }

    
    protected override void Fire()
    {
        base.Fire();

        Vector3 direcShot = player.transform.position - shotPos.position;

        
        GameObject instanBullet = Instantiate(bulletPrefab, shotPos.position, Quaternion.identity);

        float angle = Vector3.SignedAngle(Vector3.right, player.transform.position - shotPos.position, Vector3.forward);
        instanBullet.transform.localEulerAngles = new Vector3(0, 0, angle);

        instanBullet.GetComponent<Rigidbody2D>().velocity = direcShot.normalized * speedShot * Time.deltaTime;

        
    }

    
    //IEnumerator Shoot()
    //{
    //    Debug.Log("Fire!");
    //    Fire();
    //    yield return new WaitForSeconds(CDShootTime);
        
    //}

}
