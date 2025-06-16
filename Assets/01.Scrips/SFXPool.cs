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

public class SFXPool : MonoBehaviour
{
    public AudioClip[] MagicSFXClips = new AudioClip[4];
    public AudioClip[] BuffSFXClips = new AudioClip[4];
    public AudioClip[] PhysicsSFXClips = new AudioClip[2];
    public AudioClip[] ReactSFXClips = new AudioClip[3];

    public AudioClip[] UISFXClips = new AudioClip[8];

    public AudioClip Walk;
    
    public AudioClip[] Stage1Audio = new AudioClip[4];
    public AudioClip[] Stage2Audio = new AudioClip[4];

    public AudioClip[][] BackGroundAudio = new AudioClip[2][];

    private void Awake()
    {
        BackGroundAudio[0] = Stage1Audio;
        BackGroundAudio[1] = Stage2Audio;
    }
}
