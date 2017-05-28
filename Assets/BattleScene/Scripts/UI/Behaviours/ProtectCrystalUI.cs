
using UnityEngine;

public class ProtectCrystalUI : BehaviourUI {
    public override void ApplyBehaviour() {
        if (IsEnable()) {
            Suprime suprime = CharactersController.Instance.SelectedUnit.Subject;
            Crystal closestCrystal = suprime.ControllingPlayer.Crystals [0];

            foreach(Crystal crys in suprime.ControllingPlayer.Crystals) {
                if(Vector3.Distance(closestCrystal.Position, suprime.Position) 
                    > Vector3.Distance(crys.Position, suprime.Position)) {
                    closestCrystal = crys;
                }
            }

            foreach (Unit unit in suprime.Units) {
                unit.Behaviour = new UnitProtectiveBehaviour(unit, closestCrystal);
            }

            CharactersController.Instance.SelectedUnit.UnitsState = BehaviourStates.CRYSTAL_PROTECT;
        }
    }

    protected override bool IsEnable() {
        return CharactersController.Instance.SelectedUnit != null
            && CharactersController.Instance.SelectedUnit.Subject.Units.Count > 0
            && CharactersController.Instance.SelectedUnit.UnitsState != BehaviourStates.CRYSTAL_PROTECT;
    }
}

