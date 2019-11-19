using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;




public class EnemyBird : MonoBehaviour
{
    private Vector3 Position = Vector3.zero;

    Tween moveTween;

    private Vector3 distanceVector = new Vector3(2, 0, 0);

    private bool isRight;

    private Animator birdAnimator;

    public Transform player;

    private float time = 0;
    [SerializeField]
    private float distanceAttack = 2f;

    private float speedDoTween = 2f;

    void EnemyPos()
    {
        Position = transform.position;
    }

    public void BirdMove()
    {
        if (player.position.x - transform.position.x > 0)
        {

            moveTween = transform.DOMoveX(Position.x + distanceVector.x, speedDoTween).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
            //moveTween = transform.DOMoveY(Position.y + distanceVector.y, speedDoTween).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo).OnComplete(Flip);


            

        }
        else if(player.position.x - transform.position.x < 0)
        {
            moveTween = transform.DOMoveX(Position.x - distanceVector.x, speedDoTween).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
            //moveTween = transform.DOMoveY(Position.y - distanceVector.y, speedDoTween).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo).OnComplete(Flip);
            
        }



    }


    void Flip()
    {




        if (isRight)
        {
            Vector3 direc = Vector3.one;
            direc.x = -1;

            transform.localScale = new Vector3(transform.localScale.x * direc.x, transform.localScale.y, transform.localScale.z);

            isRight = !isRight;
        }



        //moveTween = transform.DOMoveX(Position.x + distanceVector.x, speedDoTween).SetEase(Ease.Linear).SetLoops(2, LoopType.Incremental);

    }

   
    void Attack()
    {
        //time += Time.deltaTime;

        float distance = Vector3.Distance(player.position, transform.position);

        if(distance <= distanceAttack)
        {
            birdAnimator.SetBool("IsAttack", true);
            time = 0;

            
        }
        else
        {
            birdAnimator.SetBool("isAttack", false);
        }


        moveTween = transform.DOMoveX(Position.x + distanceVector.x, speedDoTween).SetEase(Ease.Linear).SetLoops(2, LoopType.Incremental);


    }



    private void Update()
    {
        BirdMove();
        Attack();
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
