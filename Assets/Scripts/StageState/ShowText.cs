using System;
using UnityEngine;

class ShowText : StageState {
    float startShowStageNumTime;

    public void StateStart(StageStateManager manager) {
        manager.StageNum.transform.SetAsLastSibling();
        manager.StageNum.text = (manager.StageCount + 1) + "/" + manager.Info.StageNum;
        manager.StageNum.enabled = true;
        startShowStageNumTime = Time.time;
    }

    public void update(StageStateManager manager) {
        if (Time.time - startShowStageNumTime > 2) {
            manager.StageNum.enabled = false;
            manager.CurrentState = manager.ShowEnemy;
        }
    }
}
