using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Basket"))
        {
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            GetComponent<Rigidbody>().velocity = Vector3.zero;    // pool loop 1 kere dönünce topların hız düzenini sağlamak için  (AddForce dan dolayı)
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}
