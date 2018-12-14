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
    bool czyBylFood = false;
    int actualHour = 0, oldHour = 0, random = 0, modification = 0;
    public Text foodCounter;
    public int food;
    public int teamMembers;
    public GameObject FOG;

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
        //Plane plane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //float point = 0f;
        RaycastHit hit;

        /*if (plane.Raycast(ray, out point))
        {
            targetPosition = ray.GetPoint(point);
        }*/

        if(Physics.Raycast(ray, out hit, 1000))
        {
            float distance = transform.position.magnitude - hit.point.magnitude;
            if(distance<0)
            {
                distance *= -1;
            }
            if (hit.transform.gameObject.tag == "City" && distance < 20f) //chodzenie tylko do miasta nie dalszego niż 20 jednostek odległości
            {
                targetPosition = hit.point;
                isMoving = true;
            }
        }

        
    }

    void MovePlayer()
    {
        transform.LookAt(targetPosition);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        //randomEvents
        if(oldHour!=actualHour)
        {
            MoveFOG();
            if (speed != 4)
            {
                modification = (int)speed*10;
            }
            else
            {
                modification = 0;
            }

            //random = Random.Range(0, 100);
            random = -20;
            if(random >= 0 && random < 10 + modification)
            {
                StartZombieAttack();
                StopPlayer();
            }
        }
        oldHour = actualHour;
        //day and hour
        actualHour = (int)HourReturn();
        if(actualHour==0 && czyBylDzien == false)
        {
            day++;
            czyBylDzien = true;
        }
        dayCounter.text = "hour: " + actualHour.ToString("0") + ", " + "day: " + day.ToString("0");

        //food every 6 hours
        if(actualHour%6==0 && czyBylFood == false)
        {
            czyBylFood = true;
            food = food - teamMembers;
            //food = food - Data_static.Player_tab.Count;
            if (food < 0)
            {
                food = 0;
            }
            foodCounter.text = "Supplies: " + food.ToString("0");
        }
        


        if (transform.position == targetPosition)
        {
            isMoving = false;
        }
    }

    float HourReturn()
    {
        //hour
        hour += 0.5f * Time.deltaTime;
        if (hour%6>=5.9f && czyBylFood == true)
        {
            czyBylFood = false;
        }

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

    public Canvas ZombieAttack;
    void StartZombieAttack()
    {
        ZombieAttack.enabled = true;
    }

    void MoveFOG()
    {
        Vector3 destiny;
        destiny.x = FOG.transform.position.x + 2;
        destiny.z = FOG.transform.position.z + 2;
        destiny.y = FOG.transform.position.y;
        FOG.transform.position = Vector3.MoveTowards(FOG.transform.position, destiny, 1000 * Time.deltaTime);
    }
} 
