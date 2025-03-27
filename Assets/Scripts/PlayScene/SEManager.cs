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
        audioSource.PlayOneShot(selectSE);  //�I����
    }

    public void EnterSound()
    {
        audioSource.PlayOneShot(enterSE);   //���艹
    }
}
