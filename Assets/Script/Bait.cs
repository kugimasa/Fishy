using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : MonoBehaviour
{
    public float pullStep;
    public float dropStep;
    public int HP;
    public float boarderY = 3.0f;

    public enum STATE
    {
        STOP,
        DROP,
        PULL
    }

    [SerializeField] STATE state;

    private void Start()
    {
        state = STATE.STOP;
    }

    private void Update()
    {
        // Destroy Bait if HP is ZERO
        if (HP == 0)
        {
            Destroy(gameObject);
        }

        if (transform.position.y <= boarderY)
        {
            state = STATE.STOP;
        }

        switch (state)
        {
            case STATE.STOP:
                break;
            case STATE.PULL:
                transform.Translate(0, pullStep, 0);
                break;
            case STATE.DROP:
                transform.Translate(0, -dropStep, 0);
                break;
            default:
                break;

        }
    }

    public void SetState(STATE s)
    {
        state = s;
    }

    public void UpdateHP()
    {
        HP--;
        Debug.Log("HP:" + HP);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

}
