public struct LeaderboardData
{
    public LeaderboardData(int no, string username, int bestRunnerScore, int bestGodScore)
    {
        this.no = no;
        this.username = username;
        this.bestRunnerScore = bestRunnerScore;
        this.bestGodScore = bestGodScore;
    }

    public int no;
    public string username;
    public int bestRunnerScore;
    public int bestGodScore;
}