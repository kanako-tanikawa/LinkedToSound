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

    public override void LargeSize(float speed)
    {
        transform.localPosition = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), speed);
    }

    public override void SmallSize(float speed)
    {
        transform.localPosition = Vector3.MoveTowards(transform.position, new Vector3(-5, 0, 0), speed);
    }
}