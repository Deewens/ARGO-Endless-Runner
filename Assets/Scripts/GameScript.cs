// Coders:
// Caroline Percy
// ...

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core;
using UnityEngine;


/// <summary>
/// 
/// </summary>
public class GameScript : MonoBehaviour
{
    /// The instance of this script in the scene.
    private static GameScript instance;

    /// The runner player in the scene.
    [SerializeField]
    private GameObject runner;

    /// The obstacle spawner in the scene.
    [SerializeField]
    private GameObject obstacleSpawner;

    /// The god players in the scene.
    [SerializeField]
    private GameObject[] gods;

    /// <summary>
    /// Sets up this script to have an instance of itself.
    /// </summary>
    private void Start()
    {
        instance = this;
    }

    /// <summary>
    /// Gets the runner in the scene from the runner.
    /// </summary>
    /// <returns>The instance of the runner player script.</returns>
    public RunnerPlayer GetRunner()
    {
        return runner.GetComponent<RunnerPlayer>();
    }

    /// <summary>
    /// Gets the runner's AI in the scene from the runner.
    /// </summary>
    /// <returns>The instance of the runner's AI.</returns>
    public AI_Brain GetRunnerAI()
    {
        return runner.GetComponent<AI_Brain>();
    }

    /// <summary>
    /// Gets the obstacle spawner script from the object.
    /// </summary>
    /// <returns>The instance of the obstacle spawner script.</returns>
    public ObstacleSpawner GetObstacleSpawner() 
    { 
        return obstacleSpawner.GetComponent<ObstacleSpawner>();
    }
}
