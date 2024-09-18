using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField]
    private Button spawnButton;

    [SerializeField]
    private Button despawnButton;

    [SerializeField]
    private Button disconnectButton;

    private void Update()
    {
        if (Player.Instance == null) return;

        spawnButton.interactable = Player.Instance.controlledPawn == null;

        despawnButton.interactable = Player.Instance.controlledPawn != null;
    }
}
