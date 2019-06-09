//using System.IO;
//using System.Runtime.InteropServices;
//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Formatters.Binary;
//using ProtoBuf;

///// <summary>
///// 利用protobuf进行序列化和反序列化
///// </summary>
//class SerializeHelper
//{
//    ///// <summary>
//    ///// 序列化class为byte[]
//    ///// </summary>
//    ///// <typeparam name="T"></typeparam>
//    ///// <param name="obj"></param>
//    ///// <returns></returns>
//    //public static byte[] Serialize<T>(T obj) where T : class
//    //{
//    //    MemoryStream stream = new MemoryStream();
//    //    Serializer.Serialize(stream,obj);
//    //    return stream.ToArray();
//    //}

//    //public static T Deserialize<T>(byte[] buffer) where T : class
//    //{
//    //    MemoryStream stream = new MemoryStream(buffer);
//    //    return Serializer.Deserialize<T>(stream);
//    //}
//    /// <summary>
//    /// 序列化
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    /// <param name="value"></param>
//    /// <returns></returns>
//    public static byte[] Serialize<T>(T value)
//    {
//        MemoryStream ms = new MemoryStream();
//        Serializer.Serialize<T>(ms, value);
//        byte[] data = ms.ToArray();//length=27  709

//        return data;
//    }
//    /// <summary>
//    /// 反序列化
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    /// <param name="value"></param>
//    /// <returns></returns>
//    public static T Deserialize<T>(byte[] value) where T : new()
//    {
//        if (value == null)
//        {
//            return new T();
//        }
//        else
//        {
//            MemoryStream ms1 = new MemoryStream(value);
//            T p1 = Serializer.Deserialize<T>(ms1);
//            return p1;
//        }
//    }
//}