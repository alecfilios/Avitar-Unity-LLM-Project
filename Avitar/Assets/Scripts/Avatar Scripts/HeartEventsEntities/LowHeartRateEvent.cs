using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowHeartRateEvent : HeartRateEventBase
{
    public float OrbitSpeed;


    public override void StartMovement()
    {
        // planet to travel along a path that rotates around the sun
        transform.RotateAround(Player_T.position, Vector3.up, OrbitSpeed * Time.deltaTime);
    }
}
