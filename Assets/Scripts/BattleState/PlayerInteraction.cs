using System;
using System.Collections.Generic;
public class PlayerInteraction : BattleState {
    public void StateStart(BattleStateManager manager, BattleViewControl view) {
        view.StartBattleButton.interactable = true;
        for (int i = 0; i < manager.CharaList.Count; i++) {
            Chara chara = manager.CharaList[i];
            if (chara.Dead)
                continue;
            chara.startInteraction();
        }
    }

    public void update(BattleStateManager manager, BattleViewControl view) {
        
    }

    public void back(BattleStateManager manager, BattleViewControl view) {
        throw new NotImplementedException();
    }
}
