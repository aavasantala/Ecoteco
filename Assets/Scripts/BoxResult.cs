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
    }
    public void WrongBox()
    {
        audioRenderer.clip= wrongClip;
        audioRenderer.Play();
    }
}
