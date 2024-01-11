using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighHeartRateEvent : HeartRateEventBase
{
    public float OrbitSpeed;
    public float FloatingSpeed;
    public float Amplitude; // Adjust the amplitude of the sine wave

    private float randomOffset;
    private void Awake()
    {
        // Initialize a random offset for each object
        randomOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    public override void StartMovement()
    {
        // Calculate the y-coordinate using a sine function with a random offset
        float yPos = transform.position.y + Amplitude * Mathf.Sin(FloatingSpeed * Time.time + randomOffset);

        // Update the position of the object
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);

        // Rotate around the player
        transform.RotateAround(Player_T.position, Vector3.up, OrbitSpeed * Time.deltaTime);
    }
}
