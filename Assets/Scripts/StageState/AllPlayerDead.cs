using System;
using System.Collections.Generic;
using UnityEngine;

class AllPlayerDead : StageState {
    public void StateStart(StageStateManager manager, StageViewControl view) {
        view.showFinalText("You Dead");
    }

    public void update(StageStateManager manager, StageViewControl view) {
        if (Input.touchCount > 0) {
            Application.LoadLevel("Start");
        }
    }
}
