using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 direction;
    public static float forwardSpeed = 150;

    private int desiredLane = 1; // 0-left, 1-middle, 2-right
    public float laneDistance = 2.5f;
    public int smoothMovementSpeed = 30;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        direction.z = forwardSpeed;

        #region ArrowButton
        //arrow buttons
        /*        if (Input.GetKeyDown(KeyCode.RightArrow) || SwipeManager.swipeRight )
                {
                    desiredLane++;
                    if (desiredLane == 3)
                    {
                        desiredLane = 2;
                    }
                }*/

        /*        if (Input.GetKeyDown(KeyCode.LeftArrow) || SwipeManager.swipeLeft )
                {
                    desiredLane--;
                    if (desiredLane == -1)
                    {
                        desiredLane = 0;
                    }
                }*/
        #endregion

        #region SwipeLeftRight
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;

            if (endTouchPosition.x < startTouchPosition.x)
            {
                //LeftSwipe
                desiredLane--;
                 
                if (desiredLane == -1)
                {
                    desiredLane = 0;

                }

            }
            else if (endTouchPosition.x > startTouchPosition.x)
            {
                //RightSwipe
                desiredLane++;

                if (desiredLane == 3)
                {
                    desiredLane = 2;
                }
            }
        }
        #endregion

        //movement calculation

        Vector3 targetPosition =
            transform.position.z * transform.forward +
            transform.position.y * transform.up;

        if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }
        else if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }


        controller.Move(direction * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothMovementSpeed * Time.deltaTime);
        controller.center = controller.center;

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Go")
        {
            HUDManager.UpdateFoodPoints(1, 0, 0);
            HUDManager.UpdateScoreEnergyPoints(50, .025f);
        }

        else if(hit.transform.tag == "Grow")
        {
            HUDManager.UpdateFoodPoints(0, 1, 0);
            HUDManager.UpdateScoreEnergyPoints(50, .025f);
        }

        else if(hit.transform.tag == "Glow")
        {
            HUDManager.UpdateFoodPoints(0, 0, 1);
            HUDManager.UpdateScoreEnergyPoints(50, .025f);
        }

        else if (hit.transform.tag == "Junk")
        {
            HUDManager.ResetFoodPoints();
            HUDManager.UpdateScoreEnergyPoints(-100, -.1f);
        }

        Destroy(hit.gameObject);
    }
}