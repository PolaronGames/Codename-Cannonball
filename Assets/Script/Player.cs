using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{

    // Player data
    Transform position;
    public float speed;
    Rigidbody2D Ship;

    // World data
    Tilemap tilemap;
    WorldInfo info;
    float dockingDistance;
    float[,] heightmap;
    int detailLevel;
    int offset;
    float sandHeight;
    float grassHeight;
    float rockHeight;
    float tileWidth;

    // Tile types
    const int WATER = 0;
    const int SAND = 1;
    const int GRASS = 2;
    const int ROCK = 3;
    const int EMPTY = -1;

    // Use this for initialization
    void Start()
    {
        position = this.GetComponentsInParent<Transform>()[1];
        Ship = this.GetComponentsInParent<Rigidbody2D>()[0];
        info = this.GetComponentsInParent<WorldInfo>()[0];
        tileWidth = info.water.rect.width/100.0f;
        sandHeight = info.sandHeight;
        grassHeight = info.grassHeight;
        rockHeight = info.rockHeight;
        detailLevel = info.detailLevel;
        dockingDistance = info.dockingDistance;
        detailLevel = info.detailLevel;
        offset = (int)(Math.Pow(2, detailLevel) + 1) / 2;
        tilemap = info.GetComponentsInChildren<Tilemap>()[0];
        heightmap = info.heightmap;
    }

    Vector3Int WorldToCell()
    {
        int x = (int)(position.position.x / tileWidth) + 1;
        int y = (int)(position.position.y / tileWidth) + 1;
        return new Vector3Int(x, y, 0);
    }

    int GetTileType()
    {
        Tile tile = (Tile)tilemap.GetTile(WorldToCell());
        String name = tile.name;

        switch (name)
        {
            case "water":
                return WATER;
            case "sand":
                return SAND;
            case "grass":
                return GRASS;
            case "rock":
                return ROCK;
        }
        return EMPTY;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int TileIndex;
        float elevation;
        Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);

        // Movement
        if (Input.GetKey("right"))
        {
            velocity += new Vector3(speed, 0.0f, 0.0f);
        }
        if (Input.GetKey("left"))
        {
            velocity += new Vector3(-speed, 0.0f, 0.0f);
        }
        if (Input.GetKey("up"))
        {
            velocity += new Vector3(0.0f, speed, 0.0f);
        }
        if (Input.GetKey("down"))
        {
            velocity += new Vector3(0.0f, -speed, 0.0f);
        }
        Ship.velocity = velocity;

        // Docking    
        TileIndex = WorldToCell();
        elevation = heightmap[TileIndex.x + offset, TileIndex.y + offset];
        if (elevation >= 1.0f / dockingDistance)
        {
            Debug.Log("Port Menu Available");
        }

        // Collision
        if(elevation >= sandHeight)
        {
            //position.Translate(-step);
        }
    }
}
