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

using System.Collections;
using UnityEngine;

/// <summary>
/// A class that defines the side objects that line the path,
/// checks for collision to detect when they are off screen
/// </summary>
public class SideObject : MonoBehaviour
{
    private SideObjectSpawner _sideObjectSpawner;
    
    private void Start()
    {
        _sideObjectSpawner = FindObjectOfType<SideObjectSpawner>();
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Runner"))
        {
            StartCoroutine(ReplaceSideObject());
        }
    }
    
    /// <summary>
    /// Hide the object after 2 seconds and move it behind the player.
    /// </summary>
    private IEnumerator ReplaceSideObject()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
        _sideObjectSpawner.MoveSideObject(this);
    }
}
