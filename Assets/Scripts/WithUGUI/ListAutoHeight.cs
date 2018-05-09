using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 给 Box 物体挂载，改变元素数量的时候调用ResetHeight()方法
/// </summary>
namespace Assets.Scripts.WithUGUI
{
	class ListAutoHeight:MonoBehaviour
	{
		private RectTransform RectTransform;

		private void Start()
		{
			RectTransform = GetComponent<RectTransform>();
			ResetHeight();
		}

		int total;
		/// <summary>
		/// 重置盒子高度
		/// </summary>
		public void ResetHeight()
		{
			if(RectTransform == null)RectTransform = GetComponent<RectTransform>();
			total = transform.childCount * (150 + 10);
			Debug.Log("---" + transform.childCount);
			RectTransform.sizeDelta = new Vector2(RectTransform.sizeDelta.x, total);
		}
	}
}
