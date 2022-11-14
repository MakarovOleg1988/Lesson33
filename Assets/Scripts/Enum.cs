using System;

namespace Ai.manager
{
    [Flags]
    public enum ActionGroup: byte
    {
        Movement,
        TypeOfAttack
    }

    public enum ActionType : byte
    {
        Idle,
        Patrol,
        Attack,
        StrongAttack,
        FastAttack,
        DistanceAttack
    }

    public enum WeaponGroup : byte
    {
        LongDistance,
        ShortDistance
    }
    public enum WeaponType: byte
    {
        Knife,
        Sword,
        Bow
    }
}
