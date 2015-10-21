﻿using System;
using NUnit.Framework;

public class ParserTest {
    [Test]
    public void TestStoryParser()
    {
        Story testStory = new Story("Scene1");
        Assert.That(testStory.Scenes.Count == 2);
        Assert.That(testStory.Background.ContainsKey("BGrass2"));
        Assert.That(testStory.Chara.ContainsKey("希爾"));

        Assert.That(testStory.Scenes[0].background == "BGuild2");
        Assert.That(testStory.Scenes[0].sentences[0].effect == Story.Sentence.SentenceEffect.CharaAppearLeft);
        Assert.That(testStory.Scenes[0].sentences[0].effectParam1 == "蘭斯");

        Story.Sentence sentence = testStory.Scenes[1].sentences[testStory.Scenes[1].sentences.Count-1];
        Assert.That(sentence.speaker == "蘭斯");
        Assert.That(sentence.index == 1);
        Assert.That(sentence.sentence == "我們必須趕快通過這裡到達利薩斯首都，途中的怪物都給我滾開!");

       
        foreach (Story.Sentence sen in testStory.Scenes[0].sentences) {
            if (sen.effect == Story.Sentence.SentenceEffect.None &&
                sen.speaker == null &&
                sen.sentence == null) {
                Assert.That(false);
            }
        }

        bool hasLoud = false;
        foreach (Story.Sentence sen in testStory.Scenes[0].sentences) {
            if (sen.effect == Story.Sentence.SentenceEffect.Loud)
                hasLoud = true;
        }
        if (!hasLoud) {
            Assert.That(false);
        }
    }

    [Test]
    public void TestSkillParser() {
        SkillFactory sf = new SkillFactory();

        Skill physicSkill = sf.CreateSkill("Chop");
        Assert.That(physicSkill is PhysicSkill);
        Assert.That(physicSkill.Name == "Chop");
        Assert.That(physicSkill.Cd == 1);
        Assert.That(physicSkill.Rate == 80);
        Assert.That(physicSkill.Description == "Give one enemy 80% ATK physic damage.");

        Skill healSkill = sf.CreateSkill("Heal");
        Assert.That(healSkill is HealSkill);
        Assert.That(healSkill.Name == "Heal");
        Assert.That(healSkill.Cd == 2);
        Assert.That(healSkill.Rate == 150);
        Assert.That(healSkill.Description == "Heal 150% ATK HP for least HP character.");

        Assert.That(sf.CreateSkill("Foo") is SimpleSkill);
    }

    [Test]
    public void TestCharaInfoParser() {
        CharaInfo ci = new CharaInfo("蘭斯");

        Assert.That(ci.Name == "蘭斯");
        Assert.That(ci.MaxHP == 3664);
        Assert.That(ci.MaxATK == 1752);
        Assert.That(ci.MaxDEF == 203);
        Assert.That(ci.SkillTree.childNode[0].skill.Name == "Chop");
        Assert.That(ci.SkillTree.childNode[2].childNode[0].skill.Name == "Sex");
    }

    [Test]
    public void TestMonsterInfoParser() {
        MonsterInfo mi = new MonsterInfo("Hony");

        Assert.That(mi.Name == "Hony");
        Assert.That(mi.Race == MonsterInfo.MonsterRace.Hony);
        Assert.That(mi.HP == 15);
        Assert.That(mi.ATK == 6);
        Assert.That(mi.DEF == 1);
        Assert.That(mi.getSkill(0).Name == "HonyCollide");
        Assert.That(mi.getSkill(0) is NormalMonsterSkill);
        Assert.That(mi.getSkill(0).Rate == 100);
    }

    [Test]
    public void TestStageInfoParser() {
        StageInfo si = new StageInfo("TutorialStage");

        Assert.That(si.Name == "Tutorial");
        Assert.That(si.StageNum == 3);
        Assert.That(si.MonsterList(0)[0].Name == "IkaMan");
        Assert.That(si.MonsterList(1)[1].Name == "Hony");
        Assert.That(si.MonsterList(2)[2].Name == "Roper");
    }
}