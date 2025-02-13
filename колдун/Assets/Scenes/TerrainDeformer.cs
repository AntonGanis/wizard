using UnityEngine;

public class TerrainDeformer : MonoBehaviour
{
    public float deformationRadius;
    public float deformationStrength;
    public float raycastDistance;

    Camera playerCamera;
    Terrain terrain;

    private void Start()
    {
        playerCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                terrain = hit.collider.GetComponent<Terrain>();

                if (terrain != null)
                {
                    DeformTerrain(hit.point);
                }
            }
        }
    }

    void DeformTerrain(Vector3 hitPoint)
    {
        // Учитываем смещение террейна и вычисляем локальные координаты попадания луча
        Vector3 terrainPos = hitPoint - terrain.transform.position; // смещаем точку удара относительно террейна

        float terrainWidth = terrain.terrainData.size.x;
        float terrainHeight = terrain.terrainData.size.z;
        int heightmapResolution = terrain.terrainData.heightmapResolution;

        // Преобразуем мировые координаты в индексы карты высот
        int x = Mathf.FloorToInt((terrainPos.x / terrainWidth) * heightmapResolution);
        int z = Mathf.FloorToInt((terrainPos.z / terrainHeight) * heightmapResolution);

        // Проверяем, чтобы индексы не выходили за пределы карты высот
        x = Mathf.Clamp(x, 0, heightmapResolution - 1);
        z = Mathf.Clamp(z, 0, heightmapResolution - 1);

        // Вычисляем радиус деформации в терминах индексов карты высот
        int deformationRadiusInIndices = Mathf.FloorToInt(deformationRadius / terrainWidth * heightmapResolution);

        // Получаем высоты в области деформации
        float[,] heights = terrain.terrainData.GetHeights(x - deformationRadiusInIndices, z - deformationRadiusInIndices,
                                                           deformationRadiusInIndices * 2, deformationRadiusInIndices * 2);

        // Применяем деформацию
        for (int i = 0; i < heights.GetLength(0); i++)
        {
            for (int j = 0; j < heights.GetLength(1); j++)
            {
                float dist = Vector2.Distance(new Vector2(i, j), new Vector2(heights.GetLength(0) / 2, heights.GetLength(1) / 2));
                if (dist < deformationRadiusInIndices)
                {
                    heights[i, j] += deformationStrength * (1f - dist / deformationRadiusInIndices);
                }
            }
        }

        // Применяем измененные высоты обратно в террейн
        terrain.terrainData.SetHeights(x - deformationRadiusInIndices, z - deformationRadiusInIndices, heights);
    }
}
