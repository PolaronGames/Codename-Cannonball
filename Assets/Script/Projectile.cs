using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Projectile : MonoBehaviour
{
    // Projectile Data
    public GameObject projectilePrefab;
    
    GameObject player;

    Transform position;

    float cannonBallSpeed;

    public float speed;

    public fireSide fireSideState;
    public enum fireSide {
        LEFT,
        RIGHT
    };

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        position = player.GetComponent<Transform>();
        fireSideState = fireSide.LEFT;
        cannonBallSpeed = 6.0f;
    }

    void Update()
    {
        // Fire Projectile
        if (Input.GetKeyDown(KeyCode.A))
        {
            Fire(fireSide.LEFT, Player.shipDirectionState);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Fire(fireSide.RIGHT, Player.shipDirectionState);
        }
    }

    // FIREEEEEEEEEEEEEEEEEEEEEEEEEEEEEE
    void Fire(fireSide fireSide, Player.shipDirection shipDirectionState)
    {
        Vector2 offset = new Vector2(0.0f, 0.0f);
        // Create the Projectile from the Bullet Prefab
        var projectile = (GameObject)Instantiate(
            projectilePrefab,
            (Vector2)position.position + offset,
            Quaternion.identity);

        var projectileComponent = projectile.GetComponent<Rigidbody2D>();    

        // Set Velocity
        if (fireSide == fireSide.LEFT)
        {
            if (shipDirectionState == Player.shipDirection.UP)
            {
                offset = new Vector2(-0.25f, 0.0f);
                projectileComponent.velocity = new Vector2(-speed, 0.0f);
            }
            else if (shipDirectionState == Player.shipDirection.DOWN)
            {
                offset = new Vector2(-0.25f, 0.0f);
                projectileComponent.velocity = new Vector2(-speed, 0.0f);
            }
            else if (shipDirectionState == Player.shipDirection.RIGHT)
            {
                offset = new Vector2(0.0f, 0.25f);
                projectileComponent.velocity = new Vector2(0.0f, speed);
            }
            else if (shipDirectionState == Player.shipDirection.LEFT)
            {
                offset = new Vector2(0.0f, -0.25f);
                projectileComponent.velocity = new Vector2(0.0f, -speed);
            }
        }
        else if (fireSide == fireSide.RIGHT)
        {
            if (shipDirectionState == Player.shipDirection.UP)
            {
                offset = new Vector2(0.25f, 0.0f);
                projectileComponent.velocity = new Vector2(speed, 0.0f);
            }
            else if (shipDirectionState == Player.shipDirection.DOWN)
            {
                offset = new Vector2(0.25f, 0.0f);
                projectileComponent.velocity = new Vector2(speed, 0.0f);
            }
            else if (shipDirectionState == Player.shipDirection.RIGHT)
            {
                offset = new Vector2(0.0f, -0.25f);
                projectileComponent.velocity = new Vector2(0.0f, -speed);
            }
            else if (shipDirectionState == Player.shipDirection.LEFT)
            {
                offset = new Vector2(0.0f, 0.25f);
                projectileComponent.velocity = new Vector2(0.0f, speed);
            }
        }

        // Destroy the bullet after 2 seconds
        Destroy(projectile, 2.0f);        
    }
}