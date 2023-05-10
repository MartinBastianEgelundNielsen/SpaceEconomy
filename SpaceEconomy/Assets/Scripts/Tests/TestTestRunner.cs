using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestTestRunner
{
    // A Test behaves as an ordinary method
    [Test]
    public void verifyThatTestRunnerWorks_shouldPass()
    {
        // Arrange: set up your objects and expected results
        var trueBool = true;
        var expectedResult = true;

        // Assert: check that the result is what you expected
        Assert.AreEqual(expectedResult, trueBool);
    }

    // A Test behaves as an ordinary method
    [Test]
    public void verifyThatTestRunnerWorks_shouldFail()
    {
        // Arrange: set up your objects and expected results
        var trueBool = true;
        var expectedResult = false;

        // Assert: check that the result is what you expected
        Assert.AreNotEqual(expectedResult, trueBool);
    }


    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
