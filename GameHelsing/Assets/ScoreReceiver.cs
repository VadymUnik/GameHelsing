using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class ScoreReceiver : MonoBehaviour
{
    public string serverIp = "127.0.0.1"; // Change to your server IP
    public int serverPort = 8080;
    public string topPlayersText; // UI Text component to display top players

    public string RequestTopScores()
    {
        try
        {
            TcpClient client = new TcpClient(serverIp, serverPort);
            NetworkStream stream = client.GetStream();
            byte[] data = Encoding.ASCII.GetBytes("GET_TOP_PLAYERS");
            stream.Write(data, 0, data.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            topPlayersText = response;

            stream.Close();
            client.Close();

            return topPlayersText;
        }
        catch (SocketException ex)
        {
            Debug.Log($"SocketException: {ex}");
            return $"SocketException: {ex}";
        }
    }
}
