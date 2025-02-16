using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }    //選択状態の管理

    [SerializeField] SignalReceiver signalReceiver;
    [SerializeField] GameObject spotLight;
    [SerializeField] Camera camera;
    private SelectableObject selectableObject;  //選択状態のオブジェクト

    private Vector3 cameraPosition; //カメラの初期位置
    private Vector3 goalPosition;   //カメラの終着点
    private float distance;         //初期位置と終着点の距離
    private float speed = 10f;      //カメラの移動スピード
    private float progress = 0.0f;  //カメラの進行度合い

    [SerializeField] SEManager seManager;

    private void Awake()
    {
        Instance = this;    //シングルトンのインスタンス設定
    }

    void Start()
    {
        cameraPosition = camera.transform.position;
        goalPosition = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z - 5);
        distance = Vector3.Distance(cameraPosition, goalPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            DeselectObject();   //Escapeキーで選択解除
        }

        if(selectableObject != null)    //選択状態であれば
        {
            spotLight.transform.position = new Vector3(selectableObject.transform.position.x, spotLight.transform.position.y, spotLight.transform.position.z);
            spotLight.SetActive(true);  //スポットライトの点灯

            selectableObject.RotateObject();    //選択状態のオブジェクトを回転

            signalReceiver.GetObject(selectableObject);

            progress = Mathf.Clamp01(progress + Time.deltaTime * speed / distance);
        }
        else  //選択状態になければ
        {
            spotLight.SetActive(false); //スポットライトの消灯

            progress = Mathf.Clamp01(progress - Time.deltaTime * speed / distance);
        }

        //カメラ移動
        camera.transform.position = Vector3.Lerp(cameraPosition, goalPosition, progress);

        // カメラの回転
        float rotationAngle = Mathf.Lerp(0, 15, progress);
        camera.transform.rotation = Quaternion.Euler(rotationAngle, 0, 0);

        if(selectableObject != null && Input.GetKeyDown(KeyCode.Return))    //オブジェクトを決定したらシーン移動
        {
            DontDestroyOnLoad(seManager);
            seManager.EnterSound(); //SE

            DontDestroyOnLoad(camera);
            DontDestroyOnLoad(selectableObject);
            SceneManager.LoadScene("PlayScene");
        }
    }

    public void HandleObjectSelection(SelectableObject objectToSelect, bool IsSelected)
    {
        if (IsSelected) //選択されたら
        {
            if(selectableObject != null && !selectableObject.IsSelected)    //既に選択済みのがないか
            {
                selectableObject.SetSelected(false);
            }

            seManager.SelectSound();    //SE
            selectableObject = objectToSelect;
            goalPosition = new Vector3(objectToSelect.transform.position.x, objectToSelect.transform.position.y + 2, objectToSelect.transform.position.z - 5);
            distance = Vector3.Distance(cameraPosition, goalPosition);
        }
        else  //選択解除のとき
        {
            DeselectObject();
        }
    }

    private void DeselectObject()   //選択解除
    {
        if(selectableObject != null)
        {
            selectableObject.ResetObject();
        }
        selectableObject = null;
        signalReceiver.GetObject(selectableObject);
    }
}
