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

    [SerializeField] GameObject partidasUIGameObject;
    [SerializeField] Transform partidaItemContainer;
    [SerializeField] GameObject partidaItemPrefab;

    void ActualizarListaPartidasUI(Sessions sessions)
    {
        foreach (Transform child in partidaItemContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (ApiResponse partidaData in sessions.sessions)
        {
            GameObject newItem = Instantiate(partidaItemPrefab, partidaItemContainer);
            PartidaItem partidaItem = newItem.GetComponent<PartidaItem>();
            partidaItem.SetUp(partidaData, this);
        }
    }
    
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
                partidasUIGameObject.SetActive(true);
                break;
            case LocalConnectionState.Starting:
                partidasUIGameObject.SetActive(false);
                break;
            case LocalConnectionState.Started:
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

        ConectarnosAPartida(apiResponse);
    }

    void ConectarnosAPartida(ApiResponse apiResponse)
    {
        //Llegando a este punto el servidor está listo para conectarnos.

        uint userToken = 0;
        if (apiResponse.session_users != null)
        {
            userToken = apiResponse.session_users[0].authorization_token;
        }
        else
        {
            userToken = apiResponse.session_user.authorization_token;
        }
        EdgegapRelayData relayData = new EdgegapRelayData(
            apiResponse.relay.ip,
            apiResponse.relay.ports.server.port,
            apiResponse.relay.ports.client.port,
            userToken,
            apiResponse.authorization_token
        );

        kcpTransport.SetEdgegapRelayData(relayData);
        if (apiResponse.session_users != null)
        {
            kcpTransport.StartConnection(true); // Nos conectamos como servidor
        }
        kcpTransport.StartConnection(false); // Nos conectamos como cliente (nos convertimos en host)
    }

    public async void RefreshPartidas()
    {
        await GetTodasLasPartidasAsync();
    }
    
    async Task GetTodasLasPartidasAsync()
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", relayToken);
        HttpResponseMessage responseMessage = await httpClient.GetAsync($"{kEdgegapBaseURL}/relays/sessions");
        string response = await responseMessage.Content.ReadAsStringAsync();

        Sessions sessions = JsonUtility.FromJson<Sessions>(response);
        ActualizarListaPartidasUI(sessions);
    }

    public async Task UnirPartida(string session_id)
    {
        // Preguntamos por nuestra IP pública.
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", relayToken);
        HttpResponseMessage responseMessage = await httpClient.GetAsync($"{kEdgegapBaseURL}/ip");
        string response = await responseMessage.Content.ReadAsStringAsync();
        UserIp userIp = JsonUtility.FromJson<UserIp>(response);
        
        //Aqui
        JoinSession joinSessionData = new JoinSession
        {
            session_id = session_id,
            user_ip = userIp.public_ip
        };
        string userJson = JsonUtility.ToJson(joinSessionData);
        HttpContent content = new StringContent(userJson, Encoding.UTF8, "application/json");
        responseMessage = await httpClient.PostAsync($"{kEdgegapBaseURL}/relays/sessions:authorize-user", content);
        response = await responseMessage.Content.ReadAsStringAsync();
        print(response);
        ApiResponse apiResponse = JsonUtility.FromJson<ApiResponse>(response);
        
        ConectarnosAPartida(apiResponse);
    }

    async Task BorrarPartida(string session_id)
    {
        HttpResponseMessage responseMessage = await httpClient.DeleteAsync($"{kEdgegapBaseURL}/relays/sessions/{session_id}");
        string response = await responseMessage.Content.ReadAsStringAsync();
    }
    
    [ContextMenu("Borrar todas las partidas")]
    async void DevBorrarTodasPartidas()
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", relayToken);
        HttpResponseMessage responseMessage = await httpClient.GetAsync($"{kEdgegapBaseURL}/relays/sessions");
        string response = await responseMessage.Content.ReadAsStringAsync();

        Sessions sessions = JsonUtility.FromJson<Sessions>(response);
        foreach (ApiResponse partida in sessions.sessions)
        {
            await BorrarPartida(partida.session_id);
        }
        print("Todas las partidas fueron borradas");
    }
}
