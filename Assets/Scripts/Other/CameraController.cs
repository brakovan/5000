using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform background;

    public Transform target;

    private Vector2 minPos, maxPos;
    private Camera mainCamera;

    [SerializeField]
    private float cameraSpeed = 5f;

    private void Awake()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("CameraController: Main camera not found.");
            return;
        }
        CalculateBounds();
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position;
        desiredPosition.z = transform.position.z;

        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minPos.x, maxPos.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minPos.y, maxPos.y);

        transform.position = Vector3.Lerp(transform.position, desiredPosition, cameraSpeed);
    }

    private void CalculateBounds()
    {
        if (background != null)
        {
            SpriteRenderer spriteRenderer = background.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                float vertExtent = mainCamera.orthographicSize;
                float horzExtent = vertExtent * Screen.width / Screen.height;

                minPos.x = spriteRenderer.bounds.min.x + horzExtent;
                minPos.y = spriteRenderer.bounds.min.y + vertExtent;
                maxPos.x = spriteRenderer.bounds.max.x - horzExtent;
                maxPos.y = spriteRenderer.bounds.max.y - vertExtent;
            }
            else
            {
                Debug.LogWarning("CameraController: Background does not have a SpriteRenderer.");
            }
        }
        else
        {
            Debug.LogWarning("CameraController: No background object set.");
        }
    }
}

