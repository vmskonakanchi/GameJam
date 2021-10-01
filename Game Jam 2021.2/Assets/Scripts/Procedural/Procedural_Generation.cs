using UnityEngine;
using UnityEngine.Tilemaps;
public class Procedural_Generation : MonoBehaviour
{
    [SerializeField] Tilemap dirtTile, stoneTile, grassTile;
    [SerializeField] Tile dirt, stone, grass;
    [SerializeField] private int height;
    [SerializeField] private int width;
    [SerializeField] int maxStonexdist;
    [SerializeField] int minStoneydist;
    private void Start()
    {
        Startgeneration();
    }
    private void Startgeneration()
    {
        for (int x = 0; x < width; x++)
        {
            int minheight = height - 1;
            int maxheight = height + 2;
            height = Random.Range(minheight, maxheight);
            int minstoneSpawn = height - maxStonexdist;
            int maxstoneSpwan = height + minStoneydist;
            int totalStoneDist = Random.Range(minstoneSpawn, maxstoneSpwan);
            for (int y = 0; y < height; y++)
            {
                if (y < totalStoneDist)
                {
                    GenerateTile(stoneTile, stone, x, y);
                }
                else
                {
                    GenerateTile(dirtTile, dirt, x, y);
                }
            }
            GenerateTile(grassTile, grass, x, height);
        }
    }
    private void GenerateTile(Tilemap tilemap, Tile tile, int width, int height)
    {
        if (tilemap.gameObject.GetComponent<TilemapCollider2D>() == null)
        {
            tilemap.gameObject.AddComponent<TilemapCollider2D>();
        }
        tilemap.SetTile(new Vector3Int(width, height, 0), tile);
    }
}