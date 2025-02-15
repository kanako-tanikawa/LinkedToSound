using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class SignalReceiver : MonoBehaviour
{
    private SelectableObject selectableObject;
    public float tempo = 120;
    private float waitTime = 0;

    private void Start()
    {
        waitTime = 60 / (tempo * 2);
    }

    public void GetObject(SelectableObject selectableObject)
    {
        this.selectableObject = selectableObject;
    }

    public void SendSignal()
    {
        if (selectableObject != null)
        {
            StartCoroutine(LinkedToSound());
        }
    }

    IEnumerator LinkedToSound() //âπÇ…çáÇÌÇπÇƒìÆçÏ
    {
        selectableObject.LargeSize(waitTime);
        yield return new WaitForSeconds(waitTime);
        selectableObject.SmallSize(waitTime);
    }
}
