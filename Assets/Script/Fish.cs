using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    Vector3 baitDirection;
    Bait bait;
    bool isGrounded;
    bool canEatBait;

    public float moveStepUp = 1.0f;
    public float moveStepDown = 1.0f;
    public float moveStepSide = 1.0f;
    public Transform underWaterGround;
    public GameObject baitObject;

    void Start()
    {
        isGrounded = false;
        canEatBait = false;
        bait = baitObject.GetComponent<Bait>();
    }

    public void MoveUp(bool inputUp)
    {
        SetBait(baitObject);
        if (isGrounded)
        {
            isGrounded = false;
        }
        if (inputUp)
        {
            transform.position += baitDirection * moveStepUp * Time.deltaTime;
        }
    }

    public void MoveDown(bool inputDown)
    {
        if (inputDown && !isGrounded)
        {
            transform.position += (underWaterGround.position - transform.position) * moveStepDown * Time.deltaTime;
        }
    }

    public void MoveSide(float inputSide)
    {
        transform.position += new Vector3(inputSide * moveStepSide * Time.deltaTime, 0);
    }

    public void EatBait(bool inputSpace)
    {
        if (inputSpace && canEatBait)
        {
            bait.UpdateHP();
        }
    }

    private void SetBait(GameObject baitObj)
    {

        baitDirection = baitObj.transform.position - transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bait")
        {
            canEatBait = true;
            bait.SetState(Bait.STATE.STOP);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Bait")
        {
            canEatBait = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "UnderWaterOrigin")
        {
            isGrounded = true;
        }
    }
}
