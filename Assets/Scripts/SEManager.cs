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
        audioSource.PlayOneShot(selectSE);  //‘I‘ð‰¹
    }

    public void EnterSound()
    {
        audioSource.PlayOneShot(enterSE);   //Œˆ’è‰¹
    }
}
