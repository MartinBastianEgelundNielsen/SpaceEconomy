using UnityEngine;

namespace SpaceShooter.Scripts.PlayerController
{
    public class PlayfieldSizeHasAMax : IPlayAreaRules
    {
        public Vector3 enforceRules(IBorderMax borderMax, Vector3 position)
        {
            float x = Mathf.Clamp(position.x, borderMax.Left, borderMax.Right);
            float y = Mathf.Clamp(position.y, borderMax.Bottom, borderMax.Top);

            return new Vector3(x, y, position.z);
        }
    }
}