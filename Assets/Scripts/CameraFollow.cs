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
/// Enum that keeps track of what kind of camera this is.
/// </summary>
internal enum CameraType {
    Runner,
    God
}

/// <summary>
/// A class that lets the camera follow the Runner
/// </summary>
public class CameraFollow : MonoBehaviour
{
    /// What type of camera this one is.
    [SerializeField] private CameraType type = CameraType.Runner;
    
    private Transform _runner;
    public Transform Runner
    {
        set
        {
            _runner = value;
            _offset = transform.position - _runner.position;
        }
    }

    /// The distance from the camera to the target (ie. the runner)
    private Vector3 _offset;

    /// <summary>
    /// Makes the camera follow the player, runner and god
    /// </summary>
    void LateUpdate()
    {
        if (_runner == null)
            return;
        
        if (type == CameraType.Runner)
        {
            Vector3 newPos = _runner.position + _offset;
            transform.position = new Vector3(transform.position.x, transform.position.y, newPos.z);
        }
        else if (type == CameraType.God)
        {
            Vector3 targetPos = new Vector3(transform.position.x, transform.position.y, _runner.position.z + _offset.z);
            transform.position = targetPos;
        }
    }
}
