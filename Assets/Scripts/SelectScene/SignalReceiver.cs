using UnityEngine;
using System.Collections;

public class SignalReceiver : MonoBehaviour
{
    private SelectableObject selectableObject;
    public float tempo = 120;
    private float waitTime = 0;

    private void Start()
    {
        waitTime = 60 / (tempo * 2);
    }

    private void Update()
    {
        if (selectableObject == null)   //選択解除されたら
        {
            StopCoroutine(LinkedToSound());
        }
    }

    public void GetObject(SelectableObject selectableObject)
    {
        this.selectableObject = selectableObject;   //選択されたオブジェクトの取得
    }

    public void SendSignal()    //MIDI信号に合わせて呼び出される
    {
        if (selectableObject != null)
        {
            StartCoroutine(LinkedToSound());
        }
    }

    IEnumerator LinkedToSound() //MIDI音源に合わせた動作
    {
        selectableObject.StartLink(waitTime);
        yield return new WaitForSeconds(waitTime);
        selectableObject.EndLink(waitTime);
        yield return new WaitForSeconds(waitTime);
    }
}
