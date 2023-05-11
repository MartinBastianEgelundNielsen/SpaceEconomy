namespace SpaceShooter.Scripts.PlayerController
{
    /// <summary>
    /// The PlayAreaRulesManager is a Singleton class that manages the current rule set for the play area.
    /// The rule set determines how game objects should behave when they move outside the play area.
    /// </summary>
    internal class PlayAreaRulesManager
    {


        // The singleton instance
        internal static PlayAreaRulesManager _instance;

        /// <summary>
        /// Singleton instance of the PlayAreaRulesManager.
        /// This is the global point of access to the PlayAreaRulesManager instance.
        /// 
        /// Example use:
        /// void OnButtonPress()
        ///  PlayAreaRulesManager.Instance.ChangeRule(new PlayfieldSizeHasAMax());
        /// </summary>
        internal static PlayAreaRulesManager Instance
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
        public IPlayAreaRules _currentRule;

        /// <summary>
        /// The current rule set for the play area.
        /// </summary>
        internal IPlayAreaRules CurrentRule
        {
            get { return _currentRule; }
            private set { _currentRule = value; }
        }

        /// <summary>
        /// Private constructor to enforce singleton pattern.
        /// Sets the initial rule set.
        /// </summary>
        internal PlayAreaRulesManager()
        {
            // Set the initial rule set
            _currentRule = new TopAndButtonHasMaxLeftAndRightWrapsAround();
        }

        /// <summary>
        /// Changes the current rule set.
        /// </summary>
        /// <param name="newRule">The new rule set.</param>
        internal void ChangeRule(IPlayAreaRules newRule)
        {
            _currentRule = newRule;
        }
    }
}