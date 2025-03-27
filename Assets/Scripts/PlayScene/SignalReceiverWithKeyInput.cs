using UnityEngine;
using UnityEngine.Playables;

public class SignalReceiverWithKeyInput : MonoBehaviour     //�����ꉹ���炷
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
            timeline.Play();    //�炷
        }

        if (Input.GetKeyDown(KeyCode.Backspace))    //�ŏ��ɖ߂�
        {
            timeline.Stop();
            timeline.Play();
        }
    }

    public void SendSignal()
    {
        timeline.Pause();   //MIDI�M�����󂯎������r����~
    }
}
