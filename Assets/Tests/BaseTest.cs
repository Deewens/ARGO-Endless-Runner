// Coders:
// Caroline Percy
// ...

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// 
/// </summary>

public class BaseTest
{
    /// An instance of the main game
    protected GameScript game;

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
}
