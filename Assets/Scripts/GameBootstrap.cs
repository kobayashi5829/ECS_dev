using UnityEngine;
using Unity.NetCode;
using Unity.Networking.Transport;
using Unity.Entities;
using System.Linq;

[UnityEngine.Scripting.Preserve]
public class GameBootstrap : ClientServerBootstrap
{
    private static readonly ushort PORT = 53647;

    public override bool Initialize(string defaultWorldName)
    {
#if UNITY_EDITOR
        AutoConnectPort = PORT;
        CreateDefaultClientServerWorlds();
        Debug.Log("Editor");
        return true;
#endif
        if (System.Environment.GetCommandLineArgs().Contains("-server"))
        {
            var serverWorld = CreateServerWorld(defaultWorldName);
            var listenRequest = serverWorld.EntityManager.CreateEntity(typeof(NetworkStreamRequestListen));
            var endpoint = NetworkEndpoint.AnyIpv4.WithPort(PORT);
            serverWorld.EntityManager.SetComponentData(listenRequest, new NetworkStreamRequestListen { Endpoint = endpoint });
            Debug.Log("Server");
            return true;
        }
        else
        {
            var clientWorld = CreateClientWorld(defaultWorldName);
            var connectRequest = clientWorld.EntityManager.CreateEntity(typeof(NetworkStreamRequestConnect));
            var endpoint = NetworkEndpoint.Parse("34.83.229.95", PORT);
            clientWorld.EntityManager.SetComponentData(connectRequest, new NetworkStreamRequestConnect { Endpoint = endpoint });
            Debug.Log("Client");
            return true;
        }
    }
}