using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.WithUGUI
{
	public class Message : MonoBehaviour
	{
		[SerializeField]
		private Text MsgInfo;
		[SerializeField]
		private Image bg;

		public void ShowMessage(string msg, float delTime)
		{
			MsgInfo.text = msg;
			StartCoroutine("Start2Destory", delTime);
			tempColor.x = bg.color.r;
			tempColor.y = bg.color.g;
			tempColor.z = bg.color.b;
			tempColor.w = bg.color.a;

			bg.GetComponent<RectTransform>().sizeDelta = new Vector2(
				MsgInfo.GetComponent<RectTransform>().sizeDelta.x ,
				bg.GetComponent<RectTransform>().sizeDelta.y);
		}
		private bool isBegin = false;
		private Vector4 tempColor = new Vector4();
		private void Update()
		{
			if (isBegin)
			{
				tempColor.w = Mathf.Lerp(bg.color.a, 0, Time.deltaTime * 2);
				bg.color = tempColor;
				MsgInfo.color = new Color(1, 1, 1, tempColor.w);
				if (bg.color.a < 0.03f)
				{
					DestroyMySelf();
					isBegin = false;
				}
			}
		}

		IEnumerator Start2Destory(float delTime)
		{
			Debug.Log("StartCoroutine");
			yield return new WaitForSeconds(delTime);
			isBegin = true;
		}
		
		private void DestroyMySelf()
		{
			Destroy(gameObject);
		}
	}
}