using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.WithUGUI
{
	class ScrollBarTransition : MonoBehaviour
	{
		private Scrollbar Scrollbar;
		[SerializeField]
		private bool isGoing = false;
		[SerializeField]
		private float TargetPosition;
		[SerializeField]
		private float Speed;
		[SerializeField]
		private float del = 0.01f;

		private void Start()
		{
			Scrollbar = GetComponent<Scrollbar>();
		}
		private void Update()
		{
			if (!isGoing) { return; }
			if (Math.Abs(Scrollbar.value - TargetPosition) > del)
			{
				Scrollbar.value = Mathf.Lerp(Scrollbar.value, TargetPosition, Time.deltaTime * Speed);
				//Debug.Log(Scrollbar.value);
			}
			else
			{
				isGoing = false;
			}
		}

		public void TurnTo(float position, float speed = 8)
		{
			TargetPosition = position;
			Speed = speed;

			isGoing = true;
		}
	}
}
