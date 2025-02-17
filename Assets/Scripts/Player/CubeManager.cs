using UnityEngine;

public class CubeManager : SelectableObject
{
    private Vector3 goalScale = new Vector3(2, 2, 2);   //�g��T�C�Y
    private Vector3 startScale;  //�k���T�C�Y

    public override void RotateObject() //��]
    {
        base.RotateObject();
    }

    public override void ResetObject()  //���Z�b�g
    {
        base.ResetObject();
    }

    public override void StartLink(float speed) //�傫���Ȃ�
    {
        startScale = transform.localScale;
        transform.localScale = Vector3.MoveTowards(transform.localScale, goalScale, speed);
    }

    public override void EndLink(float speed)   //�������Ȃ�
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, startScale, speed);
    }
}