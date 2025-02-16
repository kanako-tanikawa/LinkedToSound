using UnityEngine;
using UnityEngine.Playables;

public class SignalReceiverWithKeyInput : MonoBehaviour     //音を一音ずつ鳴らす
{
    [SerializeField] private PlayableDirector timeline;

    void Update()
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
