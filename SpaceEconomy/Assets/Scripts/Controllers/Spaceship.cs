using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spaceship : MonoBehaviour
{
    [Header("=== Ship Movement Settings ===")]
    [SerializeField]
    private bool isFlightAssistOn = true;
    [SerializeField]
    private float yawTorque = 250f;

    [SerializeField]
    private float pitchTorque = 200f;

    [SerializeField]
    private float rollTorque = 200f;

    [SerializeField]
    private float thrust = 100f;

    [SerializeField]
    private float upThrust = 50f;

    [SerializeField]
    private float strafeThrust = 50f;

    [SerializeField, Range(0.001f, 0.999f)]
    private float thrustGlideReduction = 0.5f;

    [SerializeField, Range(0.001f, 0.999f)]
    private float upDownGlideReduction = 0.111f;

    [SerializeField, Range(0.001f, 0.999f)]
    private float leftRightGlideReduction = 0.111f;

    [SerializeField, Range(0.001f, 0.999f)]
    float glide,
        horizontalGlide,
        verticalGlide = 0f;

    Rigidbody rigidbody;

    // Input values
    private float thrust1D,
        strafe1D,
        upDown1D,
        roll1D;
    private Vector2 pitchYaw;
    private int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        StopWhenNoInput();
        // Roll
        rigidbody.AddRelativeTorque(Vector3.back * roll1D * rollTorque * Time.deltaTime);

        // Pitch
        rigidbody.AddRelativeTorque(
            Vector3.right * Mathf.Clamp(-pitchYaw.y, -1f, 1f) * pitchTorque * Time.deltaTime
        );

        // Yaw
        rigidbody.AddRelativeTorque(
            Vector3.up * Mathf.Clamp(pitchYaw.x, -1, 1f) * yawTorque * Time.deltaTime
        );

        // Thrust
        if (thrust1D != 0)
        {
            float currentThrust = thrust;

            // Opdater retningen baseret på thrust1D
            direction = thrust1D > 0 ? 1 : -1;

            // Anvend accelerationen
            rigidbody.AddRelativeForce(Vector3.forward * thrust1D * currentThrust * Time.deltaTime);

            // Opdater glide-værdien
            glide = Mathf.Abs(thrust1D * currentThrust);
        }
        else
        {
            // Hvis der ikke gives noget input, falder glide-værdien gradvist til nul
            glide = Mathf.Max(0, glide - thrust * Time.deltaTime);

            // Anvend den aktuelle glideværdi
            rigidbody.AddRelativeForce(Vector3.forward * glide * direction * Time.deltaTime);
        }

        // Up down
        if (upDown1D > 0.1f || upDown1D < -0.1f)
        {
            rigidbody.AddRelativeForce(Vector3.up * upDown1D * upThrust * Time.fixedDeltaTime);
            verticalGlide = upDown1D * upThrust;
        }
        else
        {
            rigidbody.AddRelativeForce(Vector3.up * verticalGlide * Time.fixedDeltaTime);
            verticalGlide *= upDownGlideReduction;
        }

        // Strafing
        if (strafe1D > 0.1f || strafe1D < -0.1f)
        {
            rigidbody.AddRelativeForce(Vector3.right * strafe1D * upThrust * Time.fixedDeltaTime);
            horizontalGlide = strafe1D * strafeThrust;
        }
        else
        {
            rigidbody.AddRelativeForce(Vector3.right * strafe1D * Time.fixedDeltaTime);
            horizontalGlide *= leftRightGlideReduction;
        }
    }

    #region Input methods
    public void OnThrust(InputAction.CallbackContext context)
    {
        thrust1D = context.ReadValue<float>();
    }

    public void OnStrafe(InputAction.CallbackContext context)
    {
        strafe1D = context.ReadValue<float>();
    }

    public void OnUpDown(InputAction.CallbackContext context)
    {
        upDown1D = context.ReadValue<float>();
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        roll1D = context.ReadValue<float>();
    }

    public void OnPitchYaw(InputAction.CallbackContext context)
    {
        // pitchYaw = context.ReadValue<Vector2>();
    }
    #endregion

    private void StopWhenNoInput()
    {
        Debug.Log(upDown1D);
        // Up down
        if (Mathf.Abs(upDown1D) > 0f)
        {
            rigidbody.AddRelativeForce(Vector3.up * upDown1D * upThrust * Time.fixedDeltaTime);
            verticalGlide = Mathf.Abs(upDown1D * upThrust);
        }
        else
        {
            verticalGlide = Mathf.Max(0, verticalGlide - upThrust * Time.deltaTime);
            rigidbody.AddRelativeForce(
                Vector3.up * verticalGlide * Mathf.Sign(upDown1D) * Time.fixedDeltaTime
            );
        }

        // Strafing
        if (Mathf.Abs(strafe1D) > 0f)
        {
            rigidbody.AddRelativeForce(
                Vector3.right * strafe1D * strafeThrust * Time.fixedDeltaTime
            );
            horizontalGlide = Mathf.Abs(strafe1D * strafeThrust);
        }
        else
        {
            horizontalGlide = Mathf.Max(0, horizontalGlide - strafeThrust * Time.deltaTime);
            rigidbody.AddRelativeForce(
                Vector3.right * horizontalGlide * Mathf.Sign(strafe1D) * Time.fixedDeltaTime
            );
        }
    }
}
