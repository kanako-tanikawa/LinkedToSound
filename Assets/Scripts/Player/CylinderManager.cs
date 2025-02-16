using UnityEngine;

public class SylinderManager : SelectableObject
{
    public override void RotateObject() //��]
    {
        base.RotateObject();
    }

    public override void ResetObject()  //���Z�b�g
    {
        base.ResetObject();
    }

    public override void StartLink(float speed) //�W�����v
    {
        transform.localPosition = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), speed);
    }

    public override void EndLink(float speed)   //���n
    {
        transform.localPosition = Vector3.MoveTowards(transform.position, new Vector3(-5, 0, 0), speed);
    }
}