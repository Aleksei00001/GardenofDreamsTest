using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHP;
    [SerializeField] private float HP;
    [SerializeField] private float damage;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackSpeed;
    private float attackTimer;

    private Player player;
    private EnemySpawner enemySpawner;

    [SerializeField] private List<DropItem> dropItems;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform visual;
    [SerializeField] private Image imageHP;

    private void Update()
    {
        if (player != null)
        {
            if (Vector2.Distance(this.transform.position, player.transform.position) < 6)
            {
                Vector2 moveDirection = new Vector3(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y);
                rigidbody.velocity = moveDirection.normalized * moveSpeed;
                animator.SetBool("Move", true);
                if (player.transform.position.x > this.transform.position.x)
                {
                    visual.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    visual.localScale = new Vector3(-1, 1, 1);
                }
            }
            else
            {
                rigidbody.velocity = new Vector2(0, 0);
                animator.SetBool("Move", false);
            }

            if (Vector2.Distance(this.transform.position, player.transform.position) < 1)
            {
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackSpeed)
                {
                    player.GetDamage(damage);
                    attackTimer = 0;
                }
            }
        }

        imageHP.fillAmount = HP / maxHP;
    }

    public void SetPlayer(Player newPlayer)
    {
        player = newPlayer;
    }

    public void GetDamage(float getDamage)
    {
        HP -= getDamage;
        if (HP <= 0)
        {
            DropItem newDropItem = Instantiate<DropItem>(dropItems[Random.Range(0, dropItems.Count)]);
            newDropItem.transform.position = transform.position;
            Destroy(this.gameObject);
        }
    }

    public void SetHP(float newHP)
    {
        HP = newHP;
    }

    public EnemyDate GetEnemyDate()
    {
        EnemyDate enemyDate = new EnemyDate(transform.position.x, transform.position.y, HP);
        return enemyDate;
    }
}
public class EnemyDate
{
    public float _positionX;
    public float _positionY;
    public float _HP;

    public EnemyDate(float positionX, float positionY, float HP)
    {
        _positionX = positionX;
        _positionY = positionY;
        _HP = HP;
    }
    [SerializeField] private float HP;

    public EnemyDate() { }
}