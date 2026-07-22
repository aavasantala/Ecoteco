using UnityEngine;

public class PauseScript : MonoBehaviour
{

    public GameObject panel;
    private void Start()
    {
        panel = Singleton.Instance.pausePanel;
    }
    private void OnEnable()
    {
        
    }
    public void OnPauseClick()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
    }

}
