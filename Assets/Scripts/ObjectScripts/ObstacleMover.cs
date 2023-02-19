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
/// A class that moves the obstacles towards the player
/// </summary>
public class ObstacleMover : MonoBehaviour
{
    public Transform targetPos;
    public Transform startPos;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    /// <summary>
    /// moves an obstacle towards the player
    /// </summary>
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos.position, 0.05f);

        if(transform.position == targetPos.position )
        {
            transform.position = startPos.position;
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Runner"))
        {
            Debug.Log("hit");
        }
    }
}
