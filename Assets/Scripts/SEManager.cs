using UnityEngine;

public class SEManager : MonoBehaviour
{
    [SerializeField] AudioClip selectSE;
    [SerializeField] AudioClip enterSE;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SelectSound()
    {
        audioSource.PlayOneShot(selectSE);  //選択音
    }

    public void EnterSound()
    {
        audioSource.PlayOneShot(enterSE);   //決定音
    }
}
