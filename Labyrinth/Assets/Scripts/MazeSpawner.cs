using UnityEngine;
using System;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] private Cell CellPrefab;
    [SerializeField] private GameObject trapZonePrefab;
    [SerializeField] private GameObject endZonePrefab;
    [SerializeField] private GameObject playerPrefab;
    public Vector3 CellSize { get; private set; }
     
    private PlayerControl player;
    


    public Maze maze { get; private set; }

    private void Start()
    {
        CellSize = new Vector3(5, 0, 5);
        MazeGenerator generator = new MazeGenerator();
        maze = generator.GenerateMaze();

        for (int x = 0; x < maze.cells.GetLength(0); x++)
        {
            for (int y = 0; y < maze.cells.GetLength(1); y++)
            {
                Cell c = Instantiate(CellPrefab, new Vector3(x * CellSize.x, y * CellSize.y, y * CellSize.z), Quaternion.identity);

                c.wallLeft.SetActive(maze.cells[x, y].WallLeft);
                c.wallBottom.SetActive(maze.cells[x, y].WallBottom);
                c.floor.SetActive(maze.cells[x, y].Floor);

                if (c.transform.position == new Vector3(maze.finishPosition.x * 5, 0, maze.finishPosition.y * 5))
                {
                    c.floor.SetActive(false);
                    GameObject end = Instantiate(endZonePrefab, c.floor.transform.position, c.floor.transform.rotation);
                }

                int random = UnityEngine.Random.Range(0, 11);
                if (random > 9)
                {
                    c.floor.SetActive(false);
                    GameObject trap = Instantiate(trapZonePrefab, c.floor.transform.position, c.floor.transform.rotation);
                    if (trap.transform.position.x > 65 || trap.transform.position.z > 65)
                    {
                        Destroy(trap);
                    }
                } 
            }
        }



        //SetPlayersPath();

    }

    /*public void SetPlayersPath()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        player.PlayerStart(hintFinder.FindPath());
    }*/
}