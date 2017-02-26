using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suprime : BaseCharacter {
    [HeaderAttribute("Suprime Property")]
    public float distanceOfCapture = 10f; //Дистанция, при которой возмоден захват
    bool startCapture = false; //Если true, то игрок начал захват
    public float energy = 0f; //Текущее кол-во энергии
    public float maxEnergy = 50f; //Максимальное кол-во энергии у ВС
    public List<Crystall> crystalls; //Кристаллы под контролем игрока
    
    void Start() {
        
    }

    void setDistanceCapture() {
        if (currentCrystall != null) {
            distanceOfCapture = currentCrystall.radiusFirst;
        }
    }
    void Update () {
        StartCapture();//для отладки, автоматически начинает захват когда в зоне
        GetEnergy();
        setDistanceCapture();
    }
    public void GetEnergy() {
        if(currentCrystall != null) { 
            if (energy<maxEnergy) {
                energy += currentCrystall.getEnergy(this); 
            }
        }
        else {
            if(crystalls.Capacity != 0)
                energy += getNearCrystall().getEnergy(this);
        }
        energy = Mathf.Clamp(energy,0,maxEnergy);
    }

    //Возвращает самый близкий кристалл к ВС
    Crystall getNearCrystall() {
        float nearDistance = 0f;
        Crystall nearCrystall = null;
        foreach(Crystall crystall in crystalls) {
            float distance = Vector3.Distance(this.transform.position,crystall.transform.position);
            if(nearDistance==0){
                nearDistance = distance;
                nearCrystall = crystall;
            }
            else if(distance<nearDistance) {
                nearDistance = distance;
                nearCrystall = crystall;
            }
        }
        return nearCrystall;
    }
    
    //Начать захват если возможно
    void StartCapture() {
        if(currentCrystall != null) {
            float distance = Vector3.Distance(this.transform.position,currentCrystall.transform.position);
            if(distance <= distanceOfCapture && !startCapture) {
                if(currentCrystall.StartCapture(this))
                    startCapture = true;
            }
            if(distance > distanceOfCapture && currentCrystall.captureSuprime==this) {
                startCapture = false;
                currentCrystall.StopCapture();
            }
        }
    }
}
