using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector timeline;
    private bool isPlayed = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (isPlayed)
            {
                timeline.Stop();
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
