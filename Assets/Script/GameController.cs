using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject fishObject;
    public GameObject baitObject;
    Fish fish;
    Bait bait;

    void Start()
    {
        fish = fishObject.GetComponent<Fish>();
        bait = baitObject.GetComponent<Bait>();
    }

    void Update()
    {
        fish.MoveUp(Input.GetKey("up"));
        fish.MoveDown(Input.GetKey("down"));
        fish.MoveSide(Input.GetAxis("Horizontal"));
        fish.EatBait(Input.GetKeyUp("space"));
    }
}
