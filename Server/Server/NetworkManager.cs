
//using System;
//using System.Net;
//using System.Net.Sockets;
//using System.Net.NetworkInformation;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;

///// <summary>
///// 用户标记
///// </summary>
//public class UserToken
//{
//    /// <summary>
//    /// 玩家id
//    /// </summary>
//    public uint uid;
//    public TcpClient TcpClient { get; private set; }

//    public byte[] Buffer { get; private set; }

//    public NetworkStream NetworkStream { get { return TcpClient.GetStream(); } }

//    public UserToken(TcpClient tcpClient, byte[] buffer, uint uid)
//    {
//        this.TcpClient = tcpClient;
//        this.Buffer = buffer;
//        this.uid = uid;
//    }
//}

//public class Msg
//{
//    public UserToken token;
//    public byte[] data;

//    public Msg(UserToken t, byte[] d)
//    {
//        token = t;
//        data = d;
//    }
//}

//public class NetworkManager 
//{
//    //端口号
//    private const int _port = 10010;

//    // 消息头长度
//    public const int MsgHeaderLength = 4;

//    // 消息ID和消息响应事件容器
//    private Dictionary<int, Action<byte[], UserToken>> _msgHandlers;

//    private TcpListener _listener;

//    private List<UserToken> _clients;

//    private IPAddress _address;

//    //客户端的ID计数器
//    private uint _uidCounter = 1001;

//    //用于存储玩家发送的消息，接收到的消息队列
//    protected Queue<Msg> _receivedMsg;

//    public Action<UserToken> clientConnect;

//    public Action<UserToken> clientDisconnect;

//    public NetworkManager()
//    {

//        _receivedMsg = new Queue<Msg>();

//        _msgHandlers = new Dictionary<int, Action<byte[], UserToken>>();

//        // 获取以太网有线连接IP
//        IPAddress ip = GetEthernetIP(NetworkInterfaceType.Ethernet);

//        if (ip == null)
//        {
//            // 获取以太网无线连接IP
//            ip = GetEthernetIP(NetworkInterfaceType.Wireless80211);
//        }

//        //启用监听（等待客户端连接）
//        StartListen(ip, _port);

//        Console.WriteLine("Server Start, ip = " + ip.ToString() + ", port:" + _port);
//    }


//    /// <summary>
//    /// 开始监听
//    /// </summary>
//    /// <param name="localIPAddress">IP地址</param>
//    /// <param name="listenPort">监听的端口号</param>
//    public void StartListen(IPAddress localIPAddress, int listenPort)
//    {
//        _address = localIPAddress;

//        //用户保存
//        _clients = new List<UserToken>();

//        //TCP展开监听
//        _listener = new TcpListener(_address, listenPort);

//        // 启用针对 TcpListener 实例的网络地址转换 (NAT)遍历。
//        _listener.AllowNatTraversal(true);

//        _listener.Start();
//        _listener.BeginAcceptTcpClient(new AsyncCallback(ClientAccepted), _listener);
//    }


//    /// <summary>
//    /// 客户端连接回调
//    /// </summary>
//    /// <param name="ar"></param>
//    private void ClientAccepted(IAsyncResult ar)
//    {
//        TcpListener tcpListener = (TcpListener)ar.AsyncState;

//        TcpClient tcpClient = tcpListener.EndAcceptTcpClient(ar);
//        byte[] buffer = new byte[tcpClient.ReceiveBufferSize];

//        UserToken token = new UserToken(tcpClient, buffer, _uidCounter++);

//        lock (_clients)
//        {
//            ClientConnect(token);
//            _clients.Add(token);
//        }

//        NetworkStream networkStream = token.NetworkStream;


//        //开始从缓存区里读数据，当读到数据执行ReceivedData
//        networkStream.BeginRead(token.Buffer, 0, token.Buffer.Length, ReceivedData, token);
//        tcpListener.BeginAcceptTcpClient(new AsyncCallback(ClientAccepted), ar.AsyncState);
//    }

