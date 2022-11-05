using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CylinderManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler  // Herhangi bir şeye tıklanıp tıklanmadığını anlamak için
{

    bool buttonPressed;
    public GameObject cylinderObject;
    [SerializeField] private float rotateForce;
    [SerializeField] private string direction;

    public void OnPointerDown(PointerEventData eventData)   // ilgili butona basılma süresi basılmasını algılamayı sağlar
    {                                                         // tıklamaları sürekli almak için 
        buttonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }




    void Update()
    {
        if (buttonPressed)
        {
            if (direction == "Left")
            {
                cylinderObject.transform.Rotate(0, rotateForce * Time.deltaTime, 0, Space.Self);
            }

            else
            {
                cylinderObject.transform.Rotate(0, -rotateForce * Time.deltaTime, 0, Space.Self);
            }
        }


    }
}
