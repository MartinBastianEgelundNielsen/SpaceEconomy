using UnityEngine;

namespace SpaceShooter.Scripts.PlayerController
{
    public class TopAndButtonHasMaxLeftAndRightWrapsAround : IPlayAreaRules
    {
        public Vector3 enforceRules(IBorderMax borderMax, Vector3 position)
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
}