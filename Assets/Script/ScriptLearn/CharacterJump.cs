using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    public float x;
    public float y;
    public bool Clamp = false;
    public bool Once = false;
    public bool PingPong = false;
    public bool Count = false;
    public int CountTimes = 1;

    public void StartJumping()
    {
        if (Clamp)
            transform.LeanMoveLocal(new Vector2(x, y), 0.5f).setEaseOutQuart().setLoopClamp();

        else if (Once)
            transform.LeanMoveLocal(new Vector2(x, y), 0.5f).setEaseOutQuart().setLoopOnce();

        else if (PingPong)
            transform.LeanMoveLocal(new Vector2(x, y), 0.5f).setEaseOutQuart().setLoopPingPong();

        else if (Count)
            transform.LeanMoveLocal(new Vector2(x, y), 0.5f).setEaseOutQuart().setLoopCount(CountTimes);


    }
}