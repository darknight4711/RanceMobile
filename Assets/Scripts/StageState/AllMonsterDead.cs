using System;
using System.Collections.Generic;
using UnityEngine.UI;

class AllMonsterDead : StageState {
    public void StateStart(StageStateManager manager) {
        manager.stageCountPlusOne();
        if (manager.StageCount < manager.Info.StageNum) {
            manager.CurrentState = manager.ShowText;
        } else {
            manager.showFinalText("Mission Complete!!");
        }
    }

    public void update(StageStateManager manager) {
        throw new NotImplementedException();
    }
}
