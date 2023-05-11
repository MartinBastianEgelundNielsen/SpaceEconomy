using NUnit.Framework;
using UnityEngine;
using SpaceShooter.Scripts.PlayerController;

[TestFixture]
public class PlayfieldSizeHasAMaxTests
{
    // Instance of the class under test
    private PlayfieldSizeHasAMax playfieldSizeHasAMax;

    // Instance of border limits
    private IBorderMax borderMax;

    [SetUp]
    public void Setup()
    {
        // Initialize the PlayfieldSizeHasAMax instance
        playfieldSizeHasAMax = new PlayfieldSizeHasAMax();

        // Initialize the border limits
        borderMax = new CustomBorderMax(2f, -2f, -2f, 2f);
    }

    [Test]
    public void EnforceRules_InsideBorder_StaysInSamePosition()
    {
        // Arrange
        Vector3 currentPosition = new Vector3(1f, 1f, 0);

        // Act
        Vector3 newPosition = playfieldSizeHasAMax.enforceRules(borderMax, currentPosition);

        // Assert
        Assert.AreEqual(currentPosition, newPosition);
    }
}
