/*
Olympus Run - A game made as part of the ARGO Project at SETU Carlow
Copyright (C) 2023 Caroline Percy <lineypercy@me.com>, Patrick Donnelly <patrickdonnelly3759@gmail.com>, 
                   Izabela Zelek <izabelawzelek@gmail.com>, Danial-hakim <danialhakim01@gmail.com>, 
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

using System;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerMenu : MonoBehaviour
{
    [Header("Scenes (MUST be added to build settings)")]
    [Scene] 
    [SerializeField] 
    private string mainMenuScene = "";

    [SerializeField] private GameObject _roleSelectorCanvas;

    [SerializeField] private TMP_InputField _ipInputField;

    public void OnSinglePlayerBtnClick()
    {
        var roleSelectionScript = _roleSelectorCanvas.GetComponent<RoleSelectionCanvas>();
        roleSelectionScript.SetNetworkMode(NetworkManagerMode.Host);
        roleSelectionScript.SetGameMode(NetworkGameMode.SinglePlayer);
        _roleSelectorCanvas.SetActive(true);
    }
    
    public void OnHostBtnClick()
    {
        var roleSelectionScript = _roleSelectorCanvas.GetComponent<RoleSelectionCanvas>();
        roleSelectionScript.SetNetworkMode(NetworkManagerMode.Host);
        roleSelectionScript.SetGameMode(NetworkGameMode.MultiPlayer);
        _roleSelectorCanvas.SetActive(true);
    }
    
    public void OnJoinBtnClick()
    {
        var uri = new Uri($"kcp://{_ipInputField.text}:7777");
        
        var roleSelectionScript = _roleSelectorCanvas.GetComponent<RoleSelectionCanvas>();
        roleSelectionScript.SetNetworkMode(NetworkManagerMode.ClientOnly);
        roleSelectionScript.SetServerUri(uri);
        roleSelectionScript.SetGameMode(NetworkGameMode.MultiPlayer);
        _roleSelectorCanvas.SetActive(true);
    }
    
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
