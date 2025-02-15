using UnityEngine;

public class SphereManager : SelectableObject
{
    public override void RotateObject() //‰ñ“]
    {
        base.RotateObject();
    }

    public override void ResetObject()  //ƒŠƒZƒbƒg
    {
        base.ResetObject();
    }

    public override void LargeSize(float speed)
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    public override void SmallSize(float speed)
    {
        GetComponent<Renderer>().material.color = Color.gray;
    }
}