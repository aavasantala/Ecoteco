using UnityEngine;
using UnityEngine.UIElements;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }

    public itemChooser itemChooser {  get; private set; }

    public BoxResult boxResult { get; private set; }

    public AudioSource audioSource { get; private set; }

    public GameObject pausePanel;

    

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        itemChooser = GetComponentInChildren<itemChooser>();
        boxResult = GetComponentInChildren<BoxResult>();
        audioSource = GetComponentInChildren<AudioSource>();

        

    }

    private void Start()
    {
        itemChooser.ChooseItem();
    }

}
