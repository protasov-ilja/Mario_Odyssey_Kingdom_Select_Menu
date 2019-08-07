using Assets.GameAssets.Scripts;
using UnityEngine;
using Zenject;

public class DependencyInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
		Container.Bind<IGameManager>().To<GameManager>().AsSingle();
    }
}