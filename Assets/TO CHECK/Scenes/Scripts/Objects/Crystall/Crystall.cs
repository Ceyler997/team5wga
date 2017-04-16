//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//public class Crystall : GameObjectEntity {

//	public enum States {
//		Stopped, //Кристалл остановлен и не вырабатывает энергию
//		Runing //Кристалл вырабатывает энергию
//	};
	
//	public States state = States.Stopped;

//	[Header("Main Settings")]
//	public int level = 1;   //Уровень кристалла
//	public BelongPlayer belongPlayer = BelongPlayer.Player_0; //Показывает кому принадлежит кристалл, о умолчанию принадлежит нейтралу
//	public Suprime captureSuprime; //тот кто захватывает кристалл

//	[Space]
//	[Header("Energy Settings:")]
//    public float energy; //Кол-во энергии в кристалле
//    public float maxEnergy = 100f; //Максимальное кол-во энергии
//    public float incrEnergy = 2f; //скорость накапливания энергии кристалом за 1с
//	public float speedTransferEnergy = 1f; //скорость передачи энергии в пределах радиуса
//	public float energyRatio = 0.5f; //Коэффициент, на который умножается значение speedTransferEnergy, если ВС находится за границей кристалла
//	public float energyPerSecond;

//	[Space]
//	[Header("Capture Settings:")]
//	public float radiusFirst = 5f; //Размер первого радиуса
//    public float radiusSecond = 10f; //Размер второго радиуса
//	public float timeToSpawnEnemy = 100f; //Время спауна врага вокруг нейтрального кристалла
//	private SphereCollider radius; //Радиус А
//	public float timeToCapture = 10f; //Время захвата в сек. 
//	public float curentTimeToCapture; //текущее время захвата
//	public  List<Collider> unitsList = new List<Collider>();//Массив юнитов, которые пересекли радиус A
//	public bool bStartCapture = false; //Если захват начался, то true
//	private Renderer texture;
//	//public List<Suprime> suprimeList = new List<Suprime>(); //Список ВС, которые в радиусе действия Кристалла, необходим для передачи энергии
//	public List<Material> materials = new List<Material>();

//    void Start() {
//		energy = 0f;
//		radius = gameObject.AddComponent<SphereCollider>();
//		radius.radius = radiusSecond; 	
//		radius.isTrigger = true;
//		texture  = GetComponent<Renderer>();
//	}
//	//Персонаж входит в Кристалл
//	void OnTriggerEnter(Collider other) {
//		if(!unitsList.Contains(other)) { 
//			unitsList.Add(other);
//            //AddListSuprime();
//            BaseObject unit = other.gameObject.GetComponent<BaseObject>();
//            if (unit is Suprime) {
//                ((Suprime) unit).CurentCrystall = this;
//            }
//		}
//	}

//	//Нахождение всех ВС в списке юнитов и добавление их в список suprimeList
//	/*void AddListSuprime() {
//		suprimeList.Clear();
//		foreach (Collider item in unitsList) {
//			Suprime suprime = item.gameObject.GetComponent<Suprime>();
//			if(suprime != null)
//				if(suprime.belogPlayer==belongPlayer)
//					suprimeList.Add(suprime);
//		}
//	}*/
	
//	//Персонаж покидает кристалл
//	void OnTriggerExit(Collider other) {
//		RemoveFromTriggerList(other.gameObject.GetComponent<BaseObject>());
//	//	AddListSuprime();
//	}

//	//Удаление юнита из списа 
//	public void RemoveFromTriggerList(BaseObject unit) {
//		unitsList.Remove(unit.GetComponent<Collider>());
//        if (unit is Suprime) {
//            ((Suprime) unit).CurentCrystall = null;
//        }
//	}

//    //Проверяет окружение кристалла, и если рядом нет союзников то переключает переменную bCanCapture в true

//    public bool CheckCapture()
//    {
//        return true;
//    }
//    /*		if(!bStartCapture) {
//                foreach(Collider other in unitsList) {
//                    BaseCharacter unit = other.gameObject.GetComponent<BaseCharacter>();
//                    if(unit.belogPlayer == belongPlayer) {
//                        curentTimeToCapture = 0f;
//                        return false;
//                    }
//                }
//            }
//            else { //Если идет захват
//                if(captureSuprime != null) {
//                    foreach(Collider other in unitsList) {
//                        BaseCharacter unit = other.gameObject.GetComponent<BaseCharacter>();
//                        if(captureSuprime.belogPlayer != unit.belogPlayer) {
//                            curentTimeToCapture = 0f;
//                            return false;
//                        }
//                    }	
//                }
//            }
//            return true;
//        }
//        */
//    //Остановка захвата
//    public void StopCapture() {
//		ChangeState();
//		bStartCapture = false;
//		curentTimeToCapture = 0f;
//		captureSuprime = null;
//	}
	
//	//Начало захвата кристалла
//	public bool StartCapture(Suprime captureSuprime) {
//		if(!bStartCapture && this.captureSuprime==null && CheckCapture()) {
//			state = States.Stopped;
//			bStartCapture = true;
//			curentTimeToCapture = 0f;
//			this.captureSuprime = captureSuprime;
//		}
//		return false;
//	}
	
//	// Update is called once per frame
//	void Update () {
//		if(state == States.Runing) { 
//			IncrEnergyEverySecond(); //Увеличение энергии
//			//TransferEnergy();
//		}
//		if(CheckCapture()) { //Если условия захвата соблюдены
//			if(bStartCapture) { //Если игрок нажал на кнопку Захвата
//				curentTimeToCapture += 1 * Time.deltaTime;
//				if(curentTimeToCapture >= timeToCapture) { //захват успешен
//					ChangeBelong();
//				}
//			}
//		}
//	}
///*	void TransferEnergy() {
//		if(suprimeList.Count!=0)
//			energyPerSecond = (speedTransferEnergy / (float) suprimeList.Count);
//			foreach(Suprime unit in suprimeList) {
//				energy -= energyPerSecond * Time.deltaTime;
//				unit.AddEnergy(energyPerSecond * Time.deltaTime);
//			}
//	}*/
//	/* 
//	public float getEnergy(Suprime suprime) {
//		if(energy >= 0 && suprime.belogPlayer==belongPlayer) {
//			float distance = Vector3.Distance(this.transform.position,suprime.transform.position);
//			float ratio = 1; //коэффициент, в зависимости находится ли в радиусе кристалла юнит
//			if(distance >= radiusSecond) {
//				ratio *= energyRatio; 
//			}
//			float transferEnergy = (speedTransferEnergy * ratio) * Time.deltaTime;
//			energy -= transferEnergy;
//			energy = Mathf.Clamp(energy,0f,maxEnergy);
//			return transferEnergy;
//		}
//		return 0;
//	}
//*/

//	//Смена владельца
//	private void ChangeBelong() { 
//		if(captureSuprime != null) {
////			this.belongPlayer = captureSuprime.belogPlayer;
//			ChangeState();//изменяем состояние
//			StopCapture();
//			texture.sharedMaterial = materials[(int)belongPlayer];
//		//	AddListSuprime();
//		}
//	}

//	//Изменение состояния генерации энергии в Кристалле
//	private void ChangeState() {
//		if(belongPlayer == BelongPlayer.Player_0)
//			state = States.Stopped;
//		else
//			state = States.Runing;
//	}

//	//Увеличение энергии каждую секунду
//	private void IncrEnergyEverySecond() {
//		energy += incrEnergy * Time.deltaTime;
//		energy = Mathf.Clamp(energy,0,maxEnergy);
//	}
//}
