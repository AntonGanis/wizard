using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Transform pos;

    [Header("Здоровье")]
    public int health;
    public Slider slider1;

    [Header("Воздух")]
    public GameObject Ui;
    public bool inWater;
    public Transform rot;
    int air = 100;
    public int airMinus = 4;
    public int airPlus = 2;
    public Slider slider2;
    float time;

    [Header("Дополнительное здоровье")]
    public int health2;
    public Slider slider3;

    [Header("Сопротивления")]
    public float physicalResistance;
    public float fireResistance;
    float fireResistance2;
    public float electricResistance;
    public float waterResistance;
    public float coldResistance;
    public float poisonResistance;
    public float soulResistance;
    public float bloodResistance;
    public float stunningResistance;
    public float magikResistance;

    UpSpeed upSpeed;
    Rigidbody rb;
    float oldMass;
    float currectMass;

    [Header("Горение")]
    public float fireLimit;
    public float fireStatus;
    public float firePlusStatus;
    public int fireMinus;
    float time2;
    public GameObject Ui2;

    void Start()
    {
        upSpeed = GetComponent<UpSpeed>();
        rb = GetComponent<Rigidbody>();
        oldMass = rb.mass;
    }

    public void TakeDamage(int amount, DamageType type)
    {
        float effectiveDamage = amount;

        switch (type)
        {
            case DamageType.Physical:
                effectiveDamage *= (1 - physicalResistance);
                break;

            case DamageType.Fire:
                effectiveDamage *= (1 - fireResistance);
                fireStatus += firePlusStatus;
                break;

            case DamageType.Electric:
                effectiveDamage *= (1 - electricResistance);
                break;

            case DamageType.Water:
                effectiveDamage *= (1 - waterResistance);
                fireStatus -= firePlusStatus * 1.5f;
                break;

            case DamageType.Cold:
                effectiveDamage *= (1 - coldResistance);
                break;

            case DamageType.Poison:
                effectiveDamage *= (1 - poisonResistance);
                break;

            case DamageType.Soul:
                effectiveDamage *= (1 - soulResistance);
                break;

            case DamageType.Blood:
                effectiveDamage *= (1 - bloodResistance);
                break;

            case DamageType.Stunning:
                effectiveDamage *= (1 - stunningResistance);
                break;

            case DamageType.Magik:
                effectiveDamage *= (1 - magikResistance);
                break;
        }

        if (health2 <= 0)
        {
            if (rb.mass != oldMass)
            {
                rb.mass -= currectMass;

                if (rb.mass < oldMass)
                {
                    rb.mass = oldMass;
                }
            }
            health -= Mathf.RoundToInt(effectiveDamage);
            health = Mathf.Max(health, 0);
            if (health <= 0)
            {
                Death();
            }
        }
        else
        {
            health2 -= Mathf.RoundToInt(effectiveDamage);
            health2 = Mathf.Max(health2, 0);
            slider3.value = health2;
        }
    }

    void Death()
    {
        if(pos == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.transform.position = pos.position;
            health = 500;
        }
    }

    public void DopHealth(int hp, float speedArmor, float mass)
    {
        health2 = hp;
        slider3.maxValue = health2;
        slider3.value = health2;
        currectMass = mass;
        rb.mass += currectMass;
        if (speedArmor != 0 && upSpeed != null)
        {
            upSpeed.AddSpeed(speedArmor);
        }
    }

    void FIRE()
    {
        if (fireStatus > 0)
        {
            time2 += Time.deltaTime;

            if (time2 > 0.5f)
            {
                if (fireStatus > fireLimit)
                {
                    if (Ui2.activeSelf == false)
                    {
                        Ui2.SetActive(true);
                        fireStatus *= 1.5f;
                    }
                    float effectiveDamage = fireMinus;
                    effectiveDamage *= (1 - fireResistance);
                    TakeDamage((int)effectiveDamage, DamageType.None);
                }
                fireStatus -= firePlusStatus/1.5f;
                time2 = 0;
                if (fireStatus < 0)
                    fireStatus = 0;
            }
        }
        if (fireStatus < fireLimit && Ui2.activeSelf == true)
        {
            Ui2.SetActive(false);
        }
    }
    void WATER()
    {
        if (inWater)
        {
            if (fireResistance != 1)
            {
                fireResistance2 = fireResistance;
                fireResistance = 1;
                fireStatus = 0;
            }
            if (air > 0)
            {
                time += Time.deltaTime;
                if (time > 0.5f)
                {
                    air -= airMinus;
                    time = 0;
                }
            }
            else
            {
                Death();
            }
        }
        else if (air < 100 || fireResistance == 1)
        {
            if (fireResistance == 1)
            {
                fireResistance = fireResistance2;
            }
            time += Time.deltaTime;
            if (time > 0.5f)
            {
                air += airPlus;
                time = 0;
            }
        }
        else
        {
            slider2.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        slider1.value = health;
        slider2.value = air;
        WATER();
        FIRE();


    }

}
