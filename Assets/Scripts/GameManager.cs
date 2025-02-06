using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class GameManager : MonoBehaviour
{
    [SerializeField] CubeManager cubeManager;
    [SerializeField] GameObject spotLight;
    [SerializeField] Camera camera;

    private Vector3 cameraPosition;
    private Vector3 goalPosition;
    private float distance;
    private float speed = 10f;
    private float progress = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = Camera.main;
        cameraPosition = camera.transform.position;
        goalPosition = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z - 5);
        distance = Vector3.Distance(cameraPosition, goalPosition);
    }

    // Update is called once per frame
    void Update()
    {
        spotLight.transform.position = new Vector3(cubeManager.transform.position.x, spotLight.transform.position.y, spotLight.transform.position.z);
        spotLight.SetActive(cubeManager.IsCubeSelected);

        //進行度を増減
        if (cubeManager.IsCubeSelected)
        {
            progress += Time.deltaTime * speed / distance;
        }
        else
        {
            progress -= Time.deltaTime * speed / distance;
        }

        //progressを0～1の範囲に制限
        progress = Mathf.Clamp01(progress);

        //カメラ移動
        camera.transform.position = Vector3.Lerp(cameraPosition, goalPosition, progress);

        // カメラの回転を設定
        float rotationAngle = Mathf.Lerp(0, 15, progress);
        camera.transform.rotation = Quaternion.Euler(rotationAngle, 0, 0);
    }
}
