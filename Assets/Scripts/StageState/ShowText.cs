using System;
using UnityEngine;

public class ShowText : StageState {
    float startShowStageNumTime;

    public void StateStart(StageStateManager manager, StageViewControl view) {
        view.showOrHideStageNumText(true, (manager.StageCount + 1) + "/" + manager.Info.StageNum);
        startShowStageNumTime = Time.time;
    }

    public void update(StageStateManager manager, StageViewControl view) {
        if (Time.time - startShowStageNumTime > 2) {
            view.showOrHideStageNumText(false);
            manager.CurrentState = manager.ShowEnemy;
        }
    }
}
