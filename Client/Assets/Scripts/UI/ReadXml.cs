using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class ReadXml : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        // 定义配置字典
        Dictionary<int, ItemCfg> dic = new Dictionary<int, ItemCfg>();

        // 定义xml文档
        XmlDocument doc = new XmlDocument();

        // 动态加载xml文档并强制转化为TextAsset
        TextAsset text = Resources.Load("Config/Item") as TextAsset;

        Debug.Log(text.text);

        // 载入文本资源的文本信息
        doc.LoadXml(text.text);

        // 通过节点路径获取配置的节点列表
        XmlNodeList nodeList = doc.SelectNodes("Nodes/Node");


        // 遍历节点列表，并获取列表中的所有数据
        for (int i = 0; i < nodeList.Count; i++)
        {
            // 获取节点列表的一个子节点，并强制转化为Xml元素;
            XmlNode node = nodeList[i];
            XmlElement elem = (XmlElement)node;
            //Debug.Log(elem.GetAttribute("Id")+"."+elem.GetAttribute("Name"));

            ItemCfg cfg = new ItemCfg();
            cfg.Id = int.Parse(elem.GetAttribute("Id"));
            cfg.Name = elem.GetAttribute("Name");
        }
    }

}
