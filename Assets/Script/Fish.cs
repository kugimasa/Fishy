using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    Vector3 bateDirection;
    bool isGrounded;

    public float moveStep = 1.0f;
    public Transform underWaterGround;
    public GameObject Bate;

    void Start()
    {
        isGrounded = false;
    }

    void Update()
    {
        Move();
    }



    void Move()
    {
        // Move towards bate
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("Bate");
            SetBate(Bate);
            transform.position += bateDirection * moveStep * Time.deltaTime;
        }

        // Move towards ground
        if (Input.GetKey(KeyCode.DownArrow) && !isGrounded)
        {
            Debug.Log("Return");
            transform.position += (underWaterGround.position - transform.position) * moveStep * Time.deltaTime;
        }

        // Move Horizontaly
        transform.position += new Vector3(Input.GetAxis("Horizontal") * moveStep, 0);
    }

    private void SetBate(GameObject bate)
    {

        bateDirection = bate.transform.position - transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        bateDirection = other.gameObject.transform.position - transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "UnderWaterOrigin")
        {
            Debug.Log("UnderWaterOrigin");
            isGrounded = true;
        }
    }
}
