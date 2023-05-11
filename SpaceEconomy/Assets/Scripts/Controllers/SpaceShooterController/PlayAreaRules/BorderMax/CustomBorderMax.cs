namespace SpaceShooter.Scripts.PlayerController
{
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
}