using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;

    [SerializeField] private Player player;

    [SerializeField] private List<Enemy> enemies;

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Enemy newEnemy = Instantiate<Enemy>(enemyPrefab);
            newEnemy.SetPlayer(player);
            Vector2 positoinSpawn = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            positoinSpawn = Random.Range(7f,10f) * positoinSpawn.normalized;
            newEnemy.transform.position = positoinSpawn;
            enemies.Add(newEnemy);
        }
    }

    public void ClealNull()
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }
    }

    public void LoadEnemySpawnerDate(EnemySpawnerDate newEnemies)
    {
        ClealNull();

        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            Destroy(enemies[i].gameObject);
        }
        enemies.Clear();

        for (int i = 0; i < newEnemies.enemyDates.Count; i++)
        {
            Enemy newEnemy = Instantiate<Enemy>(enemyPrefab);
            newEnemy.SetPlayer(player);
            newEnemy.transform.position = new Vector2(newEnemies.enemyDates[i]._positionX, newEnemies.enemyDates[i]._positionY);
            newEnemy.SetHP(newEnemies.enemyDates[i]._HP);
            enemies.Add(newEnemy);
        }
    }

    public EnemySpawnerDate GetEnemySpawnerDate()
    {
        ClealNull();

        EnemySpawnerDate playerDate = new EnemySpawnerDate(enemies);
        return playerDate;
    }
}
public class EnemySpawnerDate
{
    public List<EnemyDate> enemyDates = new List<EnemyDate>();

    public EnemySpawnerDate(List<Enemy> enemies)
    {
        foreach (var itemEnemies in enemies)
        {
            enemyDates.Add(itemEnemies.GetEnemyDate());
        }
    }

    public EnemySpawnerDate() { }
}