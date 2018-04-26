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
    Animator animator;

    // World data
    public Tilemap tilemap;
    int n;
    WorldInfo info;
    int dockingDistance;
    float[,] heightmap;
    int detailLevel;
    int offset;
    float sandHeight;
    float grassHeight;
    float rockHeight;
    float tileWidth;
    public int xBlocks, yBlocks;

    // Tile types
    const int WATER = 0;
    const int SAND = 1;
    const int GRASS = 2;
    const int ROCK = 3;
    const int EMPTY = -1;

    // Menu
    GameObject PortButton;

    // Use this for initialization
    void Start()
    {
        position = this.GetComponentsInParent<Transform>()[1];
        Ship = this.GetComponentsInParent<Rigidbody2D>()[0];
        info = this.GetComponentsInParent<WorldInfo>()[0];
        animator = GetComponent<Animator>();
        tileWidth = info.water.rect.width / 100.0f;
        sandHeight = info.sandHeight;
        grassHeight = info.grassHeight;
        rockHeight = info.rockHeight;
        detailLevel = info.detailLevel;
        dockingDistance = info.dockingDistance;
        detailLevel = info.detailLevel;
        offset = (int)(Math.Pow(2, detailLevel) + 1) / 2;
        heightmap = info.heightmap;
        PortButton = GameObject.FindGameObjectWithTag("PortButton");
        PortButton.SetActive(false);
        n = (int)Math.Pow(2, detailLevel) + 1;
    }

    bool isDockable()
    {
        for (int j = 0; j < dockingDistance; j++)
        {
            int y = WorldToCell().y + j - dockingDistance / 2;
            for (int i = 0; i < dockingDistance; i++)
            {
                int x = WorldToCell().x + i - dockingDistance / 2;
                Vector3Int p = new Vector3Int(x, y, 0);
                if (GetTileType(p) == SAND)
                {
                    return true;
                }
            }
        }

        return false;
    }

    Vector3Int WorldToCell()
    {
        int x = (int)(position.position.x / tileWidth) + 1;
        int y = (int)(position.position.y / tileWidth) + 1;
        return new Vector3Int(x, y, 0);
    }

    int GetTileType(Vector3Int p)
    {
        Tile tile = (Tile)tilemap.GetTile(p);
        if (tile == null)
        {
            return EMPTY;
        }
        else
        {
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
        }
        return EMPTY;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);

        // Movement
        if (Input.GetKey("right"))
        {
            velocity += new Vector3(speed, 0.0f, 0.0f);
            animator.Play("Right");
        }
        if (Input.GetKey("left"))
        {
            velocity += new Vector3(-speed, 0.0f, 0.0f);
            animator.Play("Left");
        }
        if (Input.GetKey("up"))
        {
            velocity += new Vector3(0.0f, speed, 0.0f);
            animator.Play("Up");
        }
        if (Input.GetKey("down"))
        {
            velocity += new Vector3(0.0f, -speed, 0.0f);
        }
        Ship.velocity = velocity;

        // Docking  
        PortButton.SetActive(isDockable());

        // Loop world map
        if (position.position.x < -tileWidth * (float)n)
        {
            position.position = new Vector3(tileWidth * (float)(xBlocks - 1) * (float)n, position.position.y, 0.0f);
        }
        if (position.position.x > tileWidth * (float)(xBlocks) * (float)n)
        {
            position.position = new Vector3(0.0f, position.position.y, 0.0f);
        }
        if (position.position.y < -tileWidth * (float)n)
        {
            position.position = new Vector3(position.position.x, tileWidth * (float)(yBlocks - 1) * (float)n, 0.0f);
        }
        if (position.position.y > tileWidth * (float)(yBlocks) * (float)n)
        {
            position.position = new Vector3(position.position.x, 0.0f, 0.0f);
        }
    }
}
