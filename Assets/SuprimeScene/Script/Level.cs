using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILeveable {
    void levelUp();
}
//Скрипт реализует прокачку уровня для ВС, Магии и Кристалла
public class Level : MonoBehaviour {
    ILeveable master;
    public int maxLevel; //Максимальный уровень
    public int curentLevel; //Текущий уровень
    
	public void setup(int maxLevel, int curentLevel) {
        this.maxLevel = maxLevel;
        this.curentLevel = curentLevel;
    }
    public void levelUp(){
        if (curentLevel < maxLevel)
            curentLevel += 1;
    }
}
