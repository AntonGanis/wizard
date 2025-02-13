using UnityEngine;

public class TerrainReverter : MonoBehaviour
{
    private Terrain terrain;                           // Ссылка на Terrain
    public float revertSpeed;                   // Увеличена скорость отката
    private float[,] originalHeights;                  // Массив для хранения исходных данных высот
    private float[,] currentHeights;                   // Массив для текущих высот Terrain
    private bool isReverting = false;                   // Флаг, чтобы отслеживать процесс отката
    private int checkInterval = 30;                     // Проверка изменений раз в несколько кадров
    private int frameCounter = 0;                      // Счётчик кадров для контроля частоты проверок

    private void Start()
    {
        terrain = GetComponent<Terrain>();

        // Инициализация массивов высот
        originalHeights = new float[terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution];
        currentHeights = new float[terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution];

        // Сохраняем начальные высоты
        originalHeights = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution);
        currentHeights = (float[,])originalHeights.Clone();
    }

    private void Update()
    {
        // Проверка изменений раз в несколько кадров
        if (frameCounter > checkInterval)
        {
            CheckForChanges();
            if (isReverting)
            {
                RevertTerrainChanges();
            }
            frameCounter = 0;  // Сбрасываем счетчик
        }
        else
        {
            frameCounter++;
        }
    }

    // Функция для проверки изменений на Terrain
    private void CheckForChanges()
    {
        // Получаем текущие высоты с Terrain
        float[,] heights = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution);

        // Проверяем, были ли изменения в высотах (оптимизация: не проверяем все элементы массива)
        bool hasChanges = false;

        for (int x = 0; x < terrain.terrainData.heightmapResolution; x++)
        {
            for (int z = 0; z < terrain.terrainData.heightmapResolution; z++)
            {
                if (Mathf.Abs(heights[x, z] - currentHeights[x, z]) > 0.01f)  // Допустимая погрешность для изменений
                {
                    hasChanges = true;
                    break;
                }
            }
            if (hasChanges) break;
        }

        // Если изменения были найдены, обновляем currentHeights
        if (hasChanges)
        {
            currentHeights = (float[,])heights.Clone();
            // Запускаем откат
            StartReverting();
        }
    }

    // Включаем процесс отката
    public void StartReverting()
    {
        isReverting = true;
    }

    // Останавливаем процесс отката
    public void StopReverting()
    {
        isReverting = false;
    }

    // Функция отката изменений
    private void RevertTerrainChanges()
    {
        // Получаем текущие высоты
        float[,] heights = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution);

        // Постепенно восстанавливаем высоты
        for (int x = 0; x < heights.GetLength(0); x++)
        {
            for (int z = 0; z < heights.GetLength(1); z++)
            {
                // Интерполяция между текущими и исходными высотами (увеличена скорость отката)
                heights[x, z] = Mathf.Lerp(heights[x, z], originalHeights[x, z], revertSpeed * Time.deltaTime);
            }
        }

        // Применяем высоты обратно
        terrain.terrainData.SetHeights(0, 0, heights);

        // Если высоты достаточно близки к исходным, останавливаем откат
        if (HasTerrainReturnedToOriginal(heights))
        {
            StopReverting();
        }
    }

    // Проверка, достаточно ли высоты восстановились
    private bool HasTerrainReturnedToOriginal(float[,] heights)
    {
        for (int x = 0; x < heights.GetLength(0); x++)
        {
            for (int z = 0; z < heights.GetLength(1); z++)
            {
                if (Mathf.Abs(heights[x, z] - originalHeights[x, z]) > 0.001f)  // Допустимая погрешность
                {
                    return false;
                }
            }
        }
        return true;
    }
}
