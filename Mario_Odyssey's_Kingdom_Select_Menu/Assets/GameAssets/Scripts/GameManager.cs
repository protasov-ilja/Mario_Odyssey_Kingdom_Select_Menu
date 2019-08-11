using Assets.GameAssets.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Assets.GameAssets.Scripts
{
	public sealed class GameManager : IGameManager
	{
		//[SerializeField] private KingdomManager _kingdomManager;
		//[SerializeField] private KingdomButtonsController _kingdomButtonsController;

		public void StartGame()
		{
			Debug.Log("GameStart");
		}

		private void Start()
		{

		}
	}
}
