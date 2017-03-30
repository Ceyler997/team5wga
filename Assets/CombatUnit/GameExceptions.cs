using System;

class UnitHaveNoMasterException : Exception{ } // Exception in case unit have no master

class NoObjectsInsideRadiusException : Exception { } // Exception in case trying to get object in the radius, when there is no objects inside

class UndefinedDefensiveUnitStateException : Exception { } // Exception in case undefined unit state

class NoTargetToProtectException : Exception { } // Exception in case no target to protect in protective unit behaviour

class NoSubjectForControlException : Exception { } // Exception in case no subject to control by behaviour
