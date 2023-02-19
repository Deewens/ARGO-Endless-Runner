/*
Olympus Run - A game made as part of the ARGO Project at SETU Carlow
Copyright (C) 2023 Caroline Percy <lineypercy@me.com>, Patrick Donnelly <patrickdonnelly3759@gmail.com>, Izabela Zelek <C00247865@itcarlow.ie>, Danial-hakim <danialhakim01@gmail.com>, Adrien Dudon <dudonadrien@gmail.com>

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

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
    public AIBrain GetRunnerAI()
    {
        return runner.GetComponent<AIBrain>();
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
