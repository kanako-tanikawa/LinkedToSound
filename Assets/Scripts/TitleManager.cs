using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void OnClickButton()
    {
        SceneManager.LoadScene("SelectScene");
    }
}
