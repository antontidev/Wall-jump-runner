using Cinemachine;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public interface IPlayerMover
{
    Vector2 Move
    {
        get; set;
    }

    bool Jump
    {
        get; set;
    }

    bool Attack
    {
        get; set;
    }
}

public class KeyboardMover : IPlayerMover
{
    public Vector2 Move 
    {
        get 
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");

            return new Vector2(horizontal, vertical);
        }
        set { }
    }
    public bool Jump 
    {
        get 
        {
            return Input.GetKeyDown("Space");
        }
        set { }
    }
    public bool Attack 
    {
        get 
        {
            return Input.GetMouseButtonDown(0);
        }
        set { }
    }
}

/// <summary>
/// Don't think that it's really good
/// </summary>
public class JoystickMover : IPlayerMover
{
    public Vector2 Move
    {
        get; set;
    }

    public bool Jump
    {
        get; set;
    }

    public bool Attack
    {
        get; set;
    }
}

public interface ISurfaceChecker
{
    bool CheckOnSurface(Vector3 newMove);
}

public class SimplePhysicsSurfaceChecker : ISurfaceChecker
{
    public bool CheckOnSurface(Vector3 newMove)
    {
        var moveWithY = new Vector3(newMove.x, 4f, newMove.z);

        var onSurface = Physics.Raycast(moveWithY, Vector3.down);

        Debug.Log(onSurface);

        return onSurface;
    }
}

public class PlayerController : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
    [Inject]
    private NetworkCameraController camController;

    [Inject]
    private IPlayerMover mover;

    [SerializeField]
    private TextMesh nickname;

    public float speed;

    public Rigidbody rb;

    private ISurfaceChecker surfaceChecker;

    private void Start()
    {
        if (photonView.IsMine)
        {
            camController.ConnectCamera(transform);
        }

        surfaceChecker = new SimplePhysicsSurfaceChecker();
    }

    /// <summary>
    /// It will be in another place in future
    /// </summary>
    /// <param name="nick"></param>
    public void SetNickname(string nick)
    {
        nickname.text = nick;
    }

    /// <summary>
    /// Not so optimally movement
    /// </summary>
    void Update()
    {
        var movement = mover.Move;

        var moveVector3 = new Vector3(movement.y, 0.0f, -movement.x);

        if (photonView.IsMine &&
            moveVector3 != Vector3.zero && 
            surfaceChecker.CheckOnSurface(moveVector3))
        {
            moveVector3 *= speed;

            rb.AddForce(moveVector3);
        }
    }

    void IPunInstantiateMagicCallback.OnPhotonInstantiate(PhotonMessageInfo info)
    {
        info.Sender.TagObject = gameObject;
    }
}
