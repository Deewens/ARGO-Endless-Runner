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
/// A class to manage the spawning of objects on the bounds of the track
/// </summary>
public class SideObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject sideObjectPrefab;
    private Vector3 _nextSpawnPos;

    /// <summary>
    /// Start spawns the first 15 pairs of columns that line the track
    /// </summary>
    private void Start()
    {
        for (var i = 0; i < 15; i++)
        {
            var sideObject = Instantiate(sideObjectPrefab, _nextSpawnPos, Quaternion.identity);
            _nextSpawnPos = sideObject.transform.GetChild(1).transform.position;
        }
    }

    /// <summary>
    /// Spawns the columns that line the track and stores the position of the next spawn location
    /// </summary>
    public void MoveSideObject(SideObject sideObject)
    {
        sideObject.transform.position = _nextSpawnPos;
        _nextSpawnPos = sideObject.transform.GetChild(1).transform.position;
        sideObject.gameObject.SetActive(true);
    }
}
