using UnityEngine;

public class CubeManager : SelectableObject
{
    public override void RotateObject() //回転
    {
        base.RotateObject();
    }

    public override void ResetObject()  //リセット
    {
        base.ResetObject();
    }

    public override void StartLink(float speed) //大きくなる
    {
        base.StartLink(speed);
    }

    public override void EndLink(float speed)   //小さくなる
    {
        base.EndLink(speed);
    }
}