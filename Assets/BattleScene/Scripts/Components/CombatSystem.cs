using UnityEngine;

public class CombatSystem : MonoBehaviour, IDeathObserver, IPunObservable {

    #region private variables
    // все поля в свойствах (см. геттеры и сеттеры)

    private float damage; // базовый урон юнита за удар
    private float critDamage; // дополнительный урон от крита
    private float frameDamage; // урон, нанесённый за текущий кадр

    private float critChance; // базовая вероятность крита, от 0.0 до 1.0
    private BattleMagicColor currentMagicColor; // текущий цвет магии, наложенной на юнита

    private float attackRadius; // радиус, в котором юнит атакует

    private float attackSpeed; // время отката атаки
    private float nextAttackTime; // минимальное время начала следующей атаки

    private IFightable target; // цель атаки
    private bool isUnderAttack; // атакуют ли цель

    private bool isSettedUp;
    #endregion

    #region properties

    private IFightable Subject { get; set; }

    public float Damage {
        get { return damage; }
        set { damage = value; }
    }

    public float CritDamage {
        get { return critDamage; }
        set { critDamage = value; }
    }

    public float CritChance {
        get { return critChance; }
        set { critChance = Mathf.Clamp(value, 0.0f, 1.0f); }
    }

    public BattleMagicColor CurrentMagicColor {
        get { return currentMagicColor; }
        set { currentMagicColor = value; }
    }

    public float AttackRadius {
        get { return attackRadius; }
        set { attackRadius = value; }
    }

    public float AttackSpeed {
        get { return attackSpeed; }
        set { attackSpeed = value; }
    }

    private float NextAttackTime {
        get { return nextAttackTime; }
        set { nextAttackTime = value; }
    }

    public IFightable Target {
        get { return target; }
        set {
            if (target != value) {

                if (target != null) {
                    target.Detach(this);
                    target.CombatSys.IsUnderAttack = false; // Указываем, что мы больше не атакуем текущую цель
                }

                if (value != null) {
                    value.Attach(this);
                }

                target = value;
            }
        }
    }

    public bool IsUnderAttack {
        get { return isUnderAttack; }
        set { isUnderAttack = value; }
    }

    public bool IsSettedUp {
        get { return isSettedUp; }
        set { isSettedUp = value; }
    }
    #endregion

    #region private methods

    // рассчитывает добавочный крит в текущий момент
    private float GetCrit() {
        float curCritChance = CritChance;

        if (CurrentMagicColor == BattleMagicColor.NO_COLOR) {
            return 0;
        }

        if (CurrentMagicColor.CounterMagic == Target.CombatSys.CurrentMagicColor) {
            curCritChance -= Target.CombatSys.CritChance;
        }

        float dice = Random.Range(0.0f, 1.0f);

        if (dice < curCritChance) {
            return CritDamage;
        } else {
            return 0;
        }
    }

    private void DealDamage(float damage) {
        Target.HealthSystem.getDamage(damage);
        Target.CombatSys.Attacked(Subject);
    }
    #endregion

    #region MonoBehaviour methods

    public void Start() {
        CurrentMagicColor = BattleMagicColor.NO_COLOR;
        NextAttackTime = Time.time;
        IsUnderAttack = false;
    }

    private void Update() {
        if (!IsSettedUp) {
            throw new SystemIsNotSettedUpException();
        }
    }
    #endregion

    #region public methods

    // Функция для настройки системы после инициализации
    public void SetupSystem(IFightable subject, float basicDmg, float critDmg, float critChance, 
        float atkRadius, float atkSpeed) {
        Subject = subject;

        Damage = basicDmg;
        CritDamage = critDmg;
        CritChance = critChance;

        AttackRadius = atkRadius;
        AttackSpeed = atkSpeed;

        currentMagicColor = BattleMagicColor.NO_COLOR;
        isSettedUp = true;
    }

    // Если юнита атакуют, то его цель меняется на атакующего
    public void Attacked(IFightable attacker) {
        IsUnderAttack = true;
        Target = attacker;
    }

    // возвращает true, если цель МОЖЕТ атаковать текущую цель (т.е. не учитывая откат атаки)
    public bool Attack() {
        if (Vector3.Distance(transform.position, Target.Position) < AttackRadius) { // проверка расстояния

            if (Time.time >= NextAttackTime) { // проверка отката
                // DEBUG
                Debug.DrawLine(transform.position, Target.Position, Color.white, 0.2f);

                frameDamage = Damage + GetCrit(); // получение наносимого урона
                
                if(!PhotonNetwork.connected && OfflineGameManager.Instance != null) {
                    DealDamage(frameDamage);
                }

                NextAttackTime = Time.time + AttackSpeed; // устанавливается откат

            }
            return true;
        }
        return false;
    }

    // получение уведомление о доступной цели. Если цели нет, то устанавливается полученная
    public void GetTargetNotification(IFightable target) {
        if (Target == null) {
            Target = target;
        }
    }
    #endregion

    #region IDeathObserver implementation

    public void OnSubjectDeath(IDeathSubject subject) {
        if (subject == Target) {
            Target = null;
        } else {
            throw new WrongDeathSubsciptionException();
        }
    }
    #endregion

    #region IPunObservable implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            stream.SendNext(currentMagicColor.MagicID);

            if (Target != null) {
                stream.SendNext(Target.ID);
            } else {
                stream.SendNext(0);
            }

            stream.SendNext(frameDamage);

            frameDamage = 0;
        } else {
            int magicId = (int) stream.ReceiveNext();
            if (magicId != currentMagicColor.MagicID) {
                currentMagicColor = BattleMagicColor.getMagicByID(magicId);
            }

            int receivedID = (int) stream.ReceiveNext();
            if (receivedID == 0) {
                Target = null;
            } else if (Target == null || Target.ID != receivedID) {
                PhotonView targetView = PhotonView.Find(receivedID);
                if (targetView != null) {
                    Target = targetView.GetComponent<IFightable>();
                } else {
                    Target = null;
                }
            }

            float receivedDamage = (float) stream.ReceiveNext();
            if (receivedDamage > 0 && Target != null) {
                DealDamage(receivedDamage);
                // DEBUG
                Debug.DrawLine(transform.position, Target.Position, Color.white, 0.2f);
            }
        }
    }
    #endregion
}

public interface IFightable : IDeathSubject {
    CombatSystem CombatSys { get; }
    Vector3 Position { get; }
    Health HealthSystem { get; }
    int ID { get; }
}

public interface IDeathObserver {
    void OnSubjectDeath(IDeathSubject subject);
}

public interface IDeathSubject {
    void Attach(IDeathObserver observer);
    void Detach(IDeathObserver observer);
    void SubjectDeath();
}