using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShooterControler : MonoBehaviour
{
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

    public interface IBorderMax
    {
        float Top { get; }
        float Bottom { get; }
        float Left { get; }
        float Right { get; }
    }

    public class CustomBorderMax : IBorderMax
    {
        public float Top { get; }
        public float Bottom { get; }
        public float Left { get; }
        public float Right { get; }

        public CustomBorderMax(float top, float bottom, float left, float right)
        {
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
        }
    }

    private float HorizontalInput { get { return Input.GetAxis("Horizontal"); } }

    private float VerticalInput { get { return Input.GetAxis("Vertical"); } }
}


public interface IPlayAreaRules
{
    Vector3 enforceRules(SpaceShooterControler.IBorderMax borderMax, Vector3 position);
}

public class PlayfieldSizeHasAMax : IPlayAreaRules
{
    public Vector3 enforceRules(SpaceShooterControler.IBorderMax borderMax, Vector3 position)
    {
        float x = Mathf.Clamp(position.x, borderMax.Left, borderMax.Right);
        float y = Mathf.Clamp(position.y, borderMax.Bottom, borderMax.Top);

        return new Vector3(x, y, position.z);
    }
}

public class TopAndButtonHasMaxLeftAndRightWrapsAround : IPlayAreaRules
{
    public Vector3 enforceRules(SpaceShooterControler.IBorderMax borderMax, Vector3 position)
    {
        float y = Mathf.Clamp(position.y, borderMax.Bottom, borderMax.Top);
        float x = position.x;

        if (x >= borderMax.Right)
            x = borderMax.Left;
        else if (x <= borderMax.Left)
            x = borderMax.Right;

        return new Vector3(x, y, position.z);
    }
}



/// <summary>
/// The PlayAreaRulesManager is a Singleton class that manages the current rule set for the play area.
/// The rule set determines how game objects should behave when they move outside the play area.
/// </summary>
public class PlayAreaRulesManager
{


    // The singleton instance
    private static PlayAreaRulesManager _instance;

    /// <summary>
    /// Singleton instance of the PlayAreaRulesManager.
    /// This is the global point of access to the PlayAreaRulesManager instance.
    /// 
    /// Example use:
    /// void OnButtonPress()
    ///  PlayAreaRulesManager.Instance.ChangeRule(new PlayfieldSizeHasAMax());
    /// </summary>
    public static PlayAreaRulesManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PlayAreaRulesManager();
            }
            return _instance;
        }
    }

    // The current rule set
    private IPlayAreaRules _currentRule;

    /// <summary>
    /// The current rule set for the play area.
    /// </summary>
    public IPlayAreaRules CurrentRule
    {
        get { return _currentRule; }
        private set { _currentRule = value; }
    }

    /// <summary>
    /// Private constructor to enforce singleton pattern.
    /// Sets the initial rule set.
    /// </summary>
    private PlayAreaRulesManager()
    {
        // Set the initial rule set
        _currentRule = new TopAndButtonHasMaxLeftAndRightWrapsAround();
    }

    /// <summary>
    /// Changes the current rule set.
    /// </summary>
    /// <param name="newRule">The new rule set.</param>
    public void ChangeRule(IPlayAreaRules newRule)
    {
        _currentRule = newRule;
    }
}

