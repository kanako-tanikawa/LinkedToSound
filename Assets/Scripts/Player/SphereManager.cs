using UnityEngine;

public class SphereManager : SelectableObject
{
    public override void RotateObject() //��]
    {
        base.RotateObject();
    }

    public override void ResetObject()  //���Z�b�g
    {
        base.ResetObject();
    }

    public override void StartLink(float speed) //�F�ύX
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    public override void EndLink(float speed)   //�F���Z�b�g
    {
        GetComponent<Renderer>().material.color = Color.gray;
    }
}