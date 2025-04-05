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
        GameObject.Find("Main Camera").name = "Sub Camera"; //Destroy‚·‚é‚Æ‰æ–Ê‚É"No cameras rendering"‚ª‰f‚Á‚Ä‚µ‚Ü‚¤
        GameObject.FindGameObjectWithTag("Player").name = "OldPlayer";     //Destroy‚·‚é‚ÆÁ‚¦‚½uŠÔ‚ª‰f‚Á‚Ä‚µ‚Ü‚¤
        SceneManager.LoadScene("SelectScene");
    }

    private void Update()
    {
        //•\¦Ø‘Ö
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isActive)  //•\¦‚ğk¬
            {
                howToPanel.SetActive(false);
                infoPanel.SetActive(true);
            }
            else  //•\¦‚ğŠg‘å
            {
                howToPanel.SetActive(true);
                infoPanel.SetActive(false);
            }

            isActive = !isActive;
        }
    }
}
