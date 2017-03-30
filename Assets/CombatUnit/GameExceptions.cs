using System;

#region BaseObject exceptions

class NoObjectsInsideRadiusException : Exception { } // Исключение при попытке получить цель из радиуса при пустом радиусе

#region Unit exceptions

class UnitHaveNoMasterException : Exception { } // Исключение приотсутствии мастера у юнита
#endregion
#endregion

#region Behaviour exceptions

#region DefensiveBehaviour exceptions

class UndefinedDefensiveUnitStateException : Exception { } // Исключение при неизвестном состоянии юнита в режиме защиты

class NoTargetToProtectException : Exception { } // Исключение при отсутствии цели для защиты у юнита в режиме защиты
#endregion

class NoSubjectForControlException : Exception { } // Исключение при отстутствии цели для контроля у поведения
#endregion

#region General components exceptions

class SystemNotSettedUpException : Exception { } // Исключение при попытке использовать не настроенную систему
#endregion