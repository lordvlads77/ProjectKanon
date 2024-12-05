using System;
using System.Collections.Generic;

[Serializable]
public class ApiResponse
{
    public string session_id;
    public uint authorization_token;
    public string status;
    public bool ready;
    public bool linked;
    public string error;
    public SessionUser[] session_users;
    public Relay relay;
    public string webhook_url;
}

[Serializable]
public class SessionUser
{
    public string ip_address;
    public float latitude;
    public float longitude;
    public uint authorization_token;
}

[Serializable]
public class Relay
{
    public string ip;
    public string host;
    public Ports ports;
}

[Serializable]
public class Ports
{
    public Port server;
    public Port client;
}

[Serializable]
public class Port
{
    public ushort port;
    public string protocol;
    public string link;
}

[Serializable]
public class UserIp
{
    public string public_ip;
}

[Serializable]
public class User
{
    public string ip;
}

[Serializable]
public class Users
{
    public List<User> users;
}