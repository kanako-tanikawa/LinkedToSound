using UnityEngine;

public class SelectScenePanelManager : MonoBehaviour
{
    [SerializeField] GameObject howToPlayPanel; //������@(�g��)
    [SerializeField] GameObject infoPanel;  //������@(�k��)
    private bool isActive = true; //���݂̏��

    //Panel��̃{�^������
    public void OnClickDeleteButton()   //�\�����k��
    {
        howToPlayPanel.SetActive(false);
        infoPanel.SetActive(true);
        isActive = false;
    }

    private void Update()
    {
        //�\���ؑ�
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isActive)  //�\�����k��
            {
                howToPlayPanel.SetActive(false);
                infoPanel.SetActive(true);
            }
            else  //�\�����g��
            {
                howToPlayPanel.SetActive(true);
                infoPanel.SetActive(false);
            }

            isActive = !isActive;
        }
    }
}
