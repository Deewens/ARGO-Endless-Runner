using System.Collections.Generic;

/// <summary>
/// Take care of fetching and converting data from Anvil to LeaderboardData.
/// </summary>
public class Leaderboard
{
    private readonly List<LeaderboardData> _fakeDatabase = new();

    public Leaderboard()
    {
        // TODO: To be replaced with a real database. To be replaced with real data fetched from anvil.
        FillFakeDatabase();
    }

    /// <summary>
    /// Fetch data from Anvil, convert it to LeaderboardData and return it.
    /// </summary>
    /// <remarks>For now, the method just use fake data.</remarks>
    /// <returns>The data from anvil stored as LeaderboardData struct.</returns>
    public List<LeaderboardData> FetchLeaderboardData()
    {
        return _fakeDatabase;
    }

    /// <summary>
    /// Fills the fake database with fake data.
    /// To be replaced with real data fetched from anvil.
    /// </summary>
    private void FillFakeDatabase()
    {
        _fakeDatabase.Add(new LeaderboardData(1, "Deewens", 100, 150));
        _fakeDatabase.Add(new LeaderboardData(2, "Deewens", 100, 150));
        _fakeDatabase.Add(new LeaderboardData(3, "Deewens", 100, 150));
        _fakeDatabase.Add(new LeaderboardData(4, "Deewens", 100, 150));
        _fakeDatabase.Add(new LeaderboardData(5, "Deewens", 100, 150));
        _fakeDatabase.Add(new LeaderboardData(6, "Deewens", 100, 150));
        _fakeDatabase.Add(new LeaderboardData(7, "Deewens", 100, 150));
        _fakeDatabase.Add(new LeaderboardData(8, "Deewens", 100, 150));
        _fakeDatabase.Add(new LeaderboardData(9, "Deewens", 100, 150));
        _fakeDatabase.Add(new LeaderboardData(10, "Deewens", 100, 150));
        _fakeDatabase.Add(new LeaderboardData(11, "Deewens", 100, 150));
        _fakeDatabase.Add(new LeaderboardData(12, "Deewens", 100, 150));
        _fakeDatabase.Add(new LeaderboardData(13, "Deewens", 100, 150));
        _fakeDatabase.Add(new LeaderboardData(14, "Deewens", 100, 150));
        _fakeDatabase.Add(new LeaderboardData(15, "Deewens", 100, 150));
        _fakeDatabase.Add(new LeaderboardData(16, "Deewens", 100, 150));
        _fakeDatabase.Add(new LeaderboardData(17, "Deewens", 100, 150));
        _fakeDatabase.Add(new LeaderboardData(18, "Deewens", 100, 150));
        _fakeDatabase.Add(new LeaderboardData(19, "Deewens", 100, 150));
        _fakeDatabase.Add(new LeaderboardData(20, "Deewens", 100, 150));
        _fakeDatabase.Add(new LeaderboardData(21, "Deewens", 100, 150));
        _fakeDatabase.Add(new LeaderboardData(22, "Deewens", 100, 150));
    }
}