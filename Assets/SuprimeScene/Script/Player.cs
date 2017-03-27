using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public string name; //Имя игрока
    public Suprime[] suprimes; //ВС, которыми владеет игрок
    public List<Crystall> crystalls; //Кристалы, которыми владеет игрок
    public GameManager manager;
    // Use this for initialization
    public GameObject P_Suprime;
    void Initialization () {
        suprimes = new Suprime[manager.MaxSuprimeAmount];
        crystalls = new List<Crystall>();
        Suprime s = Instantiate(P_Suprime, transform.position, Quaternion.identity).GetComponent<Suprime>();
        s.setPlayer(this);
    }
	//возвращает имя игрока
    public string GetName { get {return name; } }
	//Возвращает массив кристаллов принадлежавших игроку
	public Suprime[] GetSuprimes { get { return suprimes; } }
    //Возвращает массив юнитов принадлежавших игроку
    public List<Crystall> GetCrystalls{ get {return crystalls;} }
    public string PlayerName { get{return name;}  set { name = value; } }

    //Возвращает кол-во ВС под контролем игрока
    public int getNumOfSuprime() { return suprimes.Length; }
	//Добавляет ВС в массив suprimes
	public void addSuprime(Suprime suprime) {
		if(getNumOfSuprime() <= manager.MaxSuprimeAmount) {
            suprimes[getNumOfSuprime() - 1] = suprime;
        }
	}
	public void addCrystall(Crystall crystall) {
        crystalls.Add(crystall);
    }

    public void setManager(GameManager manager) {
        this.manager = manager;
        Initialization();
    }

    public GameManager getManager { get { return manager; } }

    // Update is called once per frame
    void Update () {
		
	}
}
