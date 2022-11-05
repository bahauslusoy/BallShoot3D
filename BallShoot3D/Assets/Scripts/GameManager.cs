using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] balls;
    public GameObject FirePoint;
    public float ballForce;
    int activeBallIndex;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            balls[activeBallIndex].transform.SetPositionAndRotation(FirePoint.transform.position, FirePoint.transform.rotation);
            balls[activeBallIndex].SetActive(true);
            balls[activeBallIndex].GetComponent<Rigidbody>().AddForce(balls[activeBallIndex].transform.TransformDirection(90, 90, 0)
             * ballForce, ForceMode.Force);

            
            if (balls.Length - 1 == activeBallIndex)
            {
                activeBallIndex = 0;
            }
            else
            {
                activeBallIndex++;
            }
        }

    }
}
