using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule
{
    public int currentLane;
    public int nextLane;
    public string obstacleTag;
    public int obstacleLane;
    public int weight = 1;

    public bool Matches(int t_currentLane, int t_nextLane, string t_obstacleTag, int t_obstacleLane)
    {
        // Check whether this rule matches the current state of the game
        return this.currentLane == t_currentLane
            && this.nextLane == t_nextLane
            && this.obstacleTag == t_obstacleTag
            && this.obstacleLane == t_obstacleLane;
    }

    public int getNextLane(int t_currentLane, string t_obstacleTag, int t_obstacleLane)
    {
        if (currentLane == t_currentLane && obstacleLane == t_obstacleLane && obstacleTag == t_obstacleTag)
        { 
            return weight;
        }
        //Debug.Log("rule doesnt exist yet");
        return 0; // the rule doesnt exist yet 
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Rule other = (Rule)obj;
        return currentLane == other.currentLane &&
               nextLane == other.nextLane &&
               obstacleTag == other.obstacleTag &&
               obstacleLane == other.obstacleLane &&
               weight == other.weight;
    }

    public override int GetHashCode()
    {
        return currentLane.GetHashCode() ^
               nextLane.GetHashCode() ^
               obstacleTag.GetHashCode() ^
               obstacleLane.GetHashCode() ^
               weight.GetHashCode();
    }
}
