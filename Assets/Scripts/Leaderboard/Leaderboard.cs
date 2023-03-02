/*
Olympus Run - A game made as part of the ARGO Project at SETU Carlow
Copyright (C) 2023 Caroline Percy <lineypercy@me.com>, Patrick Donnelly <patrickdonnelly3759@gmail.com>, Izabela Zelek <izabelawzelek@gmail.com>, Danial-hakim <danialhakim01@gmail.com>, Adrien Dudon <dudonadrien@gmail.com>

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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Take care of fetching and converting data from Anvil to LeaderboardData.
/// </summary>
public class Leaderboard : MonoBehaviour
{
    private readonly List<LeaderboardData> _fakeDatabase = new();

    public Leaderboard()
    {
        // TODO: To be replaced with a real database. To be replaced with real data fetched from anvil.
        //FetchLeaderboardData();
       // FillFakeDatabase();
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

    public IEnumerator GetData()
    {
        string url = "https://TQLOBBSN2N5PMVQY.anvil.app/IANHMSZIEXYQHRVG3CB6WIA4/_/api/getleaderboard";

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        
        if (www.result != UnityWebRequest.Result.Success)
        {
            UnityEngine.Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            UnityEngine.Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
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