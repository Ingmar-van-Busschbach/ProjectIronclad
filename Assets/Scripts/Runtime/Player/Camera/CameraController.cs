using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CinemachineCamera))]
[RequireComponent(typeof(CinemachineOrbitalFollow))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float minDistance = 3f;
    [SerializeField] private float maxDistance = 15f;

    private CinemachineCamera cam;
    private CinemachineOrbitalFollow orbitalFollow;

    private float scrollDelta;

    private void Start()
    {
        cam = GetComponent<CinemachineCamera>();
        orbitalFollow = cam.GetComponent<CinemachineOrbitalFollow>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        HandleZoom();
    }

    private void HandleZoom()
    {
        orbitalFollow.Radius = Mathf.Clamp(orbitalFollow.Radius - scrollDelta * zoomSpeed, minDistance, maxDistance);
    }

    public void OnMouseScroll(InputAction.CallbackContext context)
    {
        scrollDelta = context.ReadValue<float>();
    }
}
