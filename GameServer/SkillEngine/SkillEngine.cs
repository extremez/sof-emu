﻿using Communicate;
using Communicate.Interfaces;
using Communicate.Logics;
using Data.Enums;
using Data.Interfaces;
using Data.Structures.Creature;
using Data.Structures.Npc;
using Data.Structures.Player;
using Data.Structures.SkillEngine;
using GameServer.Networks.Packets.Response;
using System;
using System.Collections.Generic;
using Utility;

namespace GameServer.SkillEngine
{
    internal class SkillEngine : Global, ISkillEngine
    {
        public static AbnormalityProcessor AbnormalityProcessor = new AbnormalityProcessor();

        public void Action()
        {
            
        }

        public void AttackFinished(Creature creature)
        {
            
        }

        public void Init()
        {
            
        }

        public void MarkTarget(ISession session, Creature target)
        {
            
        }

        public void ReleaseAttack(Player player, int attackUid, int type)
        {
            
        }

        public void UseSkill(ISession session, UseSkillArgs args)
        {
            Player player = session.Player;
            Skill skill = args.GetSkill(player);

            ProcessSkill(player, args, skill);
        }

        public void UseSkill(ISession session, List<UseSkillArgs> argsList)
        {
            
        }

        public void UseSkill(Npc npc, Skill skill)
        {
            
        }



        private void ProcessSkill(Creature creature, UseSkillArgs args, Skill skill)
        {
            Log.Debug($"skill == null is {skill is null}");
            /*if (args.SkillId != 0 && skill == null)
            {
                Player p = creature as Player;

                if (args.SkillId == 1)
                    p.PlayerMode = PlayerMode.Relax;

                if (p != null)
                    VisibleService.Send(p, new ResponseActiveSkillData(args.SkillId, 1, 1));
            }
            else */
            if (skill != null)
            {
                ProcessSkill(creature, args, skill, 0);
            }
            else
            {
                ProcessAttack(creature, args);
            }
        }

        private void ProcessSkill(Creature creature, UseSkillArgs args, Skill skill, int time)
        {
            try
            {
                Player player = creature as Player;
                Creature target = creature.Target;

                if (target == null || creature.LifeStats.IsDead())
                    return;
            }
            catch (Exception ex)
            {
                Log.ErrorException("ProcessSkill:", ex);
            }
        }

        private void ProcessAttack(Creature creature, UseSkillArgs args)
        {
            try
            {
                Creature target = creature.Target;

                if (target == null || creature.LifeStats.IsDead())
                    return;

                if (!target.LifeStats.IsDead())
                {
                    creature.Attack = new Attack(creature,
                                             args,
                                             () => GlobalLogic.AttackStageEnd(creature),
                                             () => GlobalLogic.AttackFinished(creature));

                    int damage = SeUtils.CalculateDefaultAttackDamage(creature, target, creature.GameStats.Attack);
                    target.LifeStats.MinusHp(damage);

                    AiLogic.OnAttack(creature, target);
                    AiLogic.OnAttacked(target, creature, damage); 

                    return;
                }

                creature
                    .Attack
                    .Finish();
            }
            catch (Exception ex)
            {
                Log.ErrorException("ProcessAttack:", ex);
            }
        }
    }
}
