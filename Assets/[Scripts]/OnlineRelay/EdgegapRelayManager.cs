using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FishNet.Transporting;
using FishNet.Transporting.KCP.Edgegap;
using UnityEngine;
using UnityEngine.Networking;

public class EdgegapRelayManager : MonoBehaviour
{
    [SerializeField] private string relayToken;
    [SerializeField] private EdgegapKcpTransport kcpTransport;
    void Start()
    {
        kcpTransport. OnServerConnectionState += OnServerConnectionStateChange;
        kcpTransport.OnClientConnectionState += OnClientConnectionStateChange;
    }

    public void CreatePartida()
    {
        //StartCoroutine(GetIP());
        Task.Run(CrearPartidaAsync);
    }

    void OnServerConnectionStateChange(ServerConnectionStateArgs args)
    {
        switch (args.ConnectionState)
        {
            case LocalConnectionState.Stopped:
                print("Servidor Detenido");
                break;
            case LocalConnectionState.Starting:
                break;
            case LocalConnectionState.Started:
                print("Servidor Iniciado");
                break;
            case LocalConnectionState.Stopping:
                break;
        }
    }
    
    void OnClientConnectionStateChange(ClientConnectionStateArgs args)
    {
        switch (args.ConnectionState)
        {
            case LocalConnectionState.Stopped:
                print("Cliente Detenido/Desconectado");
                break;
            case LocalConnectionState.Starting:
                break;
            case LocalConnectionState.Started:
                print("Cliente Iniciado/Conectado");
                break;
            case LocalConnectionState.Stopping:
                break;
        }
    }

    private const string kEdgegapBaseURL = "https://api.edgegap.com/v1";
    HttpClient httpClient = new HttpClient();

    public async Task CrearPartidaAsync()
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", relayToken);
        HttpResponseMessage responseMessage = await httpClient.GetAsync($"{kEdgegapBaseURL}/ip");
        string response = await responseMessage.Content.ReadAsStringAsync();
        UserIp userIp = JsonUtility.FromJson<UserIp>(response);
        print(userIp.public_ip);
    }

    IEnumerator GetIP()
    {
        using UnityWebRequest unityWebRequest = new UnityWebRequest($"{kEdgegapBaseURL}/ip", "GET");
        unityWebRequest.SetRequestHeader("Authorization", relayToken);
        unityWebRequest.downloadHandler = new DownloadHandlerBuffer();

        yield return unityWebRequest.SendWebRequest();
        
        if (unityWebRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al obtener la IP");
            Debug.LogError(unityWebRequest.error);
            yield break;
        }
        
        string response = unityWebRequest.downloadHandler.text;
        print(response);
        UserIp userIp = JsonUtility.FromJson<UserIp>(response);
        print(userIp.public_ip);
    }
    
    IEnumerator CreatePartidaCoroutine()
    {
        using UnityWebRequest unityWebRequest = new UnityWebRequest($"{kEdgegapBaseURL}/relays/sessions", "POST");
        unityWebRequest.SetRequestHeader("Authorization", relayToken);

        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al crear la partida");
            Debug.LogError(unityWebRequest.error);
            yield break;
        }
        
        string response = unityWebRequest.downloadHandler.text;
        print(response);
        
        ApiResponse apiResponse = JsonUtility.FromJson<ApiResponse>(response);
        
    }
}