//    /// <summary>
//    /// 收到报文回调
//    /// </summary>
//    /// <param name="ar"></param>
//    private void ReceivedData(IAsyncResult ar)
//    {
//        UserToken client = (UserToken)ar.AsyncState;
//        NetworkStream networkStream = client.NetworkStream;

//        int numberOfReadBytes = 0;

//        try
//        {
//            //结束本次读取，获取读取到的字节数
//            numberOfReadBytes = networkStream.EndRead(ar);
//        }
//        catch
//        {
//            numberOfReadBytes = 0;
//        }

//        //如果读取到的字节数为0，表示断开连接
//        if (numberOfReadBytes == 0)
//        {
//            lock (_clients)
//            {
//                _clients.Remove(client);
//                ClientDisConnect(client);
//                return;
//            }
//        }

//        //如果读取到数据
//        byte[] receivedBytes = new byte[numberOfReadBytes];
//        Buffer.BlockCopy(client.Buffer, 0, receivedBytes, 0, numberOfReadBytes);

//        //再添加到消息队列中
//        lock (_receivedMsg)
//        {
//            Msg msg = new Msg(client, receivedBytes);
//            _receivedMsg.Enqueue(msg);
//        }

//        //接着从缓冲区读取数据（此处不是递归，只是作为回调函数中可能存在新的回调）
//        networkStream.BeginRead(client.Buffer, 0, client.Buffer.Length, ReceivedData, client);
//    }


//    /// <summary>
//    /// 停止服务器
//    /// </summary>
//    /// <returns>异步TCP服务器</returns>
//    public NetworkManager Stop()
//    {
//        _listener.Stop();

//        lock (_clients)
//        {
//            for (int i = 0; i < this._clients.Count; i++)
//            {
//                _clients[i].TcpClient.Client.Disconnect(false);
//            }
//            _clients.Clear();
//        }

//        return this;
//    }

//    public UserToken GetClient(uint uid)
//    {
//        for (int i = 0; i < _clients.Count; i++)
//        {
//            UserToken client = _clients[i];
//            if (client.uid == uid)
//            {
//                return client;
//            }
//        }
//        return null;
//    }

//    public List<UserToken> GetAllClient()
//    {
//        return _clients;
//    }


//    public void ClientConnect(UserToken client)
//    {
//        EndPoint endpoint = null;

//        endpoint = (client as UserToken).TcpClient.Client.RemoteEndPoint;

//        string ip = (endpoint as IPEndPoint).Address.ToString();

//        Console.WriteLine("{0},{1}  Connected...", ip, client.uid);


//        //处理刚刚连接的游戏逻辑(连接GameServer)
//        clientConnect(client);


//    }

//    public void ClientDisConnect(UserToken client)
//    {
//        EndPoint endpoint = (client as UserToken).TcpClient.Client.RemoteEndPoint;

//        string ip = (endpoint as IPEndPoint).Address.ToString();

//        Console.WriteLine("{0} DisConnected...", ip);

//        //处理断开连接的游戏逻辑(连接GameServer)
//        clientDisconnect(client);
//    }

//    /// <summary>
//    /// 发送报文至指定的客户端
//    /// </summary>
//    /// <param name="tcpClient">客户端</param>
//    /// <param name="datagram">报文</param>
//    void Send(TcpClient tcpClient, byte[] datagram)
//    {
//        try
//        {
//            tcpClient.GetStream().BeginWrite(datagram, 0, datagram.Length, WriteData, tcpClient);
//        }
//        catch (Exception)
//        {
//            Console.WriteLine("Client Has Disconnect...");
//        }
//    }

//    private void WriteData(IAsyncResult ar)
//    {
//        ((TcpClient)ar.AsyncState).GetStream().EndWrite(ar);
//    }

//    /// <summary>
//    /// 发送报文至所有客户端
//    /// </summary>
//    /// <param name="datagram">报文</param>
//    public void SendAll(byte[] datagram)
//    {
//        for (int i = 0; i < this._clients.Count; i++)
//        {
//            Send(_clients[i].TcpClient, datagram);
//        }
//    }
//    public IPAddress GetEthernetIP(NetworkInterfaceType type)
//    {
//        //获取所有网卡信息
//        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
//        foreach (NetworkInterface adapter in nics)
//        {
//            //判断是否为以太网卡
//            if (adapter.NetworkInterfaceType == type && adapter.OperationalStatus == OperationalStatus.Up)
//            {
//                //获取以太网卡网络接口信息
//                IPInterfaceProperties ip = adapter.GetIPProperties();

