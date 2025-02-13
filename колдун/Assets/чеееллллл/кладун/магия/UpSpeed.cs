using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpeedNew
{
    public float initialSpeed;
    public float duration;
    public float multiplier;
    private Move player;

    public int armor;
    bool armr;
    private Health player2;

    public SpeedNew(Move player, float multiplier, float duration)
    {
        this.player = player;
        this.initialSpeed = player.speed;
        this.multiplier = multiplier;
        this.duration = duration;

        player.speed *= multiplier;
    }

    public SpeedNew(Move player, float multiplier)
    {
        this.player = player;
        this.initialSpeed = player.speed;
        this.multiplier = multiplier;
        this.player2 = player.gameObject.GetComponent<Health>();

        player.speed *= multiplier;
        armr = true;
    }

    public void Update(UpSpeed upSpeed)
    {
        if (!armr)
        {
            if (duration > 0)
            {
                duration -= Time.deltaTime;
                if (duration <= 0)
                {
                    ResetSpeed(upSpeed);
                }
            }
        }
        else
        {
            if (player2.health2 == 0)
            {
                ResetSpeed(upSpeed);
            }
        }

    }

    private void ResetSpeed(UpSpeed upSpeed)
    {
        player.speed = initialSpeed;
        upSpeed.RemoveSpeed(this); 
    }
}

public class UpSpeed : MonoBehaviour
{
    private Move player;
    public List<SpeedNew> spd;

    void Start()
    {
        player = GetComponent<Move>();
        spd = new List<SpeedNew>();
    }

    public void AddSpeed(float multiplier, float duration)
    {
        SpeedNew speedEffect = new SpeedNew(player, multiplier, duration);
        spd.Add(speedEffect);
        StartCoroutine(UpdateSpeedEffects());
    }

    public void AddSpeed(float multiplier)
    {
        SpeedNew speedEffect = new SpeedNew(player, multiplier);
        spd.Add(speedEffect);
        StartCoroutine(UpdateSpeedEffects());
    }

    private IEnumerator UpdateSpeedEffects()
    {
        while (true)
        {
            foreach (var speedEffect in spd)
            {
                speedEffect.Update(this); 
            }
            yield return null;
        }
    }

    public void RemoveSpeed(SpeedNew speedEffect)
    {
        spd.Remove(speedEffect);
    }
}
