using System;
using System.Collections.Generic;
using UnityEngine;

class AllPlayerDead : StageState {
    public void StateStart(StageStateManager manager) {
        manager.showFinalText("You Dead");
    }

    public void update(StageStateManager manager) {
        if (Input.touchCount > 0) {
            Application.LoadLevel("Start");
        }
    }
}
