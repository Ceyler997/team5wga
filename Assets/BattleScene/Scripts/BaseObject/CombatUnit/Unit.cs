using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CombatSystem))]
public class Unit : BaseObject, IFightable {

    #region private fields

    private Movement movementAgent; // компонент передвижения
    private Health healthSystem; // компонент здоровья
    private CombatSystem combatSystem; // компонент системы боёвки
    private UnitAIBehaviour behaviour; // Компонент поведения
    private Suprime master; // Призвавший юнита ВС

    private List<IDeathObserver> deathObservers; //список наблюдателей
    #endregion

    #region getters and setters

    public Movement MovementAgent {
        get {return movementAgent;}
        set {movementAgent = value;}
    }

    public Health HealthSystem {
        get {return healthSystem;}
        set {healthSystem = value;}
    }

    public CombatSystem CombatSys {
        get { return combatSystem; }
        set { combatSystem = value; }
    }

    public UnitAIBehaviour Behaviour {
        get { return behaviour; }
        set {
            if (behaviour != null) {
                behaviour.End();
            }

            if (value != null) {
                value.Start();
            } else {
                throw new UnitHaveNoBehaviourException();
            }

            behaviour = value;
        }
    }

    public Suprime Master {
        get { return master; }
        set {
            if (value == null) {
                throw new UnitHaveNoMasterException();
            }

            master = value;
        }
    }

    Vector3 IFightable.Position {
        get { return transform.position; }
    }

    private List<IDeathObserver> DeathObservers {
        get {return deathObservers;}
        set {deathObservers = value;}
    }
    #endregion

	#region PunBehaviour methods

	public override void OnPhotonInstantiate (PhotonMessageInfo info)
	{
		int masterID = (int)photonView.instantiationData [0];
		Suprime unitMaster = PhotonView.Find (masterID).GetComponent<Suprime> ();
		setupUnit(unitMaster);
		//unit.Behaviour = new UnitAgressiveBehaviour(unit);
		Attach(unitMaster);
		unitMaster.Units.Add (this);
	}
	#endregion

    #region MonoBehaviour methods

    new public void Update() { // Проверяем, есть ли мастер у юнита
        base.Update();
        if (Master == null) {
            throw new UnitHaveNoMasterException();
        }

        if (Behaviour == null) {
            throw new UnitHaveNoBehaviourException();
        }
		if(photonView.isMine){
			Behaviour.UpdateState();
		}
    }

    public void OnDestroy() {
		while(DeathObservers.Count != 0) { // Используется такая конструкция, т.к. список изменяется в процессе обхода
            // Подписчик по своим внутренним алгоритмам может как отписаться, так и не отписаться
            // поэтому мы берём отдельного подписчика, а не обращаемся по индексу (первый объект может измениться)
            IDeathObserver observer = DeathObservers [0];
            observer.onSubjectDeath(this);
            Detach(observer); // При смерти объекта отписываем его подписчиков
        }

        CombatSys.Target = null; // Убираем цель, оповещая, что мы больше не атакуем предыдущую цель
	}
    #endregion

    #region public methods

    public void setupUnit(Suprime master) {
        base.setupBaseObject(master.ControllingPlayer,
            GameConf.unitReactRadius,
            GameConf.unitDetectRadius);

        MovementAgent = GetComponent<Movement>();
        MovementAgent.setupSystem(GameConf.unitMoveSpeed);

        HealthSystem = GetComponent<Health>();
        HealthSystem.setupSystem(GameConf.unitStartHealth,
            GameConf.unitMaxHealth,
            GameConf.unitBasicRegenSpeed,
            this);

        CombatSys = GetComponent<CombatSystem>();
        CombatSys.setupSystem(GameConf.unitDamage,
            GameConf.unitCritDamage,
            GameConf.unitBasicCritChance,
            GameConf.unitAttackRadius,
            GameConf.unitAttackSpeed);

        Master = master;

        DeathObservers = new List<IDeathObserver>();

        Behaviour = new UnitProtectiveBehaviour(this, master);
    }
    #endregion

    #region IDeathSubject implementation

    public void Attach(IDeathObserver observer) {
        DeathObservers.Add(observer);
    }

    public void Detach(IDeathObserver observer) {
        DeathObservers.Remove(observer);
    }

    public void SubjectDeath() {		
        PhotonNetwork.Destroy(gameObject);
    }
    #endregion
}
