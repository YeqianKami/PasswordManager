using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.WithUGUI
{
	/// <summary>
	/// 每个项目必备，记录该项目的数据内容
	/// </summary>
	public class ItemData : MonoBehaviour
	{
		public ItemData() { }
		public ItemData(string title, string key, string value, string scription)
		{
			this._title = title;
			this._account = key;
			this._password = value;
			this._scription = scription;
		}

		public string Guid
		{
			get { return _guid; }
			set { _guid = value; }
		}
		private string _guid = "";
		/// <summary>
		/// 项目标题
		/// </summary>
		public string Title
		{
			get { return _title; }
			//set
			//{
			//	if (value == null || value == "") throw new System.NullReferenceException("标题不能为空");
			//	_title = value;
			//}
		}
		[SerializeField]
		private string _title = "";
		/// <summary>
		/// 账号
		/// </summary>
		public string Account
		{
			get { return _account; }
			//set
			//{
			//	if (value == null) throw new System.NullReferenceException("账号不能为空");
			//	_Account = value;
			//}
		}
		[SerializeField]
		private string _account = "";
		/// <summary>
		/// 密码
		/// </summary>
		public string Password
		{
			get { return _password; }
			//set
			//{
			//	if (value == null) throw new System.NullReferenceException("密码不能为空");
			//	_Password = value;
			//}
		}
		[SerializeField]
		private string _password = "";
		/// <summary>
		/// 描述，可为空字符串
		/// </summary>
		public string Scription
		{
			get { return _scription; }
			//set
			//{
			//	_scription = value == null ? "" : value;
			//}
		}
		[SerializeField]
		private string _scription = "";

		/// <summary>
		/// 用传入参数里的数据来重写本类的数据
		/// </summary>
		/// <param name="newItemData"></param>
		public void ResetItemData(ItemData newItemData,bool changeItemTitle)
		{
			this._guid = newItemData.Guid;
			this._title = newItemData.Title;
			this._account = newItemData.Account;
			this._password = newItemData.Password;
			this._scription = newItemData.Scription;

			if (changeItemTitle) GetComponent<Item>().TitleText = Title;
		}
		public void ResetItemData(string guid,string title,string account,string password,string scription, bool changeItemTitle)
		{
			try
			{
				this._guid = guid;
				this._title = title;
				this._account = account;
				this._password = password;
				this._scription = scription;
			}
			catch (System.NullReferenceException)
			{
				throw;
			}
			if (changeItemTitle) GetComponent<Item>().TitleText = Title;
		}
	}
}