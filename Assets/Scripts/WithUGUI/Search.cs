using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.WithUGUI
{
	class Search : MonoBehaviour
	{
		private GameObject ItemsHome;
		private Regex _Regex;
		private InputField InputField;

		private void Start()
		{
			ItemsHome = GameObject.FindWithTag("ItemsHome");
			InputField = GetComponent<InputField>();
		}

		public void OnValueChange()
		{
			if (InputField.text != "")
			{
				_Regex = new Regex(InputField.text.ToUpper());
				SearchItem();
			}
			else
			{
				ShowAll();
			}
		}
		private void SearchItem()
		{
			Debug.Log(ItemsHome.transform.childCount);
			for (int i = 0; i < ItemsHome.transform.childCount; i++)
			{
				if(_Regex.IsMatch(ItemsHome.transform.GetChild(i).GetComponent<Item>().TitleText.ToUpper()))
				{
					ItemsHome.transform.GetChild(i).gameObject.SetActive(true);
				}
				else
				{
					ItemsHome.transform.GetChild(i).gameObject.SetActive(false);
				}
			}
		}
		private void ShowAll()
		{
			for (int i = 0; i < ItemsHome.transform.childCount; i++)
			{
				ItemsHome.transform.GetChild(i).gameObject.SetActive(true);
			}
		}
	}
}
