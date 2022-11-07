using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Basket"))
        {
            Renderer color = GetComponent<Renderer>();
            GameManager.instance.ParticleEffect(gameObject.transform.position, color.material.color); // topun rengine erişip particle effectde kullandık
            TechnicalProcess();
            GameManager.instance.InsideBall();
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            Renderer color = GetComponent<Renderer>();
            GameManager.instance.ParticleEffect(gameObject.transform.position, color.material.color);
            TechnicalProcess();
            GameManager.instance.OutsideBall();
        }
    }

    void TechnicalProcess()
    {
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        rb.velocity = Vector3.zero;// pool loop 1 kere dönünce topların hız düzenini sağlamak için  (AddForce dan dolayı)
        rb.angularVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
