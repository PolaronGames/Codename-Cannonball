﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{

    // Player data
    Transform position;
    public float speed;

    // World data
    Tilemap tilemap;
    WorldInfo info;
    float[,] heightmap;
    int detailLevel;
    int offset;

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
        info = this.GetComponentsInParent<WorldInfo>()[0];
        detailLevel = info.detailLevel;
        offset = (int)(Math.Pow(2, detailLevel) + 1)/2;
        tilemap = info.GetComponentsInChildren<Tilemap>()[0];
        heightmap = info.heightmap;
    }

    Vector3Int WorldToCell()
    {
        int x = (int)(position.position.x/2.56f) - 1;
        int y = (int)(position.position.y/2.56f) - 1;
        return new Vector3Int(x, y, 0);
    }

    int GetTileType()
    {
        Tile tile = (Tile)tilemap.GetTile(WorldToCell());
        String name = tile.name;

        switch(name)
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
        // Movement
        if (Input.GetKey("right"))
        {
            Vector3 step = new Vector3(0.1f * speed, 0.0f, 0.0f);
            position.Translate(step);
        }
        if (Input.GetKey("left"))
        {
            Vector3 step = new Vector3(-0.1f * speed, 0.0f, 0.0f);
            position.Translate(step);
        }
        if (Input.GetKey("up"))
        {
            Vector3 step = new Vector3(0.0f, 0.1f * speed, 0.0f);
            position.Translate(step);
        }
        if (Input.GetKey("down"))
        {
            Vector3 step = new Vector3(0.0f, -0.1f * speed, 0.0f);
            position.Translate(step);
        }

        // Docking     
        Vector3Int TileIndex = WorldToCell();
        float elevation = heightmap[TileIndex.x + offset, TileIndex.y + offset];
        if(elevation > 0.1f)
        {
            Debug.Log("Port Menu Available");
        }
    }
}
