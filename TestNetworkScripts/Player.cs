using FishNet.Managing.Server;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public sealed class Player : NetworkBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField]
    private Pawn pawnPrefab;

    [SyncVar]
    public Pawn controlledPawn;


    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!IsOwner) return;

        Instance = this;
    }

    [ServerRpc]
    public void ServerSpawnPawn()
    {
        Pawn pawnInstance = Instantiate(pawnPrefab);

        ServerManager.Spawn(pawnInstance.NetworkObject, Owner);

        controlledPawn = pawnInstance;

        pawnInstance.controllingPlayer = this;
    }

    [ServerRpc]
    public void ServerDespawnPawn()
    {
        if (controlledPawn != null) ServerManager.Despawn(controlledPawn.NetworkObject);
    }

    [ServerRpc]
    public void ServerDisconnect()
    {
        Owner.Disconnect(false);
    }
}
