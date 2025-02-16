using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector timeline;
    private bool isPlayed = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) //‰¹Œ¹‚ð—¬‚·
        {
            if (isPlayed)
            {
                timeline.Stop();    //—¬‚µ‚Ä‚¢‚éó‘Ô‚ÅQ‚ð‰Ÿ‚·‚Æ’âŽ~
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
