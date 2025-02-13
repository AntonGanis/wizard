using System.Collections.Generic;
using UnityEngine;

public class Aura : MonoBehaviour
{
    List<Health> enemies = new List<Health>();
    Health myHealth;
    int o;
    public DamageType damageTypes;
    public int valueDown;
    public float damage;
    float damage2;
    float time;
    public GameObject[] Ui;
    int I;

    void Start()
    {
        myHealth = GetComponent<Health>();
        damage2 = damage;
    }

    public void Go(float t, int val, DamageType Tip, int i)
    {
        damageTypes = DamageType.None;
        damageTypes = Tip;
        valueDown = val;
        time = t;
        I = i;
    }


    void Update()
    {
        if(time > 0)
        {
            if(myHealth.fireStatus > myHealth.fireLimit && damageTypes == DamageType.Fire)
            {
                if (damage2 == damage) damage *= 2;
            }
            else { damage = damage2; }
            time -= Time.deltaTime;
            if (Ui[I].activeSelf == false) { Ui[I].SetActive(true); }
            if (o > 20)
            {
                UpdateEnemyList();

                Health closestEnemy = FindClosestEnemy();
                if (closestEnemy != null)
                {
                    float dist = Vector3.Distance(transform.position, closestEnemy.transform.position);
                    if (dist < damage)
                    {
                        closestEnemy.TakeDamage(valueDown, damageTypes);
                    }
                }
                o = 0;
            }
            else
            {
                o++;
            }
        }
        else
        {
            if (Ui[I].activeSelf == true) { Ui[I].SetActive(false); }
        }
    }

    private void UpdateEnemyList()
    {
        enemies.Clear();
        Health[] allEnemies = FindObjectsOfType<Health>();
        foreach (Health enemyHealth in allEnemies)
        {
            if (enemyHealth != myHealth)
            {
                enemies.Add(enemyHealth);
            }
        }
    }

    private Health FindClosestEnemy()
    {
        Health closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Health enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}
