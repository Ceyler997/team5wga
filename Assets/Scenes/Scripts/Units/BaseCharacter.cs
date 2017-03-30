using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Удалить эту х**ню старую, как только все референсы будут убранны
public class BaseCharacter : GameObjectEntity {
	[HeaderAttribute("Base Character property")]
    public float health = 100f; // здоровье
    public float movementSpeed = 9f; //скорость передвижения
	public BelongPlayer belogPlayer = BelongPlayer.Player_0;
	public Crystall currentCrystall;//Кристалл в пределах которого находится ВС
	
	void OnDestroy() {
		Debug.Log("Live like a man, die like a warrior, be remebered as a hero!");	
		if(currentCrystall!=null)
			currentCrystall.RemoveFromTriggerList(this);
		Destroy(this.gameObject);
	}

	public void setCurCrystall(Crystall crystall) {
		currentCrystall = crystall;
	}
}
