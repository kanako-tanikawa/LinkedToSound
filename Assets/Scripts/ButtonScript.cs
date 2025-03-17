using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] GameObject LoadingPanel;

    public void OnClick()
    {
        LoadingPanel.SetActive(true);
        Destroy(GameObject.Find("SEManager"));
        GameObject.Find("Main Camera").name = "Sub Camera";
        GameObject.FindGameObjectWithTag("Player").name = "OldPlayer";
        SceneManager.LoadScene("SelectScene");
    }
}
