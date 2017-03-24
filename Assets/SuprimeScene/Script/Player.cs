using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private string name; //Имя игрока
    private Suprime[] suprimes; //ВС, которыми владеет игрок
    private List<Crystall> crystalls; //Кристалы, которыми владеет игрок
    private GameManager manager;
    Player(string name) {
        this.PlayerName = name;
        suprimes = new Suprime[manager.MaxSuprimeAmount];
        crystalls = new List<Crystall>();
    }
	// Use this for initialization
    void Start () {
		
	}
	//возвращает имя игрока
    public string Name { get; }
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

    public GameManager getManager { get { return manager; } }
    // Update is called once per frame
    void Update () {
		
	}
}
