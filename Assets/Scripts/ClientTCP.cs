using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public class ClientTCP : MonoBehaviour
{
    public static Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private byte[] _asyncBuffer = new byte[1024];
    public string IP_ADDRESS;
    public int PORT;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Connecting to server");
        _clientSocket.BeginConnect(IP_ADDRESS, PORT, new AsyncCallback(ConnectCallback), _clientSocket);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ConnectCallback(IAsyncResult ar)
    {
        _clientSocket.EndConnect(ar);
        while (true)
        {
            OnReceive();
        }
    }
    private void OnReceive()
    {
        byte[] _sizeInfo = new byte[4];
        byte[] _receivedBuffer = new byte[1024];
        int totalRead = 0, currentRead = 0;
        try
        {
            currentRead = totalRead = _clientSocket.Receive(_sizeInfo);
            if (totalRead <= 0)
            {
                Console.WriteLine("You are not connected to the server.");
            }
            else
            {
                while (totalRead < _sizeInfo.Length && currentRead > 0)
                {
                    currentRead = _clientSocket.Receive(_sizeInfo, totalRead, _sizeInfo.Length - totalRead, SocketFlags.None);
                    totalRead += currentRead;
                }
                int messageSize = 0;
                messageSize |= _sizeInfo[0];
                messageSize |= (_sizeInfo[1] << 8);
                messageSize |= (_sizeInfo[2] << 16);
                messageSize |= (_sizeInfo[3] << 24);
                byte[] data = new byte[messageSize];
                totalRead = 0;
                currentRead = totalRead = _clientSocket.Receive(data, totalRead, data.Length - totalRead, SocketFlags.None);
                while (totalRead < messageSize && currentRead > 0)
                {
                    currentRead = _clientSocket.Receive(data, totalRead, data.Length - totalRead, SocketFlags.None);
                    totalRead += currentRead;
                }
                ClientHandleNetworkData.HandleNetworkInformation(data);
            }
        }
        catch
        {
            Console.WriteLine("You are not connected to the server.");
        }
    }
    public static void SendData(byte[] data)
    {
        _clientSocket.Send(data);
    }
    public static void ThankYouServer()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)NetworkPackages.ClientPackets.CThankYou);
        buffer.WriteString("Thanks for the connect.");
        SendData(buffer.ToArray());
        buffer.Dispose();
    }
}
