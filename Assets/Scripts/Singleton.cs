using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }

    public itemChooser itemChooser {  get; private set; }

    public BoxResult boxResult { get; private set; }

    public AudioSource audioSource { get; private set; }

    public TMP_Text text;
    public TMP_Text textBest;

    public GameObject pausePanel;
    public int score;
    public int bestScore;

    

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
        
        pausePanel.SetActive(false);


    }

    private void Start()
    {
        itemChooser.ChooseItem();
    }

    public void OnValueChanged()
    {
        if(score.ToString()==null|| score.ToString() == "")
        {
            score = 0;
        }
        text.text = "Current score : " + score;
        textBest.text = "Best score : " + bestScore;
    }

}