//                //获取单播地址集
//                UnicastIPAddressInformationCollection ipCollection = ip.UnicastAddresses;
//                foreach (UnicastIPAddressInformation ipadd in ipCollection)
//                {
//                    // InterNetwork    IPV4地址      
//                    // InterNetworkV6        IPV6地址
//                    // Max            MAX 位址
//                    if (ipadd.Address.AddressFamily == AddressFamily.InterNetwork)
//                        //判断是否为ipv4
//                        return ipadd.Address;//获取ip
//                }
//            }
//        }

//        return null;
//    }

//    /// <summary>
//    /// 往消息响应事件容器中添加一个元素
//    /// </summary>
//    /// <param name="msgid"></param>
//    /// <param name="handler"></param>
//    public void RegisterMsg(int msgid, Action<byte[], UserToken> handler)
//    {
//        if (!_msgHandlers.ContainsKey(msgid))
//            _msgHandlers.Add(msgid, handler);
//    }


//    public Action<byte[], UserToken> GetMsgHandler(int msgid)
//    {
//        if (_msgHandlers.ContainsKey(msgid))
//            return _msgHandlers[msgid];
//        else
//        {
//            Console.WriteLine("Can not find msg id:{0}", msgid);
//            return null;
//        }
//    }

//    /// <summary>
//    /// 获取消息体
//    /// </summary>
//    /// <param name="msg"></param>
//    /// <param name="msgLength"></param>
//    /// <returns></returns>
//    public byte[] GetMsgBody(byte[] msg, int msgLength)
//    {
//        int bodyLength = msgLength - MsgHeaderLength;
//        byte[] body = new byte[bodyLength];
//        try
//        {
//            Array.Copy(msg, MsgHeaderLength, body, 0, bodyLength);
//        }
//        catch (Exception e)
//        {
//            Console.WriteLine(e);
//        }

//        return body;
//    }

//    /// <summary>
//    /// 获取消息头
//    /// </summary>
//    /// <param name="msg"></param>
//    /// <returns></returns>
//    public static byte[] GetMsgHeader(byte[] msg)
//    {
//        byte[] header = new byte[MsgHeaderLength];
//        try
//        {
//            Array.Copy(msg, 0, header, 0, MsgHeaderLength);
//        }
//        catch (Exception e)
//        {
//            Console.WriteLine(e);
//        }

//        return header;
//    }


//    public static ushort GetMsgId(byte[] byteHeader)
//    {
//        byte[] byteMsgId = new byte[2];
//        Array.Copy(byteHeader, 0, byteMsgId, 0, 2);
//        return System.BitConverter.ToUInt16(byteMsgId, 0);
//    }

//    public static ushort GetMsgLength(byte[] byteHeader)
//    {
//        byte[] byteBodyLen = new byte[2];
//        Array.Copy(byteHeader, 2, byteBodyLen, 0, 2);
//        return System.BitConverter.ToUInt16(byteBodyLen, 0);
//    }


//    public void Update()
//    {
//        //消息出队之后通过循环进行解析
//        for(int i = 0; i < _receivedMsg.Count; i++)
//        {
//            Msg msg = _receivedMsg.Dequeue();
//            ParseMsg(msg.token, msg.data);
//        }
//    }
//    /// <summary>
//    /// 解析收到的消息
//    /// </summary>
//    /// <param name="client"></param>
//    /// <param name="buffer"></param>
//    private void ParseMsg(UserToken client, byte[] buffer)
//    {
//        byte[] byteHeader = new byte[MsgHeaderLength];
//        int index = 0;
//        while (index < buffer.Length)
//        {
//            Array.Copy(buffer, index, byteHeader, 0, MsgHeaderLength);
//            index += MsgHeaderLength;

