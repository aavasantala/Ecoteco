using UnityEngine;

public class PauseScript : MonoBehaviour
{

    public GameObject panel;

    private void OnEnable()
    {
        panel = Singleton.Instance.pausePanel;
    }
    public void OnPauseClick()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
    }

}
