using FishNet.Object;
using FishNet.Object.Synchronizing;
using TMPro;
using UnityEngine;

public sealed class netGui002 : NetworkBehaviour
{
    [SyncVar(OnChange = nameof(UpdateTMP))]
    public string name;

    [SerializeField]
    private TMP_InputField newInputs;

    [SerializeField]
    private TextMeshProUGUI texty;

    public override void OnStartClient()
    {
        base.OnStartClient();

        this.transform.SetParent(GameObject.FindGameObjectWithTag("HomeSpawn").transform);

        if (!IsOwner)
        {

            this.GetComponent<TextMeshProUGUI>().color = new Color(100, 0, 0);
            UpdateName($"AT START ONLY REPORTS ClientOnly: {IsClientOnly}   ServerOnly: {IsServerOnly}   Client: {IsClient}   Server: {IsServer}   ID: {OwnerId}");
            newInputs.gameObject.SetActive(false);

        }

        else
        {
            this.GetComponent<TextMeshProUGUI>().color = new Color(0, 100, 0);
            UpdateName($"AT START ONLY REPORTS ClientOnly: {IsClientOnly}   ServerOnly: {IsServerOnly}   Client: {IsClient}   Server: {IsServer}   ID: {OwnerId}");
            newInputs.transform.SetParent(transform.parent.parent);
            newInputs.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
            newInputs.GetComponent<RectTransform>().anchorMin = new Vector2(0.7f, 0.9f);
            newInputs.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            newInputs.GetComponent<RectTransform>().position = new Vector2(0, 0);
            newInputs.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            newInputs.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        }
    }

    private void UpdateTMP(string oldValue, string newValue, bool isServer)
    {
            texty.text = newValue;
    }

    [ServerRpc]
    private void UpdateName(string newName)
    {
        name = newName;
    }

    public void Check()
    {
        if (!IsOwner) return;
        UpdateName(newInputs.text);
    }
}
