using System;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private RectTransform rowTemplatePrefab;
    [SerializeField] private RectTransform content;

    private Leaderboard _leaderboard;

    private void Awake()
    {
        _leaderboard = new Leaderboard();
        var data = _leaderboard.FetchLeaderboardData();

        foreach (var value in data)
        {
            var newRow = Instantiate(rowTemplatePrefab, content.transform);
            LeaderboardRowUI rowUI = newRow.GetComponent<LeaderboardRowUI>();
            if (rowUI == null)
            {
                Debug.LogError("The row template prefab does not have a LeaderboardRowUI component.");
                return;
            }
            
            rowUI.SetData(value.no, value.username, value.bestRunnerScore, value.bestGodScore);
        }
    }
    
    public void OnCloseButtonClicked()
    {
        gameObject.SetActive(false);
    }
}
