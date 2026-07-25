using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]

public class ShipMovement : MonoBehaviour
{
    [Header("Ship movement settings")]
    [SerializeField] private float pitchAngle = 10f;
    [SerializeField] private float forwardVelocity = 1f;
    [SerializeField] private float upwardsVelocity = 0.5f;
    [SerializeField] private float yawVelocity = 10f;
    [SerializeField] private PIDController.PIDController rollPIDController;
    [SerializeField] private PIDController.PIDController pitchPIDController;
    [SerializeField] private PIDController.PIDController yawPIDController;
    [SerializeField] private PIDController.PIDController thrustPIDController;
    [SerializeField] private PIDController.PIDController driftPIDController;
    [SerializeField] private PIDController.PIDController verticalPIDController;
    [SerializeField] private Vector3 projectedVelocity;

    //Components
    private Rigidbody rigidbody;

    //Inputs
    private float thrustInput;
    private float verticalInput;
    private float yawInput;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandleRotation();
        HandleMovement();
    }
    
    private void HandleRotation()
    {
        //Roll correction
        float rollCorrection = rollPIDController.UpdateAngle(Time.fixedDeltaTime, transform.eulerAngles.z, 0);
        rigidbody.AddRelativeTorque(Vector3.forward * rollCorrection * Time.fixedDeltaTime);

        //Pitch correction
        float pitchCorrection = pitchPIDController.UpdateAngle(Time.fixedDeltaTime, transform.eulerAngles.x, -verticalInput * pitchAngle);
        rigidbody.AddRelativeTorque(Vector3.right * pitchCorrection * Time.fixedDeltaTime);

        //Yaw input and correction
        float yawCorrection = yawPIDController.Update(Time.fixedDeltaTime, rigidbody.angularVelocity.y, yawInput * yawVelocity);
        rigidbody.AddRelativeTorque(Vector3.up * yawCorrection * Time.fixedDeltaTime);
    }
    private void HandleMovement()
    {
        projectedVelocity = transform.InverseTransformDirection(rigidbody.linearVelocity);

        float thrustCorrection = thrustPIDController.Update(Time.fixedDeltaTime, projectedVelocity.z, thrustInput * forwardVelocity);
        rigidbody.AddRelativeForce(Vector3.forward * thrustCorrection * Time.fixedDeltaTime);

        float driftCorrection = driftPIDController.Update(Time.fixedDeltaTime, projectedVelocity.x, 0);
        rigidbody.AddRelativeForce(Vector3.right * driftCorrection * Time.fixedDeltaTime);

        float verticalCorrection = verticalPIDController.Update(Time.fixedDeltaTime, projectedVelocity.y, verticalInput * upwardsVelocity);
        rigidbody.AddForce(Vector3.up * verticalCorrection * Time.fixedDeltaTime);
    }


    #region Input Methods
    public void OnThrust(InputAction.CallbackContext context)
    {
        thrustInput = context.ReadValue<float>();
    }
    public void OnVertical(InputAction.CallbackContext context)
    {
        verticalInput = context.ReadValue<float>();
    }
    public void OnYaw(InputAction.CallbackContext context)
    {
        yawInput = context.ReadValue<float>();
    }
    #endregion
}