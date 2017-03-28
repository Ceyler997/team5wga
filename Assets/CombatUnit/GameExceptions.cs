using System;

class UnitHaveNoMasterException : Exception{ } // Exception in case unit have no master

class NoObjectsInsideRadiusException : Exception { } // Exception in case trying to get object in the radius, when there is no objects inside
