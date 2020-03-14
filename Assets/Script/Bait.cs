using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class Bait : MonoBehaviour
{
    public float pullStep;
    public float dropStep;
    public int maxHP;
    public int currentTotalHP;
    public float boarderY = 3.0f;
    public GameObject BaitHPText;

    int[] baitEatenHP = new int[3];
    bool[] baitAlive = Enumerable.Repeat<bool>(true, 3).ToArray();
    int currentBaitHP;

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
        currentTotalHP = maxHP;
        baitEatenHP[0] = maxHP * 2 / 3;
        baitEatenHP[1] = maxHP / 3;
        baitEatenHP[2] = 0;

        currentBaitHP = maxHP / 3;
        BaitHPText.GetComponent<TextMeshPro>().text = currentBaitHP.ToString();
    }

    private void Update()
    {
        // Destroy Bait if HP is ZERO
        //if (HP == 0)
        //{
        //    Destroy(gameObject);
        //}

        if (transform.position.y <= boarderY)
        {
            state = STATE.STOP;
        }

        switch (state)
        {
            case STATE.STOP:
                break;
            case STATE.PULL:
                transform.Translate(0, 0, pullStep);
                break;
            case STATE.DROP:
                transform.Translate(0, 0, -dropStep);
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
        currentTotalHP--;
        currentBaitHP--;
        Debug.Log("HP:" + currentTotalHP);

        // BaitHPText
        SetBaitHPText();

        // EatBait Object
        for (int id = 0; id < baitEatenHP.Length; id++)
        {
            if (baitAlive[id] && currentTotalHP <= baitEatenHP[id])
            {
                BaitEaten(id + 1);
                baitAlive[id] = false;
            }
        }
    }

    public void BaitEaten(int id)
    {
        GameObject bait = transform.GetChild(id).gameObject;
        Debug.Log(bait);
        bait.SetActive(false);
    }

    // Setting Text
    public void SetBaitHPText()
    {
        if (currentTotalHP <= 0)
        {
            BaitHPText.GetComponent<TextMeshPro>().text = "";
        }
        else
        {
            if (currentBaitHP <= 0)
            {
                currentBaitHP = maxHP / 3; 
            }
            BaitHPText.GetComponent<TextMeshPro>().text = currentBaitHP.ToString();
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

}
