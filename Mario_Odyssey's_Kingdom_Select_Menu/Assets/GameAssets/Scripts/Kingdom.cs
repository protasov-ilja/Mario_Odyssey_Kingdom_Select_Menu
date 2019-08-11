using System;
using UnityEngine;

namespace Assets.GameAssets.Scripts
{
	[Serializable]
	public sealed class Kingdom : IKingdom
	{
		[SerializeField] private string _name;
		[SerializeField, Range(-180, 180)] private float _xPosition;
		[SerializeField, Range(-89, 89)] private float _yPosition;

		public string Name => _name;
		public float XPosition => _xPosition;
		public float YPosition => _yPosition;

		public Transform VisualPoint { get; set; }
	}
}

