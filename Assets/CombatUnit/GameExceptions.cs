using System;

#region BaseObject exceptions

#region Unit exceptions

public class UnitHaveNoMasterException : Exception { } // Исключение приотсутствии мастера у юнита
#endregion

#region Suprime exception

public class SuprimeHaveNoPlayerException : Exception { } // Исключение при использовании супрайма без прикреплённого игрока
#endregion

#region Crystal exceptions
public class UndefinedCrystalLevelException : Exception { } // Исключение при попытке получить данные по несуществущеему уровню кристалла
#endregion
#endregion

#region Behaviour exceptions

#region DefensiveBehaviour exceptions

public class UndefinedDefensiveUnitStateException : Exception { } // Исключение при неизвестном состоянии юнита в режиме защиты

public class NoTargetToProtectException : Exception { } // Исключение при отсутствии цели для защиты у юнита в режиме защиты
#endregion

public class NoSubjectForControlException : Exception { } // Исключение при отстутствии цели для контроля у поведения

public class UnitHaveNoBehaviourException : Exception { } // Исключение при отсутствии поведения
#endregion

#region Components exceptions

public class SystemIsNotSettedUpException : Exception { } // Исключение при попытке использовать не настроенную систему

public class NoUnitsInsideRadiusException : Exception { } // Исключение при попытке получить цель из радиуса при пустом радиусе

public class PlayerNotExistingException : Exception { } // Исключение при попытке получить игрока с несуществующим ID

public class TooMuchSuprimesException : Exception { } // Исключение при попытке создать больше ВС, чем разрешено параметрами

public class AttemptToManagerReassignmentException : Exception { } // Исключение при попытке переназначить менеджера

public class WrongDeathSubsciptionException : Exception { } // Исключение при неправильной подписке или попытке подписаться на null
#endregion