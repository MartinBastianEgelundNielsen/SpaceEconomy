using UnityEngine;
using SpaceShooter.Scripts.PlayerController;

public class SpaceShooterControler : MonoBehaviour
{
    private float HorizontalInput { get { return Input.GetAxis("Horizontal"); } }

    private float VerticalInput { get { return Input.GetAxis("Vertical"); } }

    private IBorderMax borderMax;

    [Header("Border Values - Set before start")]
    [SerializeField] private float top = 2f;
    [SerializeField] private float bottom = -2f;
    [SerializeField] private float left = -2f;
    [SerializeField] private float right = 2f;

    [Header("Speed values - Set before start")]
    [SerializeField]
    private float _speed = 3.5f;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        borderMax = new CustomBorderMax(top, bottom, left, right);
    }

    void Update()
    {
        float horizontalInput = HorizontalInput;
        float verticalInput = VerticalInput;

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        Vector3 newPosition = PlayAreaRulesManager.Instance.CurrentRule.enforceRules(borderMax, transform.position);
        transform.position = newPosition;
    }
}