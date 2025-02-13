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
        // ��������� �������� �������� � ��������� ��������� ���������� ��������� ����
        Vector3 terrainPos = hitPoint - terrain.transform.position; // ������� ����� ����� ������������ ��������

        float terrainWidth = terrain.terrainData.size.x;
        float terrainHeight = terrain.terrainData.size.z;
        int heightmapResolution = terrain.terrainData.heightmapResolution;

        // ����������� ������� ���������� � ������� ����� �����
        int x = Mathf.FloorToInt((terrainPos.x / terrainWidth) * heightmapResolution);
        int z = Mathf.FloorToInt((terrainPos.z / terrainHeight) * heightmapResolution);

        // ���������, ����� ������� �� �������� �� ������� ����� �����
        x = Mathf.Clamp(x, 0, heightmapResolution - 1);
        z = Mathf.Clamp(z, 0, heightmapResolution - 1);

        // ��������� ������ ���������� � �������� �������� ����� �����
        int deformationRadiusInIndices = Mathf.FloorToInt(deformationRadius / terrainWidth * heightmapResolution);

        // �������� ������ � ������� ����������
        float[,] heights = terrain.terrainData.GetHeights(x - deformationRadiusInIndices, z - deformationRadiusInIndices,
                                                           deformationRadiusInIndices * 2, deformationRadiusInIndices * 2);

        // ��������� ����������
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

        // ��������� ���������� ������ ������� � �������
        terrain.terrainData.SetHeights(x - deformationRadiusInIndices, z - deformationRadiusInIndices, heights);
    }
}
