using UnityEngine;
using UnityEngine.Playables;

public class SignalReceiverWithKeyInput : MonoBehaviour     //‰¹‚ğˆê‰¹‚¸‚Â–Â‚ç‚·
{
    [SerializeField] private PlayableDirector timeline;

    private void Start()
    {
        timeline.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            timeline.Play();    //–Â‚ç‚·
        }

        if (Input.GetKeyDown(KeyCode.Backspace))    //Å‰‚É–ß‚·
        {
            timeline.Stop();
            timeline.Play();
        }
    }

    public void SendSignal()
    {
        timeline.Pause();   //MIDIM†‚ğó‚¯æ‚Á‚½‚ç“r’†’â~
    }
}
