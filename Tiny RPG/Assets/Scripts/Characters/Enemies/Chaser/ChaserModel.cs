using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserModel : CharacterStats, IDamageable
{
    ChaserController controller;

    [Header("Loot Drop")]
    public GameObject WorldItemPrefab;

    [SerializeField]
    LootTable[] lootTable;

    private void Start()
    {
        controller = GetComponentInParent<ChaserController>();
    }

    protected int attack = 10;
    public int GetAttack()
    {
        return attack;
    }

    public void TakeDamage(int damage, Vector3 knockback)
    {
        SetHitPoints(GetHitPoints() - damage);
        controller.StartKnockback(knockback);
        controller.IsHurt();
        if (isDead())
        {

            KillEnemy();
        }
    }

    public void KillEnemy()
    {
        DropLoot();
        Destroy(controller.gameObject);
    }

    public void DropLoot()
    {
        for (int i = 0; i < lootTable.Length; i++)
        {
            for(int j = 0; j < lootTable[i].maxDrop;j++)
            {
                if(j < lootTable[i].minDrop)
                {
                    GameObject loot = Instantiate<GameObject>(WorldItemPrefab, transform.position, Quaternion.identity);
                    loot.GetComponent<WorldItem>().SetItem(lootTable[i].item);

                    Vector2 randomDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                    loot.GetComponent<Rigidbody2D>().AddForce(randomDir, ForceMode2D.Impulse);
                }
                else
                {
                    int random = Random.Range(0, 100);
                    if (random < lootTable[i].dropChance)
                    {
                        GameObject loot = Instantiate<GameObject>(WorldItemPrefab,transform.position,Quaternion.identity);
                        loot.GetComponent<WorldItem>().SetItem(lootTable[i].item);

                        Vector2 randomDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                        loot.GetComponent<Rigidbody2D>().AddForce(randomDir,ForceMode2D.Impulse);
                    }
                }
            }
        }
    }
}
