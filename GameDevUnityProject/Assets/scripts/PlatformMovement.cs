using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlatformMovement : MonoBehaviour
{
    public enum LOOP_DIR
    {
        FORAWRD,
        BACKWARD,
        RANDOM,
        PING_PONG
    }


    public float gizmoRadius = 2f;
    public float speed = 5;
    public float waitTime = 0;
    public bool isMoving = true;
    public LOOP_DIR moveDir = LOOP_DIR.FORAWRD;
    public Platform platform;
    public List<GameObject> points;


    [HideInInspector]
    public int currentPoint = 0;
    bool movingForawrd = true;


    // Use this for initialization
    void Start()
    {
        if(points.Count > 0)
        {
            platform.transform.position = points[currentPoint].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving && points.Count > 0)
        {
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, points[currentPoint].transform.position, Time.deltaTime * speed);
            if (platform.transform.position == points[currentPoint].transform.position)
            { 
                switch (moveDir)
                {
                    case LOOP_DIR.FORAWRD:
                        MoveForward();
                        break;
                    case LOOP_DIR.BACKWARD:
                        MoveBackward();
                        break;
                    case LOOP_DIR.RANDOM:
                        MoveRandom();
                        break;
                    case LOOP_DIR.PING_PONG:
                        MovePingPong();
                        break;
                }


                isMoving = false;
                Invoke("KeepMoving", waitTime);
            }
        }
    }

    void MoveForward()
    {
        currentPoint++;
        if (currentPoint == points.Count)
        {
            currentPoint = 0;
        }
    }

    void MoveBackward()
    {
        currentPoint--;
        if (currentPoint == -1)
        {
            currentPoint = points.Count - 1;
        }
    }

    void MoveRandom()
    {
        int temp = Random.Range(0, points.Count - 1);
        while (currentPoint == temp)
        {
            temp = Random.Range(0, points.Count - 1);
        }
        currentPoint = temp;
    }

    void MovePingPong()
    {
        Debug.Log(currentPoint);
        if (movingForawrd)
        {
            MoveForward();
            if(currentPoint == 0)
            {
                movingForawrd = false;
            }
        }
        else
        {
            MoveBackward();
            if(currentPoint == 0)
            {
                movingForawrd = true;
            }
        }
    }

    void KeepMoving()
    {
        isMoving = true;
    }


    private void OnDrawGizmos()
    {
        if(points.Count > 0)
        {
            Vector3 prev = points[0].transform.position;
            Gizmos.DrawWireSphere(prev, gizmoRadius);

            for (int i = 1; i < points.Count; i++)
            {
                Gizmos.DrawWireSphere(points[i].transform.position, gizmoRadius);
                Gizmos.DrawLine(prev, points[i].transform.position);
                prev = points[i].transform.position;
            }

            Gizmos.DrawLine(prev, points[0].transform.position);
        }
    }
}
