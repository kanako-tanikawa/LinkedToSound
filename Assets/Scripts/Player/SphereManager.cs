using UnityEngine;

public class SphereManager : SelectableObject
{
    public override void RotateObject() //回転
    {
        base.RotateObject();
    }

    public override void ResetObject()  //リセット
    {
        base.ResetObject();
    }

    public override void StartLink(float speed) //色変更
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    public override void EndLink(float speed)   //色リセット
    {
        GetComponent<Renderer>().material.color = Color.gray;
    }
}