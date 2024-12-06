using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PartidaItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nombrePartidaTxt;
    [SerializeField] TextMeshProUGUI numeroJugadoresTxt;
    EdgegapRelayManager edgegapRelayManager;

    public void SetUp(ApiResponse apiResponse, EdgegapRelayManager relayManager)
    {
        nombrePartidaTxt.text = apiResponse.session_id;
        numeroJugadoresTxt.text = apiResponse.session_users.Length.ToString();
        edgegapRelayManager = relayManager;
    }

    public async void UnirPartida()
    {
         await edgegapRelayManager.UnirPartida(nombrePartidaTxt.text);
    }
}
