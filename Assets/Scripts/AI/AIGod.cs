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

using System.Collections.Generic;
using UnityEngine;

public class AIGod : MonoBehaviour
{
    [SerializeField] private List<GodAttack> attackList = new List<GodAttack>();
    
    /// <summary>
    /// Simply returns a random attack from the list.
    /// </summary>
    /// <remarks>
    /// This method will be replaced by a proper AI logic later on (probably using fuzzy logic or something like that).
    /// </remarks>
    /// <returns>The selected attack</returns>
    public GodAttack GetRandomAttack()
    {
        int rand = UnityEngine.Random.Range(0, attackList.Count);
        return attackList[rand];
    }
    
    /// <summary>
    /// Returns the requested attack
    /// </summary>
    /// <param name="chosenAttack"></param>
    /// <returns></returns>
    public GodAttack GetAttack(int chosenAttack)
    {
        return attackList[chosenAttack];
    }
}