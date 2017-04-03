using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    private Toggle debugInfoToggle;
    void Start()
    {
        this.debugInfoToggle = GameObject.Find("DebugInfoToggle").GetComponent<Toggle>();
        this.debugInfoToggle.onValueChanged.AddListener(delegate (bool isOn)
        {
            GlobalValue.Instance.DebugInfo = isOn;
        });
    }
}
