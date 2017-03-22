using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suprime : MonoBehaviour {
    [HeaderAttribute("Suprime Property")]
    public float distanceOfCapture = 10f; //Дистанция, при которой возмоден захват
    bool startCapture = false; //Если true, то игрок начал захват
    public float energy = 0f; //Текущее кол-во энергии
    public float maxEnergy = 50f; //Максимальное кол-во энергии у ВС
    public List<Crystall> crystalls; //Кристаллы под контролем игрока
    
    void Start() {
        
    }
}
