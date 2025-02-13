using UnityEngine;

[System.Flags]
public enum DamageType
{
    None = 0,
    Physical = 1 << 0,  // 1
    Fire = 1 << 1,      // 2
    Water = 1 << 2,     // 4
    Cold = 1 << 3,      // 8
    Electric = 1 << 4,  // 16
    Poison = 1 << 5,    // 32
    Soul = 1 << 6,      // 64
    Blood = 1 << 7,     // 128
    Stunning = 1 << 8,   // 256
    Magik = 1 << 9   // 512
}
