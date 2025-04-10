using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayScenePanelManager : MonoBehaviour
{
    [SerializeField] GameObject loadingPanel;
    [SerializeField] GameObject howToPanel;
    [SerializeField] GameObject infoPanel;

    private bool isActive = false;

    public void OnClickBackSceneButton()
    {
        loadingPanel.SetActive(true);
        Destroy(GameObject.Find("SEManager"));
        GameObject.Find("Main Camera").name = "Sub Camera"; //Destroyすると画面に"No cameras rendering"が映ってしまう
        GameObject.FindGameObjectWithTag("Player").name = "OldPlayer";     //Destroyすると消えた瞬間が映ってしまう
        SceneManager.LoadScene("SelectScene");
    }

    private void Update()
    {
        //表示切替
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isActive)  //表示を縮小
            {
                howToPanel.SetActive(false);
                infoPanel.SetActive(true);
            }
            else  //表示を拡大
            {
                howToPanel.SetActive(true);
                infoPanel.SetActive(false);
            }

            isActive = !isActive;
        }
    }
}
