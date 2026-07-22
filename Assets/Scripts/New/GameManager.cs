using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int totalItems = 7;
    private int recycledCount = 0;

    void Awake()
    {
        Instance = this;
    }

    public void OnItemRecycled()
    {
        recycledCount++;
        Debug.Log($"Recycled {recycledCount}/{totalItems}");

        if (recycledCount >= totalItems)
        {
            Invoke(nameof(RestartGame), 0.5f);
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}