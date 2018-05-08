using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{

    // Player data
    Transform transform;
    public float speed;
    Rigidbody2D Ship;
    Animator animator;
    public enum ShipDirection{
        RIGHT, UPRIGHT, UP, UPLEFT, LEFT, DOWNLEFT, DOWN, DOWNRIGHT
    };
    public ShipDirection shipDirectionState;

    // World data
    WorldInfo info;
    public Tilemap tilemap;
    int dockingDistance;
    float tileWidth;

    // make this an enum
    // Tile types
    enum TileType{
        WATER,
        SAND,
        GRASS,
        ROCK,
        EMPTY
    }

    // Menu
    GameObject PortButton;

    // Weapon State
        public enum Weapons{
        WEAPON_SLOT_ONE,
        WEAPON_SLOT_TWO
    }
    public Weapons activeWeapon;

    public string weaponSlotOneName;
    public string weaponSlotTwoName;


    // Use this for initialization
    void Awake()
    {
        transform = this.GetComponentsInParent<Transform>()[1];
        Ship = this.GetComponentsInParent<Rigidbody2D>()[0];
        info = this.GetComponentsInParent<WorldInfo>()[0];
        animator = GetComponent<Animator>();
        tileWidth = info.water.rect.width / 100.0f;
        dockingDistance = info.dockingDistance;
        PortButton = GameObject.FindGameObjectWithTag("PortButton");
        PortButton.SetActive(false);
        shipDirectionState = ShipDirection.RIGHT;
        activeWeapon = Weapons.WEAPON_SLOT_ONE;
        weaponSlotOneName = "Cannon 1";
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
                if (GetTileType(p) == TileType.SAND)
                {
                    return true;
                }
            }
        }

        return false;
    }

    Vector3Int WorldToCell()
    {
        int x = (int)(transform.position.x / tileWidth) + 1;
        int y = (int)(transform.position.y / tileWidth) + 1;
        return new Vector3Int(x, y, 0);
    }

    TileType GetTileType(Vector3Int p)
    {
        Tile tile = (Tile)tilemap.GetTile(p);
        if (tile == null)
        {
            return TileType.EMPTY;
        }
        else
        {
            String name = tile.name;

            switch (name)
            {
                case "water":
                    return TileType.WATER;
                case "sand":
                    return TileType.SAND;
                case "grass":
                    return TileType.GRASS;
                case "rock":
                    return TileType.ROCK;
            }
        }
        return TileType.EMPTY;
    }

    public void SetWeaponOneSlotFilePath(string projectileName)
    {
        weaponSlotOneName = projectileName;
    }

    public void SetWeaponTwoSlotFilePath(string projectileName)
    {
        weaponSlotTwoName = projectileName;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);

        if (Input.GetKey(KeyCode.Alpha1))
        {
            activeWeapon = Weapons.WEAPON_SLOT_ONE;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            activeWeapon = Weapons.WEAPON_SLOT_TWO;
        }

        int x = 0, y = 0;
        // Movement
        if (Input.GetKey("right"))
        {
            velocity += new Vector3(speed, 0.0f, 0.0f);
            x += 1;
            shipDirectionState = ShipDirection.RIGHT;
        }
        if (Input.GetKey("left"))
        {
            velocity += new Vector3(-speed, 0.0f, 0.0f);
            x -= 1;
            shipDirectionState = ShipDirection.LEFT;
        }
        if (Input.GetKey("up"))
        {
            velocity += new Vector3(0.0f, speed, 0.0f);
            y += 1;
            shipDirectionState = ShipDirection.UP;
        }
        if (Input.GetKey("down"))
        {
            velocity += new Vector3(0.0f, -speed, 0.0f);
            y -= 1;
            shipDirectionState = ShipDirection.DOWN;
        }
        Ship.velocity = velocity;

        // animation
        switch(x)
        {
            case 1:
            switch(y)
            {
                case 1: // up-right
                animator.Play("ship_upright_anim");
                shipDirectionState = ShipDirection.UPRIGHT;
                break;
                case 0: // right
                animator.Play("ship_right_anim");
                shipDirectionState = ShipDirection.RIGHT;
                break;
                case -1: // down-right
                animator.Play("ship_downright_anim");
                shipDirectionState = ShipDirection.DOWNRIGHT;
                break;
            }
            break;
            case 0:
            switch(y)
            {
                case 1: // up
                animator.Play("ship_up_anim");
                shipDirectionState = ShipDirection.UP;
                break;
                case 0: // idle
                switch(shipDirectionState)
                {
                    case ShipDirection.RIGHT:
                    //animator.Play("ship_idle_right_anim");
                    break;
                    case ShipDirection.UPRIGHT:
                    //animator.Play("ship_idle_upright_anim");
                    break;
                    case ShipDirection.UP:
                    //animator.Play("ship_idle_up_anim");
                    break;
                    case ShipDirection.UPLEFT:
                    //animator.Play("ship_idle_upleft_anim");
                    break;
                    case ShipDirection.LEFT:
                    //animator.Play("ship_idle_left_anim");
                    break;
                    case ShipDirection.DOWNLEFT:
                    //animator.Play("ship_idle_downleft_anim");
                    break;
                    case ShipDirection.DOWN:
                    //animator.Play("ship_idle_down_anim");
                    break;
                    case ShipDirection.DOWNRIGHT:
                    //animator.Play("ship_idle_downright_anim");
                    break;
                }
                break;
                case -1: // down
                animator.Play("ship_down_anim");
                shipDirectionState = ShipDirection.DOWN;
                break;
            }
            break;
            case -1:
            switch(y)
            {
                case 1: // up-left
                animator.Play("ship_upleft_anim");
                shipDirectionState = ShipDirection.UPLEFT;
                break;
                case 0: // left
                animator.Play("ship_left_anim");
                shipDirectionState = ShipDirection.LEFT;
                break;
                case -1: // down-left
                animator.Play("ship_downleft_anim");
                shipDirectionState = ShipDirection.DOWNLEFT;
                break;
            }
            break;
        }
        
        // Docking  
        PortButton.SetActive(isDockable());
    }
}
