using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// The class that will be in charge of all tests relating to the Runner.
/// </summary>

public class RunnerTests
{
    /// An instance of the main game
    private GameScript game;

    /// <summary>
    /// Sets up the testing script
    /// </summary>
    [SetUp]
    public void Setup()
    {
        GameObject gameGameObject =
          MonoBehaviour.Instantiate(
            Resources.Load<GameObject>("GamePrefab"));
        game = gameGameObject.GetComponent<GameScript>();
    }

    /// <summary>
    /// Undoes the set up for the script, clearing memory.
    /// </summary>
    [TearDown]
    public void Teardown()
    {
        Object.Destroy(game.gameObject);
    }

    /// <summary>
    /// Test to make sure the swipe left and right are working.
    /// </summary>
    /// <returns>The time to wait until the script should continue the test.</returns>
    [UnityTest]
    public IEnumerator RunnerSwipeLeftAndRightTest()
    {
        RunnerPlayer runner = game.GetRunner();
        float oldX = runner.transform.position.x;

        // Check the left swipe
        runner.SwipeStart(new Vector2(0, 0), 1);

        yield return null;

        runner.SwipeEnd(new Vector2(-10, 0), 1);

        yield return new WaitForSeconds(1);

        Assert.IsTrue(oldX > runner.transform.position.x);

        // Check the right swipe

        oldX = runner.transform.position.x;

        runner.SwipeStart(new Vector2(0, 0), 1);

        yield return null;

        runner.SwipeEnd(new Vector2(10, 0), 1);

        yield return new WaitForSeconds(1);

        Assert.IsTrue(oldX < runner.transform.position.x);
    }

    /// <summary>
    /// Test to make sure the jump is working.
    /// </summary>
    /// <returns>The time to wait until the script should continue the test.</returns>
    [UnityTest]
    public IEnumerator RunnerJumpTest()
    {
        RunnerPlayer runner = game.GetRunner();
        float oldY = runner.transform.position.y;

        runner.Jump();

        yield return new WaitForSeconds(1);

        Assert.IsTrue(oldY < runner.transform.position.y);
    }

    /// <summary>
    /// Test to make sure the sliding is working.
    /// </summary>
    /// <returns>The time to wait until the script should continue the test.</returns>
    [UnityTest]
    public IEnumerator RunnerSlidingTest()
    {
        RunnerPlayer runner = game.GetRunner();

        // Check the left swipe
        runner.SwipeStart(new Vector2(0, 0), 1);

        yield return null;

        runner.SwipeEnd(new Vector2(0, -10), 1);

        yield return new WaitForSeconds(1);

        Assert.IsTrue(runner.sliding);
    }

    /// <summary>
    /// Test to make sure the AI jumps when it should.
    /// </summary>
    /// <returns>The time to wait until the script should continue the test.</returns>
    [UnityTest]
    public IEnumerator RunnerAIJumpTest()
    {
        RunnerPlayer runner = game.GetRunner();
        AI_Brain ai = game.GetRunnerAI();
        ObstacleSpawner spawner = game.GetObstacleSpawner();

        ai.enabled = true;
        runner.enabled = false;
        runner.transform.GetChild(1).gameObject.SetActive(true);

        float oldY = runner.transform.position.y;

        GameObject temp = spawner.SpawnWideObstacle();

        temp.gameObject.transform.position = new Vector3(0,1, 15);

        yield return new WaitForSeconds(2);

        Object.Destroy(temp);
        Assert.IsTrue(oldY < runner.transform.position.y);
    }

    /// <summary>
    /// Test to make sure the AI slides when it should.
    /// </summary>
    /// <returns>The time to wait until the script should continue the test.</returns>
    [UnityTest]
    public IEnumerator RunnerAISlidingTest()
    {
        RunnerPlayer runner = game.GetRunner();
        AI_Brain ai = game.GetRunnerAI();
        ObstacleSpawner spawner = game.GetObstacleSpawner();

        ai.enabled = true;
        runner.enabled = false;
        runner.transform.GetChild(1).gameObject.SetActive(true);

        GameObject temp = spawner.SpawnHighObstacle();

        temp.gameObject.transform.position = new Vector3(0, 2, 15);

        yield return new WaitForSeconds(2);

        Object.Destroy(temp);
        Assert.IsTrue(ai.sliding);
    }
}
