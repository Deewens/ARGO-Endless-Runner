using System.Linq;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tests
{
    public class LeaderboardTest
    {
        private GameObject InstantiateLeaderboardUI()
        {
            return Object.Instantiate(Resources.Load<GameObject>("Leaderboard/Leaderboard"));
        }

        [Test]
        public void LeaderboardUICreated()
        {
            GameObject leaderboardUI = InstantiateLeaderboardUI();

            Assert.NotNull(leaderboardUI);
        }

        [Test]
        public void LeaderboardUIScriptAttached()
        {
            GameObject leaderboardUI = InstantiateLeaderboardUI();
            LeaderboardUI leaderboardUIScript = leaderboardUI.GetComponent<LeaderboardUI>();

            Assert.NotNull(leaderboardUIScript);
        }

        [Test]
        public void CloseButtonCreated()
        {
            var leaderboardUI = InstantiateLeaderboardUI();
            var closeButton = leaderboardUI.GetComponentInChildren<Button>();

            Assert.NotNull(closeButton);
            Assert.AreEqual(closeButton.name, "CloseButton");
        }

        /// <summary>
        /// Check that the view to show the leaderboard is properly instantiated with the proper children GameObjects
        /// </summary>
        [Test]
        public void ScrollViewExist()
        {
            var leaderboardUI = InstantiateLeaderboardUI();
            var scrollRect = leaderboardUI.GetComponentInChildren<ScrollRect>();

            Assert.NotNull(scrollRect); // Check that there is a ScrollView component

            var content = scrollRect.transform
                .Find("Viewport")
                .Find("Content");

            Assert.NotNull(
                content); // Check that the "Content" GameObject is present because that were rows should be added
        }

        [Test]
        public void AreLeaderboardDataFetched()
        {
            var leaderboardUI = InstantiateLeaderboardUI();           
            leaderboardUI.SetActive(true);
            
            var scrollRect = leaderboardUI.GetComponentInChildren<ScrollRect>();
            var content = scrollRect.transform
                .Find("Viewport")
                .Find("Content");

            // TODO: for now, data are fake in the actual code, but later data will be fetched async from a real DB.
            // so this will have to be changed to use fakeData only for this test!
            Assert.True(content.childCount > 0);
        }

        [Test]
        public void IsRowAddedToLeaderboard()
        {
            GameObject leaderboardUI = InstantiateLeaderboardUI();
            leaderboardUI.SetActive(true);
            
            LeaderboardUI leaderboardUIScript = leaderboardUI.GetComponent<LeaderboardUI>();
            leaderboardUIScript.AddRow(new LeaderboardData(0, "Test", 50, 100));

            var rows = leaderboardUI.GetComponentsInChildren<LeaderboardRowUI>();
            var lastInsertedRow = rows.Last();
            
            var noText = lastInsertedRow.transform.Find("NoText").GetComponent<TextMeshProUGUI>();
            var usernameScoreText = lastInsertedRow.transform.Find("UsernameText").GetComponent<TextMeshProUGUI>();
            var bestRunnerScoreText = lastInsertedRow.transform.Find("BestRunnerScoreText").GetComponent<TextMeshProUGUI>();
            var bestGodScoreText = lastInsertedRow.transform.Find("BestGodScoreText").GetComponent<TextMeshProUGUI>();
            
            Assert.AreEqual(noText.text, "0");
            Assert.AreEqual(usernameScoreText.text, "Test");
            Assert.AreEqual(bestRunnerScoreText.text, "50");
            Assert.AreEqual(bestGodScoreText.text, "100");
        }
    }
}