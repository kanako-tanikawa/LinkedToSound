using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public bool IsCubeSelected = false;
    [SerializeField] GameManager gameManager; // GameManagerへの参照

    private void OnMouseDown() // Cubeがクリックされたとき
    {
        IsCubeSelected = !IsCubeSelected; // 選択状態を切り替える
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsCubeSelected = false;
        }

        //Cubeの回転
        if (IsCubeSelected)
        {
            transform.Rotate(new Vector3(0, 45 * Time.deltaTime, 0));
        }
        else
        {
            transform.rotation = Quaternion.identity; // Cubeを元の向きに戻す
        }
    }
}