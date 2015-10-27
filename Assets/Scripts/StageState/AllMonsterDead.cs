using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class AllMonsterDead : StageState {
    public void StateStart(StageStateManager manager, StageViewControl view) {
        manager.stageCountPlusOne();
        if (manager.StageCount < manager.Info.StageNum) {
            manager.CurrentState = manager.ShowText;
        } else {
            view.showFinalText("Mission Complete!!");
        }
    }

    public void update(StageStateManager manager, StageViewControl view) {
        if (Input.touchCount > 0) {
            Application.LoadLevel("Start");
        }
    }
}
