using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StageGenerator : MonoBehaviour
{
    [SerializeField]
    Grid m_grid;
    /// <summary>床のタイルマップ</summary>
    Tilemap m_floorTilemap;
    /// <summary>壁のタイルマップ</summary>
    Tilemap m_wallTilemap;

    // Start is called before the first frame update
    void Start()
    {
        Vector3Int position1z0 = new Vector3Int(-4, -4, 0);
        Tile floorTile = Resources.Load<Tile>("Pelette/Tiles/build_00");
        m_floorTilemap.SetTile(position1z0, floorTile);

        Vector3Int position1z1 = new Vector3Int(-4, -4, 1);
        Tile wallTile = Resources.Load<Tile>("Pelette/Tiles/roof_00");
        m_wallTilemap.SetTile(position1z1, wallTile);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
