using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class PlayerShootManager : MonoBehaviour
{
    public static PlayerShootManager instance { get; private set; }

    [SerializeField] GameObject shootPopUpPrefab;

    private void Awake()
    {
        instance = this;
    }

    //Access Entity from GameObject
    /*
    private void Start()
    {
        PlayerShootingSystem playerShootingSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<PlayerShootingSystem>();
        playerShootingSystem.OnShoot += PlayerShootingSystem_OnShoot;
    }

    void PlayerShootingSystem_OnShoot(object sender, System.EventArgs e)
    {
        Entity playerEntity = (Entity)sender;
        LocalTransform localTransform = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<LocalTransform>(playerEntity);
        Instantiate(shootPopUpPrefab, localTransform.Position, Quaternion.identity);
    }
    */

    public void PlayerShoot(Vector3 playerPosition)
    {
        Instantiate(shootPopUpPrefab, playerPosition, Quaternion.identity);

    }
}
