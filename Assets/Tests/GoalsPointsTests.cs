using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Mirror;
using System.Reflection;

public class GoalsPointsTests : BaseTest
{
    private RunnerPlayer _player;
    private PickupController _pickUpController;
    private GoalController _goalController;


    [SetUp]
    public override void Setup()
    {
        base.Setup();

        Debug.Log("Setup");
        _player = game.GetRunner();
        _pickUpController = game.GetPickupController();
        _goalController = game.GetGoalController();

        var moveForward = _player.GetComponent<MoveForward>();

        var netIdentity = _player.GetComponent<NetworkIdentity>();
        Debug.Log($"isLocalPlayer before: {netIdentity.isLocalPlayer}");
        netIdentity.GetType()
            .GetProperty("isLocalPlayer")?
            .SetValue(netIdentity, true);

        Debug.Log($"isLocalPlayer after: {netIdentity.isLocalPlayer}");
        Debug.Log($"isLocalPlayer after: {moveForward.isLocalPlayer}");
        Debug.Log($"isLocalPlayer after: {moveForward.GetComponent<RunnerPlayer>().isLocalPlayer}");
    }

    [UnityTest]
    public IEnumerator TestAllPickUpGoalsAndPointsReceived()
    {
        int PickupsToSpawn = _goalController.GetCurrentCollectionGoal();
        GameObject[] _pickUpArr;
        _pickUpArr = new GameObject[PickupsToSpawn];
        int PointsForPickUps =0;
        Debug.Log("Test Pickups : " + PickupsToSpawn);

        for (int i = 0; i < _pickUpArr.Length; i++)
        {
            if (_goalController.isAppleGoal())
            {
                _pickUpArr[i] = GameObject.Instantiate(Resources.Load("Pickups/Apple") as GameObject);
                PointsForPickUps = 5;
                Debug.Log("Test Apples ");
                Debug.Log("Test Points For PickUps one of these : " + PointsForPickUps);

            }
            else if (_goalController.isPomGoal())
            {
                _pickUpArr[i] = GameObject.Instantiate(Resources.Load("Pickups/Pomegranate") as GameObject);
                PointsForPickUps = 5;
                Debug.Log("Test Poms ");
                Debug.Log("Test Points For PickUps one of these : " + PointsForPickUps);

            }
            else if (_goalController.isCoinGoal())
            {
                _pickUpArr[i] = GameObject.Instantiate(Resources.Load("Pickups/Coin") as GameObject);
                PointsForPickUps = 5;
                Debug.Log("Test Coins ");
                Debug.Log("Test Points For PickUps one of these : " + PointsForPickUps);

            }
            else if (_goalController.isMaxHealthGoal())
            {
                _pickUpArr[i] = GameObject.Instantiate(Resources.Load("Pickups/MaxHealthPotion") as GameObject);
                PointsForPickUps = 0;
                Debug.Log("Test Max Health Potions ");
                Debug.Log("Test Points For PickUps one of these : " + PointsForPickUps);

            }
            else if (_goalController.isSpeedUpGoal())
            {
                _pickUpArr[i] = GameObject.Instantiate(Resources.Load("Pickups/SpeedUpPotion") as GameObject);
                PointsForPickUps = 0;
                Debug.Log("Test Speed Up Potions ");
                Debug.Log("Test Points For PickUps one of these : " + PointsForPickUps);

            }
            else if (_goalController.isSpeedDownGoal())
            {
                _pickUpArr[i] = GameObject.Instantiate(Resources.Load("Pickups/SpeedDownPotion") as GameObject);
                PointsForPickUps = 0;
                Debug.Log("Test Speed Down Potions ");
                Debug.Log("Test Points For PickUps one of these : " + PointsForPickUps);

            }
            _pickUpArr[i].SetActive(true);
            _pickUpArr[i].transform.position = new Vector3(0, 2, 1000);
        }


        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < _pickUpArr.Length; i++)
        {
            Assert.AreNotEqual(null, _pickUpArr[i]);
        }

        PickupController _pickUp = game.GetPickupController();

        yield return new WaitForSeconds(0.1f);
        Assert.AreNotEqual(null, _pickUp);

        _player.GetComponent<Score>().ResetCombo();
        _player.GetComponent<Score>().ResetScore();
        _player.transform.position = new Vector3(0, 0, 10);
        for (int i = 0; i < _pickUpArr.Length; i++)
        {
            _pickUpArr[i].transform.position = new Vector3(0, 2, 11 + 1);
        }
        yield return new WaitForSeconds(1f);

        int tempScore = _player.GetComponent<Score>().GetScore();
        int tempCombo = _player.GetComponent<Score>().GetCurrentCombo();
        int _scoreFromCombo = 0;

        for (int i = 0; i < _pickUpArr.Length; i++)
        {
            Debug.Log("Test Points For PickUps : " + PointsForPickUps);
            _scoreFromCombo += (i + 1) * PointsForPickUps;
        }

        int expectedScore = (_pickUpArr.Length * _goalController.GetGoalMultiplier()) + _scoreFromCombo;
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(expectedScore, tempScore);
    }
}
