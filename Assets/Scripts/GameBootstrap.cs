using UnityEngine;
using Unity.NetCode;
using Unity.Networking.Transport;
using Unity.Entities;

[UnityEngine.Scripting.Preserve]
public class GameBootstrap : ClientServerBootstrap
{
    public override bool Initialize(string defaultWorldName)
    {
#if UNITY_EDITOR
        AutoConnectPort = 53647;
        CreateDefaultClientServerWorlds();
        Debug.Log("Editor");
#elif SERVER_BUILD
        var serverWorld = CreateServerWorld(defaultWorldName);
        var listenRequest = serverWorld.EntityManager.CreateEntity(typeof(NetworkStreamRequestListen));
        var endpoint = NetworkEndpoint.Parse("0.0.0.0", 53647);
        serverWorld.EntityManager.SetComponentData(listenRequest, new NetworkStreamRequestListen { Endpoint = endpoint });
        Debug.Log("Server");
#else
        var clientWorld = CreateClientWorld(defaultWorldName);
        var connectRequest = clientWorld.EntityManager.CreateEntity(typeof(NetworkStreamRequestConnect));
        var endpoint = NetworkEndpoint.Parse("127.0.0.1", 53647);
        clientWorld.EntityManager.SetComponentData(connectRequest, new NetworkStreamRequestConnect { Endpoint = endpoint });
        Debug.Log("Client");
#endif
        return true;
    }
}