using UnityEngine;

namespace Assets.GameAssets.Scripts
{
	public interface IKingdom
	{
		string Name { get; }
		float XPosition { get; }
		float YPosition { get; }

		Transform VisualPoint { get; set; }
	}
}
