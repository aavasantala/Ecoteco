using UnityEditor;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuScript : MonoBehaviour 
{
    void OnEnable() => GetComponent<PanelRenderer>().RegisterUIReloadCallback(OnUIReload);
    void OnDisable() => GetComponent<PanelRenderer>().UnregisterUIReloadCallback(OnUIReload);

    void OnUIReload(PanelRenderer pr, VisualElement root)
    {
        Button startButton = root.Q<Button>("button");
        startButton.RegisterCallback<MouseUpEvent>((evt) => StartButtonClicked());
        
    }

    void StartButtonClicked()
    {
        SceneManager.LoadScene("Level1");
    }
}
