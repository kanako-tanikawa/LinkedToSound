using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public bool IsCubeSelected = false;
    [SerializeField] GameManager gameManager; // GameManager�ւ̎Q��

    private void OnMouseDown() // Cube���N���b�N���ꂽ�Ƃ�
    {
        IsCubeSelected = !IsCubeSelected; // �I����Ԃ�؂�ւ���
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsCubeSelected = false;
        }

        //Cube�̉�]
        if (IsCubeSelected)
        {
            transform.Rotate(new Vector3(0, 45 * Time.deltaTime, 0));
        }
        else
        {
            transform.rotation = Quaternion.identity; // Cube�����̌����ɖ߂�
        }
    }
}