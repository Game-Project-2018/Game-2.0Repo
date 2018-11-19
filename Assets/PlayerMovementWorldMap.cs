using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMovementWorldMap : MonoBehaviour {

    public float speed;
    private Vector3 targetPosition;
    private bool isMoving;
    public Text dayCounter;
    private float hour = 0;
    private int day = 0;
    bool czyBylDzien = false;

    const int LEFT_MOUSE_BUTTON = 0;

    // Use this for initialization
    void Start() {
        targetPosition = transform.position;
        isMoving = false;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (Input.GetMouseButton(LEFT_MOUSE_BUTTON))
        {
            if(!EventSystem.current.IsPointerOverGameObject())
            {
                SetTargetPosition();
            }
        }

        if (isMoving)
        {
            MovePlayer();
        }
    }

    void SetTargetPosition()
    {
        Plane plane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float point = 0f;

        if (plane.Raycast(ray, out point))
        {
            targetPosition = ray.GetPoint(point);
        }

        isMoving = true;
    }

    void MovePlayer()
    {
        transform.LookAt(targetPosition);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        //day and hour
        int actualHour = (int)HourReturn();
        if(actualHour==0 && czyBylDzien == false)
        {
            day++;
            czyBylDzien = true;
        }
        dayCounter.text = "hour: " + actualHour.ToString("0") + ", " + "day: " + day.ToString("0");

        if (transform.position == targetPosition)
        {
            isMoving = false;
        }
    }

    float HourReturn()
    {
        //hour
        hour += 0.5f * Time.deltaTime;
        if (hour >= 24f)
        {
            czyBylDzien = false;
            return hour = 0;
        }
        else
        {
            return hour;
        }
    }

    public void StopPlayer()
    {
        targetPosition = transform.position;
    }
} 
