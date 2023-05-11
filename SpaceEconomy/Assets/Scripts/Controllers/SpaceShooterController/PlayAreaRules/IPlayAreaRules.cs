using UnityEngine;

namespace SpaceShooter.Scripts.PlayerController
{
    public interface IPlayAreaRules
    {
        public Vector3 enforceRules(IBorderMax borderMax, Vector3 position);
    }
}