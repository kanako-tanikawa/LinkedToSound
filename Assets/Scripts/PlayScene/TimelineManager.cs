using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector timeline;
    private bool isPlayed = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) //音源を流す
        {
            if (isPlayed)
            {
                timeline.Stop();    //流している状態でQを押すと停止
                isPlayed = false;
            }
            else
            {
                timeline.Play();
                isPlayed = true;
            }
        }

        if(timeline.time >= timeline.duration)
        {
            isPlayed = false;
        }
    }
}
