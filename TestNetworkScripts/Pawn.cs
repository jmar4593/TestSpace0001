using FishNet.Object.Synchronizing;
using UnityEngine.InputSystem;
using UnityEngine;
using FishNet.Object;

public class Pawn : NetworkBehaviour
{
    [SyncVar]
    public Player controllingPlayer;

    [SerializeField]
    private InputActionAsset inputActionMap;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float rotationSpeed;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!IsOwner) return;

        if (!inputActionMap.enabled) inputActionMap.Enable();
    }

    private void Update()
    {
        if (!IsOwner) return;

        float horizontal = inputActionMap.FindActionMap("Pawn").FindAction("Horizontal").ReadValue<float>();

        float vertical = inputActionMap.FindActionMap("Pawn").FindAction("Horizontal").ReadValue<float>();

        transform.Translate(transform.forward * (vertical * movementSpeed * Time.deltaTime), Space.World);

        transform.Rotate(Vector3.up * (horizontal * rotationSpeed * Time.deltaTime), Space.World);
        transform.Rotate(Vector3.up * (horizontal * rotationSpeed * Time.deltaTime), Space.World);
    }
}
