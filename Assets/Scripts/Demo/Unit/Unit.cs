using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Demo.Unit
{
    public abstract class Unit : MonoBehaviour
    {
        public float moveSpeed;

        public List<Skill.Skill> skillList;

        public virtual void Load(cfg.Character cfg)
        {
            moveSpeed = cfg.moveSpeed;

            foreach (var skillId in cfg.skills)
            {
                var skillcfg = Tools.tables.TbSkill.Get(skillId);
                var script = Type.GetType(skillcfg.script);
                var skill = (Skill.Skill)gameObject.AddComponent(script);
                skill.Load(skillcfg);
                skillList.Add(skill);
            }
        }
    }
}