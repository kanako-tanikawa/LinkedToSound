using UnityEngine;
using UnityEngine.Playables;

public class SignalReceiverWithJump : MonoBehaviour
{
    [SerializeField] private PlayableDirector timeline;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            timeline.Play();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            timeline.time = 0.0;
        }
    }

    public void SendSignal()
    {
        timeline.Pause();
    }
}
