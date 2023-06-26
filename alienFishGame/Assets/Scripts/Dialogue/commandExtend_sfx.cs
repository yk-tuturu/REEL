using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class commandExtend_sfx : MonoBehaviour
{
    public FMODUnity.EventReference attackSuccessSFX;
    public FMODUnity.EventReference attackFailSFX;
    public FMODUnity.EventReference bossDefeatSFX;
    public FMODUnity.EventReference overlordSnarlSFX;

    // Start is called before the first frame update
    void Start()
    {
        commandManager.instance.commandData.Add("playAttackSuccess", new Action(playAttackSuccess));
        commandManager.instance.commandData.Add("playAttackFail", new Action(playAttackFail));
        commandManager.instance.commandData.Add("playBossDefeat", new Action(playBossDefeat));
        commandManager.instance.commandData.Add("playOverlordSnarl", new Action(playOverlordSnarl));
    }

    public void playAttackSuccess()
    {
        FMODUnity.RuntimeManager.PlayOneShot(attackSuccessSFX);
    }

    public void playAttackFail()
    {
        FMODUnity.RuntimeManager.PlayOneShot(attackFailSFX);
    }

    public void playBossDefeat()
    {
        FMODUnity.RuntimeManager.PlayOneShot(bossDefeatSFX);
    }

    public void playOverlordSnarl()
    {
        FMODUnity.RuntimeManager.PlayOneShot(overlordSnarlSFX);
    }
}
