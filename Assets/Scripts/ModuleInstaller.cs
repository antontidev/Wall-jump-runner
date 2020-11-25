using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Cinemachine;

public class ModuleInstaller : MonoInstaller
{
    [SerializeField]
    private NetworkCameraController vCam;

    public override void InstallBindings()
    {
        Container.Bind<NetworkCameraController>().FromInstance(vCam).AsCached().NonLazy();
        Container.Bind<IPlayerMover>().To<JoystickMover>().AsSingle();
    }
}
