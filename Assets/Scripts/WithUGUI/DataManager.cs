using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

namespace Assets.Scripts.WithUGUI
{
	/// <summary>
	/// 数据管家
	/// </summary>
	public class DataManager : MonoBehaviour
	{
		[SerializeField]
		private GameObject ItemsHome;
		[SerializeField]
		private GameObject ItemPrefab;

		/// <summary>
		/// 数据文件路径
		/// </summary>
		private string dataPath;//= Application.persistentDataPath + "data.bin";
		public string DataPath
		{
			get { return dataPath; }
		}

		/// <summary>
		/// xml文件
		/// </summary>
		private XmlDocument XmlDoc;

		private void Check()
		{
			if (ItemsHome == null) ItemsHome = GameObject.FindWithTag("ItemsHome");
		}

		private void Awake()
		{
#if UNITY_EDITOR
			dataPath =Application.dataPath + @"/Resources/data.xml";
#else
		dataPath = Application.persistentDataPath + "/data.bin";
#endif
			MyRijndael.FilePath = dataPath;
			CheckXmlFile();
			Start2AddItemInApp();
		}

		/// <summary>
		/// 查找文件，存在则加载，不存在则创建
		/// </summary>
		private void CheckXmlFile()
		{
			Check();
			ItemPrefab = Resources.Load<GameObject>("WithUGUI/Item");

			if (System.IO.File.Exists(dataPath))
			{
				LoadData();
				Debug.Log("Load");
			}
			else
			{
				CreateDataFile();
				Debug.Log("Load");
			}
		}

		/// <summary>
		/// 加载现有文件
		/// </summary>
		private void LoadData()
		{
			XmlDoc = new XmlDocument();
			//XmlReaderSettings setting = new XmlReaderSettings();
			//setting.IgnoreComments = true;
			//var reader = XmlReader.Create(dataPath, setting);
			//XmlDoc.Load(reader);
			XmlDoc.LoadXml(MyRijndael.Decrypt());
			//reader.Close();
		}

		/// <summary>
		/// 开始添加项目
		/// </summary>
		private void Start2AddItemInApp()
		{
			XmlNodeList NodeList = XmlDoc.DocumentElement.ChildNodes;
			for (int i = 0; i < NodeList.Count; i++)
			{
				XmlNodeList list = NodeList[i].ChildNodes;
				AddItemInApp(NodeList[i].Attributes["guid"].Value, list[0].InnerText, list[1].InnerText, list[2].InnerText, list[3].InnerText);
			}
			//ItemsHome.GetComponent<UIGrid>().Reposition();
		}
		private GameObject tempG;
		private ItemData tempID;
		/// <summary>
		/// 往界面中添加项目
		/// </summary>
		/// <param name="title"></param>
		/// <param name="account"></param>
		/// <param name="password"></param>
		/// <param name="scription"></param>
		/// <returns>添加的项目</returns>
		private GameObject AddItemInApp(string guid, string title, string account, string password, string scription = "")
		{
			if (title == "" || title == null)
			{
				throw new System.NullReferenceException("标题不能为空");
			}
			else if (account == "" || account == null)
			{
				throw new System.NullReferenceException("账号不能为空");
			}
			else if (password == "" || password == null)
			{
				throw new System.NullReferenceException("密码不能为空");
			}

			tempG = Instantiate(ItemPrefab);
			tempID = tempG.GetComponent<ItemData>();

			tempID.ResetItemData(guid, title, account, password, scription == null ? "" : scription, true);
			
			tempG.transform.SetParent(ItemsHome.transform,false);

			string str = "";
			foreach (var item in ItemsHome.GetComponentsInChildren<ItemData>())
			{
				str += item.Title + "\t";
			}

			return tempG;
		}
		private GameObject AddItemInApp(ItemData newItemData)
		{
			return AddItemInApp(newItemData.Guid, newItemData.Title, newItemData.Account, newItemData.Password, newItemData.Scription);
		}

		/// <summary>
		/// 创建文件
		/// </summary>
		private void CreateDataFile()
		{
			XmlDoc = new XmlDocument();
			//创建描述信息
			XmlDeclaration dec = XmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
			XmlDoc.AppendChild(dec);
			//创建根节点
			XmlElement root = XmlDoc.CreateElement("data");
			XmlDoc.AppendChild(root);
			//保存文件
			Save();
		}

		/// <summary>
		/// 创建新项目到文件当中去
		/// </summary>
		/// <param name="itemData"></param>
		private void CreateNewItemInFile(ItemData itemData)
		{
			CheckXmlFile();//重新加载文件，如果文件被删除则重新生成文件

			XmlElement item = CreateNewItem(itemData);

			XmlDoc.DocumentElement.AppendChild(item);

			Save();
		}

		public GameObject AddItem(int sortID,ItemData itemData)
		{
			CreateNewItemInFile(itemData);
			return AddItemInApp(itemData);
		}

		/// <summary>
		/// 仅创建一个 XmlNode 而不添加到文件
		/// </summary>
		/// <param name="itemData"></param>
		/// <returns></returns>
		private XmlElement CreateNewItem(ItemData itemData)
		{
			XmlElement item = XmlDoc.CreateElement("item");

			string newGuid = System.Guid.NewGuid().ToString("D");
			item.SetAttribute("guid", newGuid);
			itemData.Guid = newGuid;

			XmlElement title = XmlDoc.CreateElement("title");
			title.InnerText = itemData.Title;
			item.AppendChild(title);

			XmlElement key = XmlDoc.CreateElement("key");
			key.InnerText = itemData.Account;
			item.AppendChild(key);

			XmlElement value = XmlDoc.CreateElement("value");
			value.InnerText = itemData.Password;
			item.AppendChild(value);

			XmlElement scription = XmlDoc.CreateElement("scription");
			scription.InnerText = itemData.Scription;
			item.AppendChild(scription);
			return item;
		}

		/// <summary>
		/// 修改旧数据
		/// </summary>
		/// <param name="oldItemData">旧数据</param>
		/// <param name="newItemData">新数据</param>
		/// <exception cref="System.NullReferenceException">未找到要被修改的数据</exception>
		public void ChangeOldData(ItemData oldItemData, ItemData newItemData)
		{
			CheckXmlFile();//重新加载文件，如果文件被删除则重新生成文件

			XmlNodeList itemLiat = XmlDoc.DocumentElement.ChildNodes;
			XmlElement target = FindTargetNode(oldItemData, itemLiat);

			if (target == null) throw new System.NullReferenceException("未找到要被修改的数据");
			target.ChildNodes[0].InnerText = newItemData.Title;
			target.ChildNodes[1].InnerText = newItemData.Account;
			target.ChildNodes[2].InnerText = newItemData.Password;
			target.ChildNodes[3].InnerText = newItemData.Scription;

			Save();
		}

		/// <summary>
		/// 在 itemList 中寻找 beFound
		/// </summary>
		/// <param name="beFound"></param>
		/// <param name="itemLiat"></param>
		/// <returns></returns>
		private XmlElement FindTargetNode(ItemData beFound, XmlNodeList itemLiat)
		{
			XmlElement target = null;
			foreach (XmlElement item in itemLiat)
			{
				if (item.Attributes["guid"].Value == beFound.Guid)
				{
					target = item;
					break;
				}
			}
			return target;
		}


		private void DeleteItemFromFile(ItemData beDelette)
		{
			CheckXmlFile();//重新加载文件，如果文件被删除则重新生成文件

			XmlNodeList itemLiat = XmlDoc.DocumentElement.ChildNodes;

			Debug.Log(beDelette.Guid + "\t" + itemLiat[0].Attributes["guid"].Value);
			Debug.Log(itemLiat.Count);

			XmlElement target = FindTargetNode(beDelette, itemLiat);

			XmlDoc.DocumentElement.RemoveChild(target);

			XmlDoc.Save(dataPath);
		}
		public void DeleteItem(ItemData beDelette)
		{
			DeleteItemFromFile(beDelette);
			Destroy(beDelette.gameObject, 0);
		}

		private void Save()
		{
			MyRijndael.Encrypt(XmlDoc.InnerXml);
		}

	}
}