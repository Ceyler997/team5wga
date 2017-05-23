﻿using UnityEngine;

public class MagicManager : MonoBehaviour {

    public Teleport Teleport { get; private set; }
    public CaptureCrystal CaptureCrystal { get; private set; }
    public HealMagic Heal { get; private set; }
    public RedMagic Red { get; private set; }
    public WhiteMagic White { get; private set; }
    public BlackMagic Black { get; private set; }
    public LevelUpCrystal LvlUpCrys { get; private set; }

    public bool IsCasting { get; set; } // Кастует ли супрайм в данный момент
    public bool IsActive { get; set; }  // Активен ли какой-то эффект в данный момент

    public void Setup(Suprime caster) {

        Teleport = GetComponentInChildren<Teleport>();
        Teleport.Setup(caster);

        CaptureCrystal = GetComponentInChildren<CaptureCrystal>();
        CaptureCrystal.Setup(caster);

        Heal = GetComponentInChildren<HealMagic>();
        Heal.Setup(caster);

        Red = GetComponentInChildren<RedMagic>();
        Red.Setup(caster);

        White = GetComponentInChildren<WhiteMagic>();
        White.Setup(caster);

        Black = GetComponentInChildren<BlackMagic>();
        Black.Setup(caster);

        LvlUpCrys = GetComponentInChildren<LevelUpCrystal>();
        LvlUpCrys.Setup(caster);
    }
}
