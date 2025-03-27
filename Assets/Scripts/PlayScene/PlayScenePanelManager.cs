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
        GameObject.Find("Main Camera").name = "Sub Camera";
        GameObject.FindGameObjectWithTag("Player").name = "OldPlayer";
        SceneManager.LoadScene("SelectScene");
    }

    private void Update()
    {
        //ï\é¶êÿë÷
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isActive)  //ï\é¶Çèkè¨
            {
                howToPanel.SetActive(false);
                infoPanel.SetActive(true);
            }
            else  //ï\é¶ÇägëÂ
            {
                howToPanel.SetActive(true);
                infoPanel.SetActive(false);
            }

            isActive = !isActive;
        }
    }
}
