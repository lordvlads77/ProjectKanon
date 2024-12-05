using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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

    public async void CreatePartida()
    {
        //StartCoroutine(GetIP());
        await CrearPartidaAsync();
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
        // Preguntamos por nuestra IP pública.
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", relayToken);
        HttpResponseMessage responseMessage = await httpClient.GetAsync($"{kEdgegapBaseURL}/ip");
        string response = await responseMessage.Content.ReadAsStringAsync();
        UserIp userIp = JsonUtility.FromJson<UserIp>(response);

        // Creamos lista de usuarios que van a jugar en la partida
        Users users = new Users
        {
            users = new List<User>()
        };
        users.users.Add(new User() {ip = userIp.public_ip}); // Nos agregamos a nosotros mismos.
        //Aquí si tenemos un sistema de amigos/party solemos agregar las IPs de los demás jugadores
        
        string userJson = JsonUtility.ToJson(users);
        HttpContent content = new StringContent(userJson, Encoding.UTF8, "application/json");
        responseMessage = await httpClient.PostAsync($"{kEdgegapBaseURL}/relays/sessions", content);
        response = await responseMessage.Content.ReadAsStringAsync();
        print(response);
        ApiResponse apiResponse = JsonUtility.FromJson<ApiResponse>(response);
        print("Session: " + apiResponse.session_id);

        while (!apiResponse.ready)
        {
            await Task.Delay(2500); // esta en microsegundos, se traduciria a 2.5 segundos
            responseMessage = await httpClient.GetAsync($"{kEdgegapBaseURL}/relays/sessions/{apiResponse.session_id}");
            response = await responseMessage.Content.ReadAsStringAsync();
            apiResponse = JsonUtility.FromJson<ApiResponse>(response);
            print(response);
        }
        
        //Llegando a este punto el servidor está listo para conectarnos.
        EdgegapRelayData relayData = new EdgegapRelayData(
            apiResponse.relay.ip,
            apiResponse.relay.ports.server.port,
            apiResponse.relay.ports.client.port,
            apiResponse.session_users[0].authorization_token,
            apiResponse.authorization_token
        );

        kcpTransport.SetEdgegapRelayData(relayData);
        kcpTransport.StartConnection(true); // Nos conectamos como servidor
        kcpTransport.StartConnection(false); // Nos conectamos como cliente (nos convertimos en host)
    }

    [ContextMenu("Obtener Info")]
    void ObtenerInformacionPartida()
    {
        Task task = EsperarYHostear();
    }
    
    async Task EsperarYHostear()
    {
        string session_id = "16487a466626-S";
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", relayToken);
        HttpResponseMessage responseMessage = await httpClient.GetAsync($"{kEdgegapBaseURL}/relays/sessions/{session_id}");
        string response = await responseMessage.Content.ReadAsStringAsync();
        print(response);
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
