using UnityEngine;

public class SylinderManager : SelectableObject
{
    public override void RotateObject() //回転
    {
        base.RotateObject();
    }

    public override void ResetObject()  //リセット
    {
        base.ResetObject();
    }

    public override void StartLink(float speed) //ジャンプ
    {
        transform.localPosition = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), speed);
    }

    public override void EndLink(float speed)   //着地
    {
        transform.localPosition = Vector3.MoveTowards(transform.position, new Vector3(-5, 0, 0), speed);
    }
}