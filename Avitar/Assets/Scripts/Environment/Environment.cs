using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public Material daySkybox;
    public Material nightSkybox;
    public Light directionalLight;

    public Color dayColor = new Color(1f, 0.6f, 0.25f);

    public Color nightColor = new Color(0.25f, 0.25f, 1f);

    // Start is called before the first frame update
    void Awake()
    {
        CheckTimeAndAssignEnvironment();
    }

    void CheckTimeAndAssignEnvironment()
    {
        DateTime currentTime = DateTime.Now;
        int currentHour = currentTime.Hour;
        if (true)//if (currentHour >= 6 && currentHour < 18) // Assuming day is from 6 AM to 6 PM
        {
            RenderSettings.skybox = daySkybox;
            RenderSettings.fogColor = dayColor;
            directionalLight.color = dayColor;
        }
        else
        {
            RenderSettings.skybox = nightSkybox;
            RenderSettings.fogColor = nightColor;
            directionalLight.color = nightColor;
        }
    }
}
