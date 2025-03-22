using UnityEngine;

public class SelectScenePanelManager : MonoBehaviour
{
    [SerializeField] GameObject howToPlayPanel; //操作方法(拡大)
    [SerializeField] GameObject infoPanel;  //操作方法(縮小)
    private bool isActive = true; //現在の状態

    //Panel上のボタン操作
    public void OnClickDeleteButton()   //表示を縮小
    {
        howToPlayPanel.SetActive(false);
        infoPanel.SetActive(true);
        isActive = false;
    }

    private void Update()
    {
        //表示切替
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isActive)  //表示を縮小
            {
                howToPlayPanel.SetActive(false);
                infoPanel.SetActive(true);
            }
            else  //表示を拡大
            {
                howToPlayPanel.SetActive(true);
                infoPanel.SetActive(false);
            }

            isActive = !isActive;
        }
    }
}
