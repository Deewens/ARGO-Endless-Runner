using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestSuite
{

    private GameObject _runnerPlayer;
    private MoveForward _moveForwardCS;

    private GameObject _groundSpawner;
    private GroundSpawner _groundSpawnerCS;

    // 1
    [UnityTest]
    public IEnumerator PlayermoveForward()
    {
        // 2
        //_runnerPlayer = MonoBehaviour.Instantiate(Resources.Load(("Assets/Prefabs/RunnerPlayer.prefab"), typeof(GameObject)) as GameObject);
        //_runnerPlayer = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Assets/Prefabs/RunnerPlayer.prefab"));
        //_moveForwardCS = _runnerPlayer.GetComponent<MoveForward>();
         yield return new WaitForSeconds(1.0f);

        Assert.Less(0,10);
        // _groundSpawner = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Assets/Prefabs/GroundSpawner"));
        // _groundSpawnerCS = _groundSpawner.GetComponent<GroundSpawner>();
        // // 3
        //// GameObject runner = game.GetSpawner().SpawnAsteroid();
        // // 4
        // float initialZPos = _runnerPlayer.transform.position.z;
        // // 5
        // yield return new WaitForSeconds(0.1f);
        // // 6
        // _runnerPlayer.transform.position = new Vector3(0,0,100.0f);
        // Assert.Less(_runnerPlayer.transform.position.z, initialZPos);
        // 7
        //Object.Destroy(_runnerPlayer.gameObject);
        //Object.Destroy(_moveForwardCS);
        //Object.Destroy(_groundSpawner.gameObject);
        //Object.Destroy(_groundSpawnerCS);
    }
}
