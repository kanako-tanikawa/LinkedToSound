using UnityEngine;

public class CubeManager : MonoBehaviour
{

    private void Start()
    {
        transform.localScale = new Vector3(5, 5, 5);
    }

    public void SizeChange()
    {
        transform.localScale = new Vector3(10, 10, 10);
    }
}
