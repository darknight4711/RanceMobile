using NUnit.Framework;
using NSubstitute;
using UnityEngine;

public class StageStateTest {
    [Test]
    public void testShowText() {
        //ARRANGE
        StageStateManager mockSSM = Substitute.For<StageStateManager>();
        StageViewControl mockSVC = Substitute.For<StageViewControl>();

        mockSSM.StageCount.Returns(0);
        mockSSM.ShowEnemy.Returns(new ShowEnemy());
        mockSSM.Info.Returns(new StageInfo("TutorialStage"));
        
        //ACT
        StageState showText = new ShowText();
        showText.StateStart(mockSSM, mockSVC);

        //TEST
        mockSVC.Received().showOrHideStageNumText(true, "1/3");
        showText.update(mockSSM, mockSVC);
        Assert.That(mockSSM.CurrentState is ShowEnemy);
        mockSVC.Received().showOrHideStageNumText(false);
    }
}
