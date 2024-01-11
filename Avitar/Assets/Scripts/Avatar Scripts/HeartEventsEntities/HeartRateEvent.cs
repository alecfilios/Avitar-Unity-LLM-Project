using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartRateEvent : MonoBehaviour
{
    
    private Transform Player_T;
    public float OrbitSpeed;
    public float FloatingSpeed;
    public float Amplitude; // Adjust the amplitude of the sine wave

    private float randomOffset;

    private void Awake()
    {
        Player_T = GameObject.Find("Avatar").transform;
    }

    private void Start()
    {
        // TODO: Fix those positions
        float x = UnityEngine.Random.Range(-2f, 2f);
        float y = UnityEngine.Random.Range(1f, 2f);
        float z = UnityEngine.Random.Range(-2f, 2f);


        transform.position = new Vector3(x, y, z);

        // Initialize a random offset for each object
        randomOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    private void Update()
    {
        StartMovement();
    }

    public void StartMovement()
    {
        // Calculate the y-coordinate using a sine function with a random offset
        float yPos = Player_T.position.y + Amplitude * Mathf.Sin(FloatingSpeed * Time.time + randomOffset);

        // Update the position of the object
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);

        // Rotate around the player
        transform.RotateAround(Player_T.position, Vector3.up, OrbitSpeed * Time.deltaTime);
    }

}
