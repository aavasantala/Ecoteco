using UnityEngine;

public class BoxResult : MonoBehaviour
{

    public AudioClip correctClip;
    public AudioClip wrongClip;
    
    private AudioSource audioRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        audioRenderer=Singleton.Instance.audioSource;
    }
    public void CorrectBox()
    {
        audioRenderer.clip = correctClip;
        audioRenderer.Play();
        Singleton.Instance.score += 1;
        
        if(Singleton.Instance.bestScore < Singleton.Instance.score)
        {
            Singleton.Instance.bestScore = Singleton.Instance.score;
        }
        Singleton.Instance.OnValueChanged();
    }
    public void WrongBox()
    {
        audioRenderer.clip= wrongClip;
        audioRenderer.Play();

        Singleton.Instance.score = 0;
        Singleton.Instance.OnValueChanged();
    }
}
