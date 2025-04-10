using UnityEngine;
using UnityEngine.Playables;

public class SignalReceiverWithKeyInput : MonoBehaviour     //音を一音ずつ鳴らす
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
            timeline.Play();    //鳴らす
        }

        if (Input.GetKeyDown(KeyCode.Backspace))    //最初に戻す
        {
            timeline.Stop();
            timeline.Play();
        }
    }

    public void SendSignal()
    {
        timeline.Pause();   //MIDI信号を受け取ったら途中停止
    }
}
