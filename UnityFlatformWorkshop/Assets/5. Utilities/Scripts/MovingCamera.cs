using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    [SerializeField]
    private Vector3 deltaXFree = Vector3.zero;
    [SerializeField]
    private Vector3 deltaYFree = Vector3.zero;
    [SerializeField]
    private float freeStepX = 5f;
    [SerializeField]
    private float freeStepY = 1f;
    [SerializeField]
    private float backGroundStep = 23f;
    [SerializeField]
    private float backgroundOffset = 3f;
    private int currentIndex = 0;
    private int otherIndex = 1;

    //[SerializeField]
    //private bool CanMove = true;
    [SerializeField] private MovingBackground[] movingBackgrounds;
    private float deltaX = 0;
    [SerializeField]
    

    public GameObject player;


    public float limitXCamera = 69f;
    public float limitYCamera = 1f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 
    }


    void Update()
    {

        CamTransForm();








        //if (CanMove)
        //{
        //    transform.position += moveSpeed * Time.deltaTime;
        //    deltaX += moveSpeed.x * Time.deltaTime;
        //}
        //if (deltaX >= backgroundStep.x)
        //{
        //    for (int i = 0; i < movingBackgrounds.Length; i++)
        //    {
        //        movingBackgrounds[i].transform.position += backgroundStep;
        //    }
        //    deltaX -= backgroundStep.x;
        //}
    }




    void CamTransForm()
    {
        //camera moving follow player
        if (player.transform.position.x < transform.position.x)
        {
            if (transform.position.x < 0) return;
            if (transform.position.x - player.transform.position.x > freeStepX)
            {
                deltaXFree.x = transform.position.x - (player.transform.position.x + freeStepX);

                transform.position -= deltaXFree;
            }

        }
        else
        {
            if (transform.position.x > limitXCamera) return;

            if (player.transform.position.x - transform.position.x > freeStepX)
            {
                deltaXFree.x = (player.transform.position.x - freeStepX) - transform.position.x;
                transform.position += deltaXFree;
            }


        }

      

        //background chay theo camera
        if (transform.position.x - movingBackgrounds[currentIndex].transform.position.x < -backgroundOffset)
        {
            if (movingBackgrounds[otherIndex].transform.position.x > movingBackgrounds[currentIndex].transform.position.x)
            {
                movingBackgrounds[otherIndex].transform.position -= new Vector3(2 * backGroundStep, 0, 0);
            }
            //doi background khac
            if (transform.position.x - movingBackgrounds[currentIndex].transform.position.x < -backGroundStep)
            {
                if (currentIndex == 0)
                {
                    currentIndex = 1;
                    otherIndex = 0;
                }
                else
                {
                    currentIndex = 0;
                    otherIndex = 1;
                }
            }


        }
        else if (transform.position.x - movingBackgrounds[currentIndex].transform.position.x > backgroundOffset)
        {
            if (movingBackgrounds[otherIndex].transform.position.x < movingBackgrounds[currentIndex].transform.position.x)
            {
                movingBackgrounds[otherIndex].transform.position += new Vector3(2 * backGroundStep, 0, 0);
            }
            //doi background khac
            if (transform.position.x - movingBackgrounds[currentIndex].transform.position.x > backGroundStep)
            {
                if (currentIndex == 0)
                {
                    currentIndex = 1;
                    otherIndex = 0;
                }
                else
                {
                    currentIndex = 0;
                    otherIndex = 1;
                }
            }


        }




        //camera moing fllow Y
        if (player.transform.position.y > transform.position.y)
        {
            if (transform.position.y > limitYCamera) return;
            if (player.transform.position.y - transform.position.y > freeStepY)
            {
                deltaYFree.y = player.transform.position.y - freeStepY - transform.position.y;

                transform.position += deltaYFree;
            }

        }
        else
        {
            if (transform.position.y < 0) return;
            if (transform.position.y - player.transform.position.y > freeStepY)
            {
                deltaYFree.y = transform.position.y - (player.transform.position.y + freeStepY);

                transform.position -= deltaYFree;
            }
        }
    }










  
}
