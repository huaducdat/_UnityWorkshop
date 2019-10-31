using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bird : Base_Enemy
{
 

   
    [Header("BirdMove")]
    public float delteX = 3;
    public float durationTweem = 1;

    private bool isRight;
    private float xPos;
    private float yPos;
    public Transform player;
    [Header("AttackDetails")]
    public float attackRange = 6f;

    [Header("EggBoom")]
    public GameObject Egg;
    public Transform shootPos;
    public float timeDropCD = 1f;
    private bool wasDrop = false;
    private float timeDropCDTemp;

    private void Awake()
    {
        TransformPos();
    }

    protected override void Start()
    {
        
        base.Start();
        
    }

    protected override void OnEnable()
    {
        base.OnEnable();       
        Move();
        //Debug.Log(xPos);
        //Debug.Log(yPos);
        
    }

    private void Update()
    {
        Attack();
    }
    void TransformPos()
    {
        xPos = transform.position.x;
        yPos = transform.position.y;
    }



    void Move()
    {
        MoveLeft();
    }


    void MoveRight()
    {
        Debug.Log("Run1");
        transform.localScale = new Vector3(-1, 1, 1);
        transform.DOMoveX(xPos + delteX, durationTweem).SetEase(Ease.InOutQuad).OnComplete(MoveLeft);
        transform.DOMoveY(yPos - 0.5f, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);

    }

    void MoveLeft()
    {
        Debug.Log("Run2");
        transform.localScale = new Vector3(1, 1, 1);
        transform.DOMoveX(xPos - delteX, durationTweem).SetEase(Ease.InOutQuad).OnComplete(MoveRight);
        transform.DOMoveY(yPos - 0.5f, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    void Attack()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, new Vector3(0, -attackRange, 0));
        if (hit2D.collider != null)
        {
            if (hit2D.collider.CompareTag("Player"))
            {
                if (!wasDrop)
                {
                    DropBoom();
                    wasDrop = true;
                }
            }
        }

        if(wasDrop)
        {
            timeDropCDTemp -= Time.deltaTime;
        }
        if(timeDropCDTemp <= 0)
        {
            wasDrop = false;
            timeDropCDTemp = timeDropCD;
        }


        float distance = Vector3.Distance(player.position, transform.position);

        if(distance <= attackRange)
        {
            animator.SetBool("IsAttack", true);
            
           
        }
        else
        {
            animator.SetBool("IsAttack", false);
        }




    }

    
   

 
    void DropBoom()
    {
      
            Debug.Log("Fire!!!");
            GameObject eggIns = Instantiate(Egg, shootPos.position, Quaternion.identity) as GameObject;
        
    }
  


    //private void Update()
    //{
    //    Debug.Log(xPos + "\n");
    //    Debug.Log(yPos + "\n");
    //}




}
