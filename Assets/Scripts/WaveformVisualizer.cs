using UnityEngine;

public class WaveformVisualizer : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] LineRenderer lineRenderer;

    public int sampleSize = 512;
    public float width = 10f;
    public float amplitude = 5f;

    private float[] samples;    //波形データを格納する配列

    private void Start()
    {
        samples = new float[sampleSize];    //初期化
        lineRenderer.positionCount = sampleSize;
    }

    private void Update()
    {
        if (audioSource.isPlaying)
        {
            audioSource.GetOutputData(samples, 0);  //データ取得(振幅データ)
        }

        for(int i = 0; i < sampleSize; i++)
        {
            float x = i / (float) sampleSize * width - (width / 2);
            float y = amplitude * samples[i];

            lineRenderer.SetPosition(i, new Vector3(x, y, 0));  //描画
        }
    }

}
