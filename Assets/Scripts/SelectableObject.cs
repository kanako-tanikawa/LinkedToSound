using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    public bool IsSelected {  get; private set; }

    private void OnMouseDown()
    {
        IsSelected = true;
        GameManager.Instance.HandleObjectSelection(this, IsSelected);   //マウスが押されたオブジェクトを選択状態に
    }

    public virtual void RotateObject()
    {
        transform.Rotate(new Vector3(0, 45 * Time.deltaTime, 0)); //オブジェクトの回転
    }

    public virtual void ResetObject()
    {
        transform.rotation = Quaternion.identity; // オブジェクトを元に戻す
    }

    public void SetSelected(bool selected)
    {
        IsSelected = selected;
    }
}
