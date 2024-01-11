using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Avatar Avatar;

    public float smoothSpeed = 4f; // Smoothing factor for camera movement
    public float rotationSpeed = 2f; // Smoothing factor for camera rotation

    public bool isMoving = false;

    public void MoveToTargetByLocation(Island island)
    {
        if (isMoving)
        {
            Debug.LogWarning("Cannot move, camera is already in motion.");
        }
        else
        {
            // Smoothly move the camera towards the target position and rotation
            StartCoroutine(MoveCamera(island));
        }
    }

    IEnumerator MoveCamera(Island island)
    {
        isMoving = true;

        Vector3 targetPosition = island.GetCameraTarget_T().position;
        Quaternion targetRotation = island.GetCameraTarget_T().rotation;


        while (Vector3.Distance(transform.position, targetPosition) > 0.1f ||
               Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        // Ensure the camera reaches the exact target position and rotation
        transform.position = targetPosition;
        transform.rotation = targetRotation;

        isMoving = false;

        // Teleport the Avatar as well
        Avatar.Teleport(island);
    }
}
