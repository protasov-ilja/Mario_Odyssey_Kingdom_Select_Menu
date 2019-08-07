using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using UnityEngine;

namespace Assets.GameAssets.Scripts.DI
{
	public sealed class DITest : MonoBehaviour
	{
		[Inject]
		private IGameManager _gameManager;

		private void Start()
		{
			_gameManager.StartGame();
		}
	}
}
