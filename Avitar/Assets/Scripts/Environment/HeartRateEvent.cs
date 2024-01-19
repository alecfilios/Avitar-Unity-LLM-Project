using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartRateEvent : MonoBehaviour
{

    private Transform Player_T;
    public float OrbitSpeed;
    public float FloatingSpeed;
    public float Amplitude; // Adjust the amplitude of the sine wave

    private Vector3 _initPosition;

    private float randomAngle;

    private void Awake()
    {
        Player_T = GameObject.Find("Avatar").transform;
    }

    private void Start()
    {
        float x = GenerateRandomPosition(1, 2);
        float y = GenerateRandomPosition(1, 2);
        float z = GenerateRandomPosition(1, 2);

        _initPosition = new Vector3(x, y, z);

        transform.localPosition = _initPosition;

        // Initialize a random offset for each object
        randomAngle = Random.Range(0f, 2f * Mathf.PI);
    }

    private void OnEnable()
    {
        transform.localPosition = _initPosition;
    }

    private void Update()
    {
        StartMovement();
    }

    public void StartMovement()
    {
        // Calculate the y-coordinate using a sine function with a random offset
        float yPos = Amplitude * Mathf.Sin(FloatingSpeed * Time.time + randomAngle) + _initPosition.y;

        // Update the position of the object
        transform.localPosition = new Vector3(transform.localPosition.x, yPos, transform.localPosition.z);

        // Rotate around the player
        transform.RotateAround(Player_T.position, Vector3.up, OrbitSpeed * Time.deltaTime);
    }

    float GenerateRandomPosition(float min, float max)
    {
        float pos;
        // Generate a random number between -1 and -2 or between 1 and 2
        if (Random.Range(0, 2) == 0)
        {
            pos = Random.Range(-max, -min);
        }
        else
        {
            pos = Random.Range(min, max);
        }
        return pos;
    }

}
