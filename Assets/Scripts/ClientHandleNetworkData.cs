using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientHandleNetworkData : MonoBehaviour
{
    private delegate void Packet_(byte[] data);
    private static Dictionary<int, Packet_> Packets;

    public static void InitializeNetworkPackages()
    {
        Debug.Log("Initialize Network Packages.");
        Packets = new Dictionary<int, Packet_>
            {
                {(int) NetworkPackages.ServerPackets.SConnectionOK, HandleConnectionOK }
            };
    }
    private static void HandleConnectionOK(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        string msg = buffer.ReadString();
        buffer.Dispose();
        //ADD CODE TO EXECUTE HERE
        Debug.Log(msg);
        ClientTCP.ThankYouServer();
    }
    public void Awake()
    {
        InitializeNetworkPackages();
    }
    public static void HandleNetworkInformation(byte[] data)
    {
        int packetNum;
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        packetNum = buffer.ReadInteger();
        buffer.Dispose();
        if (Packets.TryGetValue(packetNum, out Packet_ Packet))
        {
            Packet.Invoke(data);
        }

    }
}
