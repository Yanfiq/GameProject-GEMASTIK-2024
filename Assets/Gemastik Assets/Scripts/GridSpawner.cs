using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public static GridSpawner Instance;

    void Awake()
    {
        Instance = this;
    }

    [SerializeField] Vector2Int size;
    [SerializeField] float gridSize;

    [SerializeField] Transform gridParent;
    [SerializeField] GameObject gridPrefab;

    Node[,] grid;

    void Start()
    {
        //GenerateGrid();
    }

    void GenerateGrid()
    {
        if (size == Vector2.zero) return;
        grid = new Node[size.x, size.y];
        Vector3 worldBottomLeft = transform.position - Vector3.right * (size.x-1) / 2 - Vector3.forward * (size.y-1) / 2;

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                grid[x, y] = Instantiate(gridPrefab, gridParent).GetComponent<Node>();
                grid[x, y].transform.position = worldBottomLeft + Vector3.right * x + Vector3.forward * y;
            }
        }

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                //grid[x, y].neighbours = GetNeighbours();
            }
        }
    }

    public List<Node> GetNeighbours()
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = size.x + x;
                int checkY = size.y + y;

                if (checkX >= 0 && checkX < size.x && checkY >= 0 && checkY < size.y)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }
}
