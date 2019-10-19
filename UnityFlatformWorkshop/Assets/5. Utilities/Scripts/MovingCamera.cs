using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    [SerializeField]
    private Vector3 moveSpeed = new Vector3(2, 0, 0);
    [SerializeField]
    private bool CanMove = true;
    [SerializeField] private MovingBackground[] movingBackgrounds;
    private float deltaX = 0;
    [SerializeField]
    private Vector3 backgroundStep = new Vector3(23, 0, 0);

    void InputDirection()
    {
        if(Input.GetKey(KeyCode.A))
        {
            if (CanMove)
            {
                transform.position -= moveSpeed * Time.deltaTime;
                deltaX -= moveSpeed.x * Time.deltaTime;
            }
            if (deltaX <= -backgroundStep.x)
            {
                for (int i = 0; i < movingBackgrounds.Length; i++)
                {
                    movingBackgrounds[i].transform.position -= backgroundStep;
                }
                deltaX += backgroundStep.x;
            }
        }

        if(Input.GetKey(KeyCode.D))
        {
            if (CanMove)
            {
                transform.position += moveSpeed * Time.deltaTime;
                deltaX += moveSpeed.x * Time.deltaTime;
            }
            if (deltaX >= backgroundStep.x)
            {
                for (int i = 0; i < movingBackgrounds.Length; i++)
                {
                    movingBackgrounds[i].transform.position += backgroundStep;
                }
                deltaX -= backgroundStep.x;
            }
        }
    }


    void Update()
    {


        InputDirection();




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
}
