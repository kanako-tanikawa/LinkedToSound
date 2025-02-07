using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    public bool IsSelected {  get; private set; }

    private void OnMouseDown()
    {
        IsSelected = true;
        GameManager.Instance.HandleObjectSelection(this, IsSelected);   //�}�E�X�������ꂽ�I�u�W�F�N�g��I����Ԃ�
    }

    public virtual void RotateObject()
    {
        transform.Rotate(new Vector3(0, 45 * Time.deltaTime, 0)); //�I�u�W�F�N�g�̉�]
    }

    public virtual void ResetObject()
    {
        transform.rotation = Quaternion.identity; // �I�u�W�F�N�g�����ɖ߂�
    }

    public void SetSelected(bool selected)
    {
        IsSelected = selected;
    }
}