//            //获取消息id和消息长度，把4个字节对半分成22
//            ushort msgid = GetMsgId(byteHeader);
//            ushort bodyLen = GetMsgLength(byteHeader);

//            Action<byte[], UserToken> handler;

//            try
//            {
//                if (bodyLen == 0)   //如果消息体为0表示消息只有指令没有实体
//                {
//                    handler = GetMsgHandler(msgid);
//                    if(handler == null)
//                        continue;
//                    handler(new byte[0], client);
//                }
//                else
//                {
//                    byte[] byteBody = new byte[bodyLen];
//                    Array.Copy(buffer, index, byteBody, 0, bodyLen);    //取出消息体中的数据
//                    index += bodyLen;

//                    handler = GetMsgHandler(msgid);
//                    handler(byteBody, client);
//                }
//            }
//           catch(Exception ex)
//            {
//                Console.WriteLine(ex);
//            }
//        }
//    }

//    /// <summary>
//    /// 向客户端发送消息
//    /// </summary>
//    /// <param name="client"></param>
//    /// <param name="msgid"></param>
//    /// <param name="body"></param>
//    public void Send<T>(UserToken client, T msgobj, ushort msgid) where T : class
//    {
//        byte[] byteBody;
//        //如果为null说明不需要参数
//        if (msgobj == null)
//        {
//            byteBody = new byte[0];
//        }
//        else
//        {
//            byteBody = SerializeHelper.Serialize<T>(msgobj);
//        }
       
//        byte[] byteHeader = new byte[MsgHeaderLength];

//        // 消息ID
//        byte[] byteMsgId = BitConverter.GetBytes(msgid);
//        byteMsgId.CopyTo(byteHeader, 0);

//        // 消息体长度
//        ushort bodyLength = (ushort)byteBody.Length;
//        byte[] byteBodyLength = BitConverter.GetBytes(bodyLength);
//        byteBodyLength.CopyTo(byteHeader, byteMsgId.Length);

//        // 消息体
//        byte[] msg = new byte[byteHeader.Length + byteBody.Length];
//        byteHeader.CopyTo(msg, 0);
//        byteBody.CopyTo(msg, byteHeader.Length);


//        TcpClient tcpClient = (client as UserToken).TcpClient;
//        if (tcpClient.Connected)
//        {
//            Send(tcpClient, msg);
//        }
            
//    }
//    /// <summary>
//    /// 向其他玩家发送
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    /// <param name="client">除此之外</param>
//    /// <param name="msgobj"></param>
//    /// <param name="msgid"></param>
//    public void SendOther<T>(UserToken client, T msgobj, ushort msgid) where T : class
//    {
//        for (int i = 0; i < _clients.Count; i++)
//        {
//            if (_clients[i].uid!=client.uid)
//            {
//                Send<T>(_clients[i], msgobj, msgid);
//            }
//        }
//    }

//    /// <summary>
//    /// 向客户端发送消息(无参数)
//    /// </summary>
//    /// <param name="client"></param>
//    /// <param name="msgid"></param>
//    /// <param name="body"></param>
//    public void Send(UserToken client ,ushort msgid)
//    {
//        //byte[] byteBody = ProtoBufUtils.Serialize(msgobj);
//        byte[] byteBody = new byte[0];
//        byte[] byteHeader = new byte[MsgHeaderLength];

//        // 消息ID
//        byte[] byteMsgId = BitConverter.GetBytes(msgid);
//        byteMsgId.CopyTo(byteHeader, 0);

//        // 消息体长度
//        ushort bodyLength = (ushort)byteBody.Length;
//        byte[] byteBodyLength = BitConverter.GetBytes(bodyLength);
//        byteBodyLength.CopyTo(byteHeader, byteMsgId.Length);

//        // 消息体
//        byte[] msg = new byte[byteHeader.Length + byteBody.Length];
//        byteHeader.CopyTo(msg, 0);
//        byteBody.CopyTo(msg, byteHeader.Length);

//        TcpClient tcpClient = (client as UserToken).TcpClient;
//        if (tcpClient.Connected)
//            Send(tcpClient, msg);
//    }

//}