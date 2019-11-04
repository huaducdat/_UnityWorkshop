using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;


public class BaseEnemySpine : Base_Enemy
{
    public string idle;
    public string hurt;
    public string hit;
    public string run;
    public string jump;
    public string die;

    public string attackEvent;
    public SkeletonAnimation skeletonAni;


    public float moveMultiplier = 1;

    Rigidbody2D rigiEnemySnpine;
    [SerializeField]
    private float jumpForce = 200f;

    [Header("FindPlayer varrible")]
    public GameObject player;
    public string playerTag;
    public float rangeFind;
    public float rangeRun;
    public float timeFireCD = 2f;
    protected bool wasFire = false;
    protected float tempTime = 0;

    protected virtual void Awake()
    {
        rigiEnemySnpine = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag(playerTag);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();

        skeletonAni.state.Complete += OnAnimationComplete;
        skeletonAni.AnimationState.Event += HandleAnimEvent;
    }


    protected override void Update()
    {
        base.Update();
        FindPlayer();
    }





    void Jump()
    {
        if (isGround)
        {
            rigiEnemySnpine.AddForce(new Vector2(0, jumpForce));
            SetJump();
        }
        
    }




    protected virtual void Fire()
    {
        SetAttack();

    }


    

    protected virtual void FindPlayer()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < rangeFind && distance > rangeRun)
        {
            if (player.transform.position.x < transform.position.x)
            {
                MoveLeft();
            }
            else if (player.transform.position.x > transform.position.x)
            {
                MoveRight();
            }

            

            if (!wasFire)
            {
                Fire();
                wasFire = true;
            }         

        }

        if (wasFire)
        {
            tempTime += Time.deltaTime;
        }
        if (tempTime >= timeFireCD)
        {
            wasFire = !wasFire;
            tempTime = 0;
        }



        if (distance < rangeRun)
        {
            if (player.transform.position.x < transform.position.x)
            {
                transform.position += moveStep;
            }
            if (player.transform.position.x > transform.position.x)
            {
                transform.position -= moveStep;
            }



        }

    }











   void OnAnimationComplete(TrackEntry entry)
    {
        switch(currentAction)
        {
            case EnemyAction.Hit:
                SetIdle();
                break;
            case EnemyAction.Hurt:
                SetIdle();
                break;
            case EnemyAction.Run:
                SetIdle();
                break;
            case EnemyAction.Idle:
                break;
                
        }

        if(string.CompareOrdinal(entry.animation.name, die) == 0)
        {
            DeSpawnOnDead();
        }

    }

    void HandleAnimEvent(TrackEntry entry, Spine.Event e)
    {
        if(string.CompareOrdinal(attackEvent, e.data.name) == 0)
        {
            StartCoroutine(EneEnableAttackBox());
        }
    }

    [SerializeField]
    private HitBox attackBox;
    
    IEnumerator EneEnableAttackBox()
    {
        attackBox.gameObject.SetActive(true);
        yield return new WaitForSeconds(.2f);
        attackBox.gameObject.SetActive(false);
    }








    void SetAnimation(string ani, bool isLoop)
    {
        if(ani == idle)
        {
            skeletonAni.Skeleton.SetToSetupPose();
        }
        if(ani == run)
        {
            skeletonAni.timeScale = moveMultiplier;
            skeletonAni.state.SetAnimation(0, ani, isLoop);
        }
        else
        {
            skeletonAni.timeScale = 1;
            skeletonAni.state.SetAnimation(0, ani, isLoop);
        }



    }



    protected void SetIdle()
    {
        if (currentAction == EnemyAction.Idle) return;
        else
        {
            currentAction = EnemyAction.Idle;
            moveStep.x = 0;
            skeletonAni.state.ClearTrack(0);
            SetAnimation(idle, true);
        }

    }


    private Vector3 moveStep = Vector3.zero;
    public float speedMove;
    void MoveRight()
    {
        currentAction = EnemyAction.Run;
        TurnRight();
        SetAnimation(run, true);
        moveStep.x = speedMove * Time.deltaTime;
        
    }


    protected void TurnRight()
    {
        transform.localScale = Vector3.one;
    }

    void MoveLeft()
    {
        currentAction = EnemyAction.Run;
        TurnLeft();
        SetAnimation(run, true);
        moveStep.x = speedMove * Time.deltaTime;
    }


    protected void TurnLeft()
    {
        transform.localScale = new Vector3(-1, 1, 1);
    }

    protected void SetRun()
    {
        SetAnimation(run, true);
    }

    protected void SetAttack()
    {
        currentAction = EnemyAction.Hit;
        //moveStep.x = 0;
        skeletonAni.state.ClearTrack(0);
        SetAnimation(hit, false);

    }

    void SetJump()
    {
        currentAction = EnemyAction.Jump;
        skeletonAni.state.ClearTrack(0);
        SetAnimation(jump, true);
    }

    protected void SetHurt()
    {
        currentAction = EnemyAction.Hurt;
        skeletonAni.state.ClearTrack(0);
        SetAnimation(hurt, false);
    }
    
    void SetDie()
    {
        currentAction = EnemyAction.Die;
        skeletonAni.state.ClearTrack(0);
        SetAnimation(die, false);
    }



    protected void DeSpawnOnDead()
    {
        SimplePool.Despawn(gameObject);
    }


    //

    private bool isGround;
    private LayerMask layerMask;
    public Transform groundCheck;
    private float checkRadius = .2f;
    void CheckGround()
    {
        bool wasGround = isGround;
        isGround = false;
        Collider2D[] collider2D = Physics2D.OverlapCircleAll(groundCheck.position, checkRadius, layerMask);

        for(int i = 0; i < collider2D.Length; i++)
        {
            if(collider2D[i].gameObject != gameObject )
            {
                isGround = true;
                if(!wasGround)
                {
                    SetAnimation(idle, true);
                }
            }
        }
           

    }
















}
