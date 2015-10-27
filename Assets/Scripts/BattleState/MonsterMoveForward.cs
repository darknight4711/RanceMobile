using System;
using System.Collections.Generic;
public class MonsterMoveForward : BattleState {

    public void back(BattleStateManager manager, BattleViewControl view) {
        throw new NotImplementedException();
    }

    public void StateStart(BattleStateManager manager, BattleViewControl view) {
        for (int i = 0; i < view.MonsterPanelList.Count; i++) {
            view.MonsterPanelList[i].playMoveForwardAnimation(i);
        }
    }

    public void update(BattleStateManager manager, BattleViewControl view) {
        if (isMonsterMoveForwardFinish(view)) {
            manager.CurrentState = manager.CheckPlayerDead;
        }
    }

    private bool isMonsterMoveForwardFinish(BattleViewControl view) {
        bool isEnemyMoveFinish = true;
        foreach (MonsterPanelControl mpc in view.MonsterPanelList) {
            if (!mpc.isMoveForwardAnimationFinish())
                isEnemyMoveFinish = false;
        }
        return isEnemyMoveFinish;

    }
}
