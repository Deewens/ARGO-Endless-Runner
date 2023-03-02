/*
Olympus Run - A game made as part of the ARGO Project at SETU Carlow
Copyright (C) 2023 Caroline Percy <lineypercy@me.com>, Patrick Donnelly <patrickdonnelly3759@gmail.com>, 
                   Izabela Zelek <izabelawzelek@gmail.com>, Danial Hakim <danialhakim01@gmail.com>, 
                   Adrien Dudon <dudonadrien@gmail.com>

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

using Codice.Client.Common.GameUI;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class GodReferencesSetter : NetworkBehaviour
{
    [Header("The UI linked to the god")]
    [SerializeField]
    private GameObject uiPrefab;

    [Header("The camera linked to the god")]
    [SerializeField]
    private GodCamera _cameraPrefab;

    private GodPlayer _player;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        _player = GetComponent<GodPlayer>();
        GameObject ui = Instantiate(uiPrefab);
        GodCamera runnerCamera = Instantiate(_cameraPrefab);

        var analyticsManager = FindObjectOfType<AnalyticsManager>();
        if (analyticsManager != null)
        {
            analyticsManager.God = gameObject;
        }


        int index = 1;
        foreach (Transform child in ui.transform)
        {
            child.GetComponent<Button>().onClick.AddListener(delegate { _player.ChooseAttack(index); });

            index++;

            child.transform.Find("Background").gameObject.SetActive(false);
            child.transform.Find("Selected").gameObject.SetActive(false);
        }

    }
}
