using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    [Header("----Ball Settings")]
    public GameObject[] balls;
    public GameObject FirePoint;
    [SerializeField] private float  ballForce;
    int activeBallIndex;

    [Header("----Level Settings")]
    [SerializeField] private int targetBallCount;
    [SerializeField] private int availableBallCount;
    int enterBallCount;
    public Slider levelSlider;
    public TextMeshProUGUI remainBallCount_text;


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
