using UnityEngine;

public class CubeManager : SelectableObject
{
    private Vector3 goalScale = new Vector3(2, 2, 2);   //拡大サイズ
    private Vector3 startScale;  //縮小サイズ

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
        startScale = transform.localScale;
        transform.localScale = Vector3.MoveTowards(transform.localScale, goalScale, speed);
    }

    public override void EndLink(float speed)   //小さくなる
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, startScale, speed);
    }
}