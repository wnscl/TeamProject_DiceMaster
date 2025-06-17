using UnityEngine;

public enum MagicSFXEnum
{
    Fire,
    Ice,
    Thunder,
    Wind,
}

public enum BuffSFXEnum
{
    Heal,
    AtkBuff,
    DefBuff,
    Debuff
}

public enum PyhsicsSFXEnum
{
    Slash,
    Bite    
}
public enum ReactSFXEnum
{
    Hit,
    Evade,
    Death
}

public enum UISFXEnum
{
    Confirm,
    Denied,
    UseItem,
    Equip,
    Unequip,
    BuySell,
    Pause,
    Unpause
}

public enum Car
{
    door,
    engine,
    drive
}

public class SFXPool : MonoBehaviour
{
    public AudioClip[] MagicSFXClips = new AudioClip[4];
    public AudioClip[] BuffSFXClips = new AudioClip[4];
    public AudioClip[] PhysicsSFXClips = new AudioClip[2];
    public AudioClip[] ReactSFXClips = new AudioClip[3];

    public AudioClip[] UISFXClips = new AudioClip[8];

    public AudioClip Walk;

    public AudioClip[] battleAudio = new AudioClip[4];
    public AudioClip[] Stage1Audio = new AudioClip[4];
    public AudioClip[] Stage2Audio = new AudioClip[4];
    public AudioClip[] Stage3Audio = new AudioClip[4];

    public AudioClip[][] BackGroundAudio = new AudioClip[3][];

    public AudioClip[] CarSFX = new AudioClip[3];

    private void Awake()
    {
        BackGroundAudio[0] = Stage1Audio;
        BackGroundAudio[1] = Stage2Audio;
        BackGroundAudio[2] = Stage3Audio;
    }
}
