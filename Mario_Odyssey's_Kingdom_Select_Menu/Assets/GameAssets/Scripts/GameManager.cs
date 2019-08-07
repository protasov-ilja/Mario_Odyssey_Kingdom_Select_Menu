using UnityEngine;

namespace Assets.GameAssets.Scripts
{
	public sealed class GameManager : IGameManager
	{
		public GameManager()
		{
			Debug.Log("Created");
		}

		public void StartGame()
		{
			Debug.Log("StartGame");
		}
	}
}
