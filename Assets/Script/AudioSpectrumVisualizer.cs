using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSpectrumVisualizer : MonoBehaviour
{
    public FFTWindow fftWindow = FFTWindow.BlackmanHarris; // FFT窗口类型
    public int spectrumSize = 1024; // 频谱数据的大小
    public float scale = 100f; // 缩放因子

    public GameObject cubePrefab; // 立方体预制体
    private AudioSource audioSource;
    private float[] spectrumData;

    private GameObject[] cubes; // 保存已创建的立方体

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spectrumData = new float[spectrumSize];

        // 创建立方体数组，长度与频谱数据大小相同
        cubes = new GameObject[spectrumSize];

        // 在开始时生成所有立方体
        for (int i = 0; i < spectrumSize; i++)
        {
            cubes[i] = Instantiate(cubePrefab, transform);
        }
    }

    void Update()
    {
        // 获取音频频谱数据
        audioSource.GetSpectrumData(spectrumData, 0, fftWindow);

        // 可视化频谱数据
        VisualizeSpectrum();
    }

    void VisualizeSpectrum()
    {
        for (int i = 0; i < spectrumSize; i++)
        {
            // 将频谱数据转换为可视化效果，例如通过调整立方体的高度
            float spectrumValue = spectrumData[i] * scale;

            // 更新立方体的位置和大小
            cubes[i].transform.localPosition = new Vector3(i * 0.5f, spectrumValue * 0.1f, 0f);
            cubes[i].transform.localScale = new Vector3(0.1f, spectrumValue, 1f);
        }
    }
}
