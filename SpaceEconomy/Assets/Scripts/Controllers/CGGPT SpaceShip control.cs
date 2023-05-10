using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceshipController : MonoBehaviour
{
    public InputActionAsset actionAsset;
    
    [SerializeField] private float maxAcceleration = 10f;

    // Input Actions
    private InputAction thrustAction;
    private InputAction strafeAction;
    private InputAction upDownAction;
    private InputAction rollAction;
    private InputAction pitchYawAction;

    // Input values
    private float thrust1D;
    private float strafe1D;
    private float upDown1D;
    private float roll1D;
    private Vector2 pitchYaw;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Initialize your actions
        thrustAction = actionAsset.FindAction("Thrust");
        strafeAction = actionAsset.FindAction("Strafe");
        upDownAction = actionAsset.FindAction("UpDown");
        rollAction = actionAsset.FindAction("Roll");
        pitchYawAction = actionAsset.FindAction("PitchYaw");

        // Register callbacks for your actions
        thrustAction.performed += ctx => thrust1D = ctx.ReadValue<float>();
        thrustAction.canceled += ctx => thrust1D = 0;
        strafeAction.performed += ctx => strafe1D = ctx.ReadValue<float>();
        strafeAction.canceled += ctx => strafe1D = 0;
        upDownAction.performed += ctx => upDown1D = ctx.ReadValue<float>();
        upDownAction.canceled += ctx => upDown1D = 0;
        rollAction.performed += ctx => roll1D = ctx.ReadValue<float>();
        rollAction.canceled += ctx => roll1D = 0;
        pitchYawAction.performed += ctx => pitchYaw = ctx.ReadValue<Vector2>();
        pitchYawAction.canceled += ctx => pitchYaw = Vector2.zero;
    }

    void OnEnable()
    {
        // Enable your actions
        thrustAction.Enable();
        strafeAction.Enable();
        upDownAction.Enable();
        rollAction.Enable();
        pitchYawAction.Enable();
    }

    void OnDisable()
    {
        // Disable your actions
        thrustAction.Disable();
        strafeAction.Disable();
        upDownAction.Disable();
        rollAction.Disable();
        pitchYawAction.Disable();
    }

    void FixedUpdate()
    {
        // Apply forces based on input values
        Vector3 thrustForce = transform.forward * thrust1D * maxAcceleration;
        Vector3 strafeForce = transform.right * strafe1D * maxAcceleration;
        Vector3 upDownForce = transform.up * upDown1D * maxAcceleration;

        // Add the forces to the Rigidbody
        rb.AddForce(thrustForce);
        rb.AddForce(strafeForce);
        rb.AddForce(upDownForce);

        // Apply rotation based on input values
        Vector3 rotation = new Vector3(-pitchYaw.y, pitchYaw.x, -roll1D);
        rb.AddTorque(rotation);
    }
}
