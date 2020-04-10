using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    Vector3 baitDirection;
    Bait bait;
    bool isGrounded;
    bool canEatBait;
    Animator animator;
    Transform fishBone;

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
        animator = GetComponent<Animator>();
        fishBone = transform.GetChild(0).gameObject.transform;
    }

    public void MoveUp(bool inputUp)
    {
        Vector3 fishAngle = fishBone.localEulerAngles;
        SetBait(baitObject);
        if (isGrounded)
        {
            isGrounded = false;
        }
        if (inputUp)
        {
            fishBone.localEulerAngles = new Vector3(-15f, fishAngle.y, fishAngle.z);
            transform.position += baitDirection * moveStepUp * Time.deltaTime;
        }
    }

    public void MoveDown(bool inputDown)
    {
        Vector3 fishAngle = fishBone.localEulerAngles;
        if (inputDown && !isGrounded)
        {
            fishBone.localEulerAngles = new Vector3(15f, fishAngle.y, fishAngle.z);
            transform.position += (underWaterGround.position - transform.position) * moveStepDown * Time.deltaTime;
        }
    }

    public void RotationXIdle(bool inputUp, bool inputDown)
    {
        Vector3 fishAngle = fishBone.localEulerAngles;
        if (!inputUp && !inputDown)
        {
            fishBone.localEulerAngles = new Vector3(0.0f, fishAngle.y, fishAngle.z);
        }
    }

    public void MoveSide(float inputSide)
    {
        transform.position += new Vector3(inputSide * moveStepSide * Time.deltaTime, 0);
        if (inputSide > 0.0f)
        {
            fishBone.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        }
        else if (inputSide < 0.0f)
        {
            fishBone.localEulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        }
    }

    public void EatBait(bool inputSpace)
    {
        if (inputSpace && canEatBait)
        {
            bait.UpdateHP();
        }
    }

    public void EatBait1(bool input1)
    {
        if (input1)
        {
            bait.BaitEaten(1);
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
