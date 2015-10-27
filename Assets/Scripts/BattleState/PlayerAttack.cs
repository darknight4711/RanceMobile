using System;
using System.Collections.Generic;
public class PlayerAttack : BattleState {
    BattleStateManager manager;

    public void back(BattleStateManager manager, BattleViewControl view) {
        removeCurrentOrderChara(manager);
        nextCharaAction();
    }

    public void StateStart(BattleStateManager manager, BattleViewControl view) {
        view.StartBattleButton.interactable = false;
        for (int i = 0; i < manager.CharaList.Count; i++) {
            Chara chara = manager.CharaList[i];
            if (chara.Dead)
                continue;
            chara.startBattle();
        }
        this.manager = manager;
        nextCharaAction();
    }

    public void update(BattleStateManager manager, BattleViewControl view) {
        Chara chara = manager.getOrderedObject(0);
        int index = manager.CharaList.IndexOf(manager.getOrderedObject(0));
        Skill currentPlaySkill = chara.getSkill(chara.CurrentSkill);
        if (currentPlaySkill.checkPlaySkillFinish(manager.CharaList, manager.MonsterList, index)) {
            manager.BackState = this;
            manager.CurrentState = manager.CheckEnemyDead;
        }
    }

    private void nextCharaAction() {
        if (manager.getOrderedCount() > 0) {
            manager.getOrderedObject(0).View.LightIcon();
            Chara chara = manager.getOrderedObject(0);
            int index = manager.CharaList.IndexOf(manager.getOrderedObject(0));
            Skill currentPlaySkill = chara.getSkill(chara.CurrentSkill);
            currentPlaySkill.playSkill(manager.CharaList, manager.MonsterList, index);
        } else {
            manager.CurrentState = manager.PlayerTurnPost;
        }
    }

    private void removeCurrentOrderChara(BattleStateManager manager) {
        if (manager.getOrderedCount() <= 0)
            return;
        Chara chara = manager.getOrderedObject(0);
        manager.removeOrder(chara);
        chara.View.DelightIcon();
    }
}
