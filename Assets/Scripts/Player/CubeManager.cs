using UnityEngine;

public class CubeManager : SelectableObject
{
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
        base.StartLink(speed);
    }

    public override void EndLink(float speed)   //�������Ȃ�
    {
        base.EndLink(speed);
    }
}