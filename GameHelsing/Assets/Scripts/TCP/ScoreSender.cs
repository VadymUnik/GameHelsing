using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class ScoreSender : MonoBehaviour
{
    public string serverIp = "127.0.0.1"; // Change to your server IP
    public int serverPort = 8080;

    public bool SendScore(string playerName, int score)
    {
        try
        {
            TcpClient client = new TcpClient(serverIp, serverPort);
            NetworkStream stream = client.GetStream();
            string message = $"{playerName}:{score}";
            byte[] data = Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);
            stream.Close();
            client.Close();
            return true;
        }
        catch (SocketException ex)
        {
            Debug.Log($"SocketException: {ex}");
            return false;
        }
    }
}
