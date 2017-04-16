using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public string playerName; //Имя игрока
    public Suprime[] suprimes; //ВС, которыми владеет игрок
    private int countSuprime = 0; //Текущее кол-во ВС
    public List<Crystal> crystalls; //Кристалы, которыми владеет игрок

    // Use this for initialization
    public GameObject P_Suprime;
	// Возвращает имя игрока
    public string GetName { get {return name; } }
	// Возвращает массив кристаллов принадлежавших игроку
	public Suprime[] GetSuprimes { get { return suprimes; } }
    // Возвращает массив юнитов принадлежавших игроку
    public List<Crystal> GetCrystalls{ get {return crystalls;} }
    public string PlayerName { get{return name;}  set { name = value; } }

    // Возвращает кол-во ВС под контролем игрока
    public int getNumOfSuprime() { return countSuprime; }

	// Добавляет ВС в массив suprimes
	public void addSuprime(Vector3 position) {
        if(getNumOfSuprime() < GameConf.maxSuprimeAmount) {
            GameObject gameObject = Instantiate(P_Suprime, position, Quaternion.identity);
            Suprime suprime = gameObject.GetComponent<Suprime>();
            suprime.setPlayer(this);
            suprimes[getNumOfSuprime()] = suprime;
            countSuprime++;
        }
	}
	public void addCrystall(Crystal crystall) {
        crystalls.Add(crystall);
    }

    public void setup() {
        suprimes = new Suprime[GameConf.maxSuprimeAmount];
        crystalls = new List<Crystal>();
        addSuprime(transform.position);
    }
}
