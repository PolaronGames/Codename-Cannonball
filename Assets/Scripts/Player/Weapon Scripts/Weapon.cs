using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum WeaponSide
{
    LEFT,
    RIGHT
};

public class Weapon : MonoBehaviour
{
    // Projectile Data
    GameObject projectilePrefab;

    GameObject player;
    Movement playerData;

    Transform playerTransform;


    float speed;
    float damage;
    float projectileLifeSpan;

    float fireRate;

    float nextFire = 0.0f;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        playerData = player.GetComponent<Movement>();
        playerTransform = player.GetComponent<Transform>();
        projectilePrefab = Resources.Load("Prefabs/" + playerData.weaponSlotOneName) as GameObject;
        SetActiveWeapon();
    }

    public void SetActiveWeapon()
    {
        if (playerData.activeWeapon == Weapons.WEAPON_SLOT_ONE)
        {
            projectilePrefab = Resources.Load("Prefabs/" + playerData.weaponSlotOneName) as GameObject;
            speed = projectilePrefab.GetComponent<Projectile>().speed;
            damage = projectilePrefab.GetComponent<Projectile>().damage;
            fireRate = projectilePrefab.GetComponent<Projectile>().fireRate;
            projectileLifeSpan = projectilePrefab.GetComponent<Projectile>().lifeSpan;
        }
        else
        {
            projectilePrefab = Resources.Load("Prefabs/" + playerData.weaponSlotTwoName) as GameObject;
            speed = projectilePrefab.GetComponent<Projectile>().speed;
            damage = projectilePrefab.GetComponent<Projectile>().damage;
            fireRate = projectilePrefab.GetComponent<Projectile>().fireRate;
            projectileLifeSpan = projectilePrefab.GetComponent<Projectile>().lifeSpan;
        }
    }

    void Update()
    {
        // Fire Projectile
        if (Input.GetKey("A") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Fire(WeaponSide.LEFT, playerData.shipDirectionState);
        }

        if (Input.GetKey("D") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Fire(WeaponSide.RIGHT, playerData.shipDirectionState);
        }
    }

    void Fire(WeaponSide side, ShipDirection direction)
    {
        Vector3 offset = new Vector3(0.0f, 0.0f, 0.0f);
        Vector2 velocity;
        GameObject projectile = Instantiate(projectilePrefab, playerTransform.position, Quaternion.identity);
        Rigidbody2D body = projectile.GetComponent<Rigidbody2D>();
        body.gravityScale = 0.0f;
        switch (direction)
        {
            case ShipDirection.RIGHT:
                switch (side)
                {
                    case WeaponSide.LEFT:
                        offset = new Vector3(0.0f, 1.0f, 0.0f);
                        break;
                    case WeaponSide.RIGHT:
                        offset = new Vector3(0.0f, -1.0f, 0.0f);
                        break;
                }
                break;
            case ShipDirection.UPRIGHT:
                switch (side)
                {
                    case WeaponSide.LEFT:
                        offset = new Vector3(-1.0f, 1.0f, 0.0f);
                        break;
                    case WeaponSide.RIGHT:
                        offset = new Vector3(1.0f, -1.0f, 0.0f);
                        break;
                }
                break;
            case ShipDirection.UP:
                body.gravityScale = 1.0f;
                switch (side)
                {
                    case WeaponSide.LEFT:
                        offset = new Vector3(-1.0f, 0.0f, 0.0f);
                        break;
                    case WeaponSide.RIGHT:
                        offset = new Vector3(1.0f, 0.0f, 0.0f);
                        break;
                }
                break;
            case ShipDirection.UPLEFT:
                switch (side)
                {
                    case WeaponSide.LEFT:
                        offset = new Vector3(-1.0f, -1.0f, 0.0f);
                        break;
                    case WeaponSide.RIGHT:
                        offset = new Vector3(1.0f, 1.0f, 0.0f);
                        break;
                }
                break;
            case ShipDirection.LEFT:
                switch (side)
                {
                    case WeaponSide.LEFT:
                        offset = new Vector3(0.0f, -1.0f, 0.0f);
                        break;
                    case WeaponSide.RIGHT:
                        offset = new Vector3(0.0f, 1.0f, 0.0f);
                        break;
                }
                break;
            case ShipDirection.DOWNLEFT:
                switch (side)
                {
                    case WeaponSide.LEFT:
                        offset = new Vector3(1.0f, -1.0f, 0.0f);
                        break;
                    case WeaponSide.RIGHT:
                        offset = new Vector3(-1.0f, 1.0f, 0.0f);
                        break;
                }
                break;
            case ShipDirection.DOWN:
                body.gravityScale = 1.0f;
                switch (side)
                {
                    case WeaponSide.LEFT:
                        offset = new Vector3(1.0f, 0.0f, 0.0f);
                        break;
                    case WeaponSide.RIGHT:
                        offset = new Vector3(-1.0f, 0.0f, 0.0f);
                        break;
                }
                break;
            case ShipDirection.DOWNRIGHT:
                switch (side)
                {
                    case WeaponSide.LEFT:
                        offset = new Vector3(1.0f, 1.0f, 0.0f);
                        break;
                    case WeaponSide.RIGHT:
                        offset = new Vector3(-1.0f, -1.0f, 0.0f);
                        break;
                }
                break;
        }
        offset.Normalize();
        velocity = offset;
        offset *= 0.25f;
        velocity *= speed;
        projectile.transform.position += offset;
        body.velocity = velocity + playerData.Ship.velocity;
    }

    // FIREEEEEEEEEEEEEEEEEEEEEEEEEEEEEE
    /*void Fire(fireSide fireSide, Player.ShipDirection shipDirectionState)
    {
        Vector2 offset = new Vector2(0.0f, 0.0f);
        // Create the Projectile from the Bullet Prefab
        var projectile = (GameObject)Instantiate(
            projectilePrefab,
            (Vector2)position.position + offset,
            Quaternion.identity);

        var projectileComponent = projectile.GetComponent<Rigidbody2D>();    

        // Set Velocity
        float angle = 5.0f;
        if (fireSide == fireSide.LEFT)
        {
            if (shipDirectionState == Player.ShipDirection.UP)
            {
                offset = new Vector2(-0.25f, 0.0f);
                projectileComponent.velocity = new Vector2(-speed, angle) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 1.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.DOWN)
            {
                offset = new Vector2(-0.25f, 0.0f);
                projectileComponent.velocity = new Vector2(-speed, angle) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 1.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.RIGHT)
            {
                offset = new Vector2(0.0f, 0.25f);
                projectileComponent.velocity = new Vector2(0.0f, speed) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 0.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.LEFT)
            {
                offset = new Vector2(0.0f, -0.25f);
                projectileComponent.velocity = new Vector2(0.0f, -speed) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 0.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.DOWNLEFT)
            {
                offset = new Vector2(-0.125f, 0.125f);
                projectileComponent.velocity = new Vector2(-speed/2, speed/2) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 0.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.DOWNRIGHT)
            {
                offset = new Vector2(-0.125f, -0.125f);
                projectileComponent.velocity = new Vector2(-speed/2, -speed/2) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 0.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.UPLEFT)
            {
                offset = new Vector2(-0.125f, -0.125f);
                projectileComponent.velocity = new Vector2(-speed/2, -speed/2) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 0.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.UPRIGHT)
            {
                offset = new Vector2(-0.125f, 0.125f);
                projectileComponent.velocity = new Vector2(-speed/2, speed/2) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 0.0f;
            }
        }
        else if (fireSide == fireSide.RIGHT)
        {
            if (shipDirectionState == Player.ShipDirection.UP)
            {
                offset = new Vector2(0.25f, 0.0f);
                projectileComponent.velocity = new Vector2(speed, angle) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 1.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.DOWN)
            {
                offset = new Vector2(0.25f, 0.0f);
                projectileComponent.velocity = new Vector2(speed, angle) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 1.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.RIGHT)
            {
                offset = new Vector2(0.0f, -0.25f);
                projectileComponent.velocity = new Vector2(0.0f, -speed) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 0.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.LEFT)
            {
                offset = new Vector2(0.0f, 0.25f);
                projectileComponent.velocity = new Vector2(0.0f, speed) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 0.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.DOWNLEFT)
            {
                offset = new Vector2(0.125f, 0.125f);
                projectileComponent.velocity = new Vector2(speed/2, -speed/2) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 0.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.DOWNRIGHT)
            {
                offset = new Vector2(0.125f, -0.125f);
                projectileComponent.velocity = new Vector2(speed/2, speed/2) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 0.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.UPLEFT)
            {
                offset = new Vector2(0.125f, 0.125f);
                projectileComponent.velocity = new Vector2(speed/2, speed/2) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 0.0f;
            }
            else if (shipDirectionState == Player.ShipDirection.UPRIGHT)
            {
                offset = new Vector2(0.125f, -0.125f);
                projectileComponent.velocity = new Vector2(speed/2, -speed/2) + playerScript.Ship.velocity;
                projectileComponent.gravityScale = 0.0f;
            }
        }

        // Destroy the bullet based on projectile life span
        Destroy(projectile, projectileLifeSpan);        
    }*/
}