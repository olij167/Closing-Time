using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ConveyorBelt : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    public Vector3 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().position -= direction * speed * Time.deltaTime;
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + direction * speed * Time.deltaTime);
    }
    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.GetComponent<Rigidbody>() != null)
    //    {
    //        collision.gameObject.GetComponent<Rigidbody>().velocity = direction;
    //    }
    //}
}
