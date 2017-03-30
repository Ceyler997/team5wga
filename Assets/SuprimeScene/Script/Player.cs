using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public string playerName; //Имя игрока
    public Suprime[] suprimes; //ВС, которыми владеет игрок
    private int countSuprime = 0; //Текущее кол-во ВС
    public List<Crystall> crystalls; //Кристалы, которыми владеет игрок
    public GameManager manager;
    // Use this for initialization
    public GameObject P_Suprime;
    void Initialization () {
        suprimes = new Suprime[manager.MaxSuprimeAmount];
        crystalls = new List<Crystall>();
        addSuprime(transform.position);
    }
	//возвращает имя игрока
    public string GetName { get {return name; } }
	//Возвращает массив кристаллов принадлежавших игроку
	public Suprime[] GetSuprimes { get { return suprimes; } }
    //Возвращает массив юнитов принадлежавших игроку
    public List<Crystall> GetCrystalls{ get {return crystalls;} }
    public string PlayerName { get{return name;}  set { name = value; } }

    //Возвращает кол-во ВС под контролем игрока
    public int getNumOfSuprime() { return countSuprime; }
	//Добавляет ВС в массив suprimes
	public void addSuprime(Vector3 position) {
        if(getNumOfSuprime() < manager.MaxSuprimeAmount) {
            Suprime suprime = Instantiate(P_Suprime, position, Quaternion.identity).GetComponent<Suprime>();
            suprime.setPlayer(this);
            suprimes[getNumOfSuprime()] = suprime;
            countSuprime++;
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
