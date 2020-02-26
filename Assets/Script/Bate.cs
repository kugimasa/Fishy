using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bate : MonoBehaviour
{
    float dropStep;
    public enum STATE
    {
        STOP,
        DROP,
        PULL
    }

    public STATE state;

    private void Start()
    {
        state = STATE.STOP;
    }

    private void Update()
    {
        switch (state)
        {
            case STATE.STOP:
                break;
            case STATE.PULL:
                transform.Translate(0, -dropStep*2, 0);
                break;
            case STATE.DROP:
                transform.Translate(0, dropStep, 0);
                break;
            default:
                break;

        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

}
