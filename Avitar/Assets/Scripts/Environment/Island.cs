using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    [SerializeField]
    Transform AvatarTarget;
    [SerializeField]
    Transform CameraTarget;

    [SerializeField]
    AnimationClip clip;

    public Transform GetAvatarTarget_T()
    {
        return AvatarTarget;
        ;
    }
    public Transform GetCameraTarget_T()
    {
        return CameraTarget;
    }

    public AnimationClip GetClip()
    {
        return clip;
    }
}
