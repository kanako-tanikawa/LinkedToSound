using UnityEngine;

public class SignalReceiver : MonoBehaviour
{
    [SerializeField] CubeManager cubeManager;

    private void Start()
    {
        cubeManager = GetComponent<CubeManager>();
    }

    public void SendSignal()
    {
        cubeManager.IsCubeSelected = true;
    }
}
