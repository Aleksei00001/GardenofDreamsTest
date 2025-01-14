using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private VirtualJoystick joystick;
    [SerializeField] private Inventory inventory;
    [SerializeField] private float maxHP;
    [SerializeField] private float HP;
    [SerializeField] private Slot slotItemWeapon;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private float detectionRadius;
    [SerializeField] private Enemy enemy;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform visual;
    [SerializeField] private Image imageHP;

    private float attackTimer;

    private void Start()
    {
        slotItemWeapon.SetInventory(inventory);
        slotItemWeapon.UpdateDate();
    }

    void Update()
    {
        Vector2 moveDirection = new Vector2(joystick.inputVector.x, joystick.inputVector.y);
        rigidbody.velocity = moveDirection * moveSpeed;

        if(moveDirection.magnitude >= 0.01)
        {
            animator.SetBool("Move", true);
            if (moveDirection.magnitude * moveSpeed > 0.5)
            {
                animator.SetFloat("Speed", moveDirection.magnitude * moveSpeed);
            }
            else
            {
                animator.SetFloat("Speed", 0.5f);
            }
        }
        else
        {
            animator.SetBool("Move", false);
        }

        if (joystick.inputVector.x > 0)
        {
            visual.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            visual.localScale = new Vector3(-1, 1, 1);
        }

        if (slotItemWeapon.item != null)
        {
            if (attackTimer < slotItemWeapon.item.attackeSpeed)
            {
                attackTimer += Time.deltaTime;
            }
        }

        enemy = DetectEnemies();


        imageHP.fillAmount = HP / maxHP;
    }

    public void Attack()
    {
        if (attackTimer >= slotItemWeapon.item.attackeSpeed)
        {
            if (enemy != null)
            {
                if (slotItemWeapon.item.itemType == 3)
                {
                    Slot bulletSlot = inventory.FindItemID(slotItemWeapon.item.useBulletID);
                    if (bulletSlot != null)
                    {
                        bulletSlot.item.AddCount(-1);
                        if (bulletSlot.item.count <= 0)
                        {
                            inventory.RemoveItem(bulletSlot);
                        }
                        else
                        {
                            bulletSlot.UpdateDate();
                        }
                        Projectile newProjectile = Instantiate(projectilePrefab, this.transform.position, this.transform.rotation);
                        newProjectile.SetProjectileDate(5, slotItemWeapon.item.damage, 10, new Vector2(enemy.transform.position.x - transform.position.x, enemy.transform.position.y - transform.position.y));
                        attackTimer = 0;
                    }
                }
            }
        }
    }

    public void SelectSlotItemWeapon()
    {
        if (slotItemWeapon.item != null)
        {
            Item tempSlot = new Item();
            tempSlot.SetItem(slotItemWeapon.item);
            slotItemWeapon.item.SetItem(inventory.selectSlot.item);
            inventory.selectSlot.item.SetItem(tempSlot);
            slotItemWeapon.UpdateDate();
            inventory.selectSlot.UpdateDate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DropItem>() != null)
        {
            if (inventory.AddItem(collision.GetComponent<DropItem>().item) == true)
            {
                Destroy(collision.gameObject);
            }
        }
    }

    public Enemy DetectEnemies()
    {
        Enemy target = null;

        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        List<Enemy> enemyList = new List<Enemy>();

        if (enemiesInRange.Length > 0)
        {
            foreach (Collider2D enemy in enemiesInRange)
            {
                if (enemy.GetComponent<Enemy>() != null)
                {
                    enemyList.Add(enemy.GetComponent<Enemy>());
                }
            }
        }

        if (enemyList.Count > 0)
        {
            target = enemyList[0];
            {
                for (int i = 0; i < enemyList.Count - 1; i++)
                {
                    if (Vector2.Distance(target.transform.position, this.transform.position) > Vector2.Distance(enemyList[i].transform.position, this.transform.position))
                    {
                        target = enemyList[i];
                    }
                }
            }
        }
        return target;
    }

    public void GetDamage(float getDamage)
    {
        HP -= getDamage;
        if (HP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void LoadPlayerDate(PlayerDate newPlayer)
    {
        slotItemWeapon.item.SetItem(newPlayer._slot._itemData);
        slotItemWeapon.item.SetSprite(inventory.FindSpriteForID(slotItemWeapon.item.itemID));
        slotItemWeapon.UpdateDate();
        transform.position = new Vector2(newPlayer._positionX, newPlayer._positionY);
        moveSpeed = newPlayer._moveSpeed;
        maxHP = newPlayer._maxHP;
        HP = newPlayer._HP;
    }

    public PlayerDate GetPlayerDate()
    {
        PlayerDate playerDate = new PlayerDate(slotItemWeapon, transform.position.x, transform.position.y, moveSpeed, maxHP, HP);
        return playerDate;
    }
}
public class PlayerDate
{
    public SlotDate _slot;
    public float _positionX;
    public float _positionY;
    public float _moveSpeed;
    public float _maxHP;
    public float _HP;

    public PlayerDate(Slot slot, float positionX, float positionY, float moveSpeed, float maxHP, float HP)
    {
        _slot = slot.GetSlotDate();
        _positionX = positionX;
        _positionY = positionY;
        _moveSpeed = moveSpeed;
        _maxHP = maxHP;
        _HP = HP;
    }

    public PlayerDate() { }
}