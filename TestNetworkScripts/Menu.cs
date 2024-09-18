using FishNet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class Menu : MonoBehaviour
{

    [SerializeField]
    private Button connectButton;

    private void Start()
    {
        connectButton.onClick.AddListener(() => InstanceFinder.ClientManager.StartConnection());
    }
}
