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
        if (selectableObject == null)   //�I���������ꂽ��
        {
            StopCoroutine(LinkedToSound());
        }
    }

    public void GetObject(SelectableObject selectableObject)
    {
        this.selectableObject = selectableObject;   //�I�����ꂽ�I�u�W�F�N�g�̎擾
    }

    public void SendSignal()    //MIDI�M���ɍ��킹�ČĂяo�����
    {
        if (selectableObject != null)
        {
            StartCoroutine(LinkedToSound());
        }
    }

    IEnumerator LinkedToSound() //MIDI�����ɍ��킹������
    {
        selectableObject.StartLink(waitTime);
        yield return new WaitForSeconds(waitTime);
        selectableObject.EndLink(waitTime);
        yield return new WaitForSeconds(waitTime);
    }
}
