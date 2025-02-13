using UnityEngine;

public class TerrainReverter : MonoBehaviour
{
    private Terrain terrain;                           // ������ �� Terrain
    public float revertSpeed;                   // ��������� �������� ������
    private float[,] originalHeights;                  // ������ ��� �������� �������� ������ �����
    private float[,] currentHeights;                   // ������ ��� ������� ����� Terrain
    private bool isReverting = false;                   // ����, ����� ����������� ������� ������
    private int checkInterval = 30;                     // �������� ��������� ��� � ��������� ������
    private int frameCounter = 0;                      // ������� ������ ��� �������� ������� ��������

    private void Start()
    {
        terrain = GetComponent<Terrain>();

        // ������������� �������� �����
        originalHeights = new float[terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution];
        currentHeights = new float[terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution];

        // ��������� ��������� ������
        originalHeights = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution);
        currentHeights = (float[,])originalHeights.Clone();
    }

    private void Update()
    {
        // �������� ��������� ��� � ��������� ������
        if (frameCounter > checkInterval)
        {
            CheckForChanges();
            if (isReverting)
            {
                RevertTerrainChanges();
            }
            frameCounter = 0;  // ���������� �������
        }
        else
        {
            frameCounter++;
        }
    }

    // ������� ��� �������� ��������� �� Terrain
    private void CheckForChanges()
    {
        // �������� ������� ������ � Terrain
        float[,] heights = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution);

        // ���������, ���� �� ��������� � ������� (�����������: �� ��������� ��� �������� �������)
        bool hasChanges = false;

        for (int x = 0; x < terrain.terrainData.heightmapResolution; x++)
        {
            for (int z = 0; z < terrain.terrainData.heightmapResolution; z++)
            {
                if (Mathf.Abs(heights[x, z] - currentHeights[x, z]) > 0.01f)  // ���������� ����������� ��� ���������
                {
                    hasChanges = true;
                    break;
                }
            }
            if (hasChanges) break;
        }

        // ���� ��������� ���� �������, ��������� currentHeights
        if (hasChanges)
        {
            currentHeights = (float[,])heights.Clone();
            // ��������� �����
            StartReverting();
        }
    }

    // �������� ������� ������
    public void StartReverting()
    {
        isReverting = true;
    }

    // ������������� ������� ������
    public void StopReverting()
    {
        isReverting = false;
    }

    // ������� ������ ���������
    private void RevertTerrainChanges()
    {
        // �������� ������� ������
        float[,] heights = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution);

        // ���������� ��������������� ������
        for (int x = 0; x < heights.GetLength(0); x++)
        {
            for (int z = 0; z < heights.GetLength(1); z++)
            {
                // ������������ ����� �������� � ��������� �������� (��������� �������� ������)
                heights[x, z] = Mathf.Lerp(heights[x, z], originalHeights[x, z], revertSpeed * Time.deltaTime);
            }
        }

        // ��������� ������ �������
        terrain.terrainData.SetHeights(0, 0, heights);

        // ���� ������ ���������� ������ � ��������, ������������� �����
        if (HasTerrainReturnedToOriginal(heights))
        {
            StopReverting();
        }
    }

    // ��������, ���������� �� ������ ��������������
    private bool HasTerrainReturnedToOriginal(float[,] heights)
    {
        for (int x = 0; x < heights.GetLength(0); x++)
        {
            for (int z = 0; z < heights.GetLength(1); z++)
            {
                if (Mathf.Abs(heights[x, z] - originalHeights[x, z]) > 0.001f)  // ���������� �����������
                {
                    return false;
                }
            }
        }
        return true;
    }
}
