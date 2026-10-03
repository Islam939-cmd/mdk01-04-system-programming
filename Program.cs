System.Net.IPAddress localAddr = System.Net.IPAddress.Parse("127.0.0.1");
int port = 5001;

System.Net.Sockets.TcpListener server = new System.Net.Sockets.TcpListener(localAddr, port);

try
{
    server.Start();
    Console.WriteLine($"Сервер успешно запущен и слушает порт {port}...");

    while (true)
    {
        using System.Net.Sockets.TcpClient client = server.AcceptTcpClient();
        Console.WriteLine($"Подключился клиент: {client.Client.RemoteEndPoint}");

        string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string message = $"Привет! Текущее время на сервере: {currentTime}\n";

        byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
        using System.Net.Sockets.NetworkStream stream = client.GetStream();
        stream.Write(data, 0, data.Length);
    }
}
catch (Exception e)
{
    Console.WriteLine($"Ошибка при работе сервера: {e.Message}");
}
finally
{
    server.Stop();
}
