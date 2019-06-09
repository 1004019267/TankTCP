
using System.Collections.Generic;
using System.Xml;
using System.Reflection;
using System;



/// <summary>
/// 游戏配置解析器 
/// </summary>
public class ConfigParser
{
    protected T GreateAndSetValue<T>(XmlElement node)
    {
        // 通过类型创建一个对象实例
        T obj = Activator.CreateInstance<T>();

        // 获取一个类的所有字段
        FieldInfo[] fields = typeof(T).GetFields();

        for (int i = 0; i < fields.Length; i++)
        {
            string name = fields[i].Name;
            if (string.IsNullOrEmpty(name)) continue;

            string fieldValue = node.GetAttribute(name);
            if (string.IsNullOrEmpty(fieldValue)) continue;

            try
            {
                ParsePropertyValue<T>(obj, fields[i], fieldValue);
            }
            catch (Exception)
            {
                Console.WriteLine(string.Format("XML读取错误：对象类型({2}) => 属性名({0}) => 属性类型({3}) => 属性值({1})",
                    fields[i].Name, fieldValue, typeof(T).ToString(), fields[i].FieldType.ToString()));
            }
        }
        return obj;
    }


    private void ParsePropertyValue<T>(T obj, FieldInfo fieldInfo, string valueStr)
    {
        System.Object value = valueStr;

        // 将字符串解析为类中定义的类型
        if (fieldInfo.FieldType.IsEnum)
            value = Enum.Parse(fieldInfo.FieldType, valueStr);
        else
        {
            if (fieldInfo.FieldType == typeof(int))
                value = int.Parse(valueStr);
            else if (fieldInfo.FieldType == typeof(byte))
                value = byte.Parse(valueStr);
            else if (fieldInfo.FieldType == typeof(bool))
                value = bool.Parse(valueStr);
            else if (fieldInfo.FieldType == typeof(float))
                value = float.Parse(valueStr);
            else if (fieldInfo.FieldType == typeof(double))
                value = double.Parse(valueStr);
            else if (fieldInfo.FieldType == typeof(uint))
                value = uint.Parse(valueStr);
            else if (fieldInfo.FieldType == typeof(ulong))
                value = ulong.Parse(valueStr);
            //else if (fieldInfo.FieldType == typeof(Vector3))
            //{
            //    string[] str = valueStr.Split(',');
            //    value = new Vector3(float.Parse(str[0]), float.Parse(str[1]), float.Parse(str[2]));
            //}
        }

        if (value == null)
            return;

        fieldInfo.SetValue(obj, value);
    }
    /// <summary>
    /// 载入xml配置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="tablename"></param>
    /// <returns></returns>
    public  Dictionary<int, T> LoadConfig<T>(string tablename, string indexName = "Id")
    {
        // 定义配置字典
        Dictionary<int, T> dic = new Dictionary<int, T>();

        // 定义xml文档
        XmlDocument doc = new XmlDocument();

        //// 动态加载xml文档并强制转化为TextAsset
        //TextAsset text = Resources.Load("Config/" + tablename) as TextAsset;

        //// 载入文本资源的文本信息
        //doc.LoadXml(text.text);

        string path = "Config/" + tablename + ".xml";
        doc.Load(path);

        // 通过节点路径获取配置的节点列表
        XmlNodeList nodeList = doc.SelectNodes("Nodes/Node");


        // 遍历节点列表，并获取列表中的所有数据
        for (int i = 0; i < nodeList.Count; i++)
        {
            // 获取节点列表的一个子节点，并强制转化为Xml元素;
            XmlNode node = nodeList[i];
            XmlElement elem = (XmlElement)node;

            // 生成一个配置对象，并将对象的成员赋值
            T obj = GreateAndSetValue<T>(elem);

            // 获取对象类型，并通过ID获取域的值
            FieldInfo fieldInfo = obj.GetType().GetField(indexName);

            int ID = (int)fieldInfo.GetValue(obj);


            // 将读出的对象添加到配置字典中
            if (!dic.ContainsKey(ID))
            {
                dic.Add(ID, obj);
            }
        }
        //LogManager.Log(string.Format("Load {0}", typeof(T).Name));

        return dic;
    }
}