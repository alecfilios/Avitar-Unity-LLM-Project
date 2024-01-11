using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeartRateEventBase : MonoBehaviour
{

    protected Transform Player_T;
    private void Start()
    {
        Player_T = GameObject.Find("Avatar").transform;

        float x = UnityEngine.Random.Range(-2f, 2f);
        float y = UnityEngine.Random.Range(2f, 3f);
        float z = UnityEngine.Random.Range(-2f, 2f);

        transform.position = new Vector3(x, y, z);

    }
    private void Update()
    {
        StartMovement();
    }

    public abstract void StartMovement();
}
