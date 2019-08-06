using System;
using UnityEngine;

namespace ProjectName
{
	[Serializable]
	public sealed class Kingdom
	{
		public string name;

		[Range(-180, 180)]
		public float x;

		[Range(-89, 89)]
		public float y;

		[HideInInspector]
		public Transform visualPoint;
	}
}

