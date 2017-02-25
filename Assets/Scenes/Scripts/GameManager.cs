using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    //Все ВС в игре
    public List<Suprime> suprimes = new List<Suprime>();
    //Все кристаллы в игре
    public List<Crystall> crystall = new List<Crystall>();

    private void Start() {
        SpawnCrystalls();
        SpawnUnits();
    }
    //Создание юнитов и расположение их на начальных точках
    void SpawnUnits() {
      
    }
    //Создание кристаллов и расположение их на начальных точках
    void SpawnCrystalls() {

    }

	
}
