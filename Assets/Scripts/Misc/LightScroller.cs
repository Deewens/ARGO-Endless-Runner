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

public class LightScroller : MonoBehaviour
{
    private Transform _runner;

    private Vector3 _offset;

    public Transform Runner
    {
        set
        {
            _runner = value;
            _offset = transform.position - _runner.position;
        }
    }

    void Start()
    {
        _runner = GameObject.Find("RunnerPlayer").transform;
        _offset = transform.position - _runner.position;
    }

    private void Update()
    {
        if(_runner == null)
        {
            Debug.Log("Cannot Find Runner in Scene - null reference");
            _runner = GameObject.Find("RunnerPlayer").transform;
            _offset = transform.position - _runner.position;
        }
    }

    void LateUpdate()
    {
        Vector3 newPos = _runner.position + _offset;
        transform.position = new Vector3(transform.position.x, transform.position.y, newPos.z);
    }
}
