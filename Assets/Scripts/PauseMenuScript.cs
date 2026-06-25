using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject panel;
    void Start()
    {
        panel = Singleton.Instance.pausePanel;
    }

    public void OnResumeClick()
    {
        Time.timeScale = 1f;
        panel.SetActive(false);
    }

    public void OnQuitClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
}
