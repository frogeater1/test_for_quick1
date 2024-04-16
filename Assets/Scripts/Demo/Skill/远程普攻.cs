using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Tools = Utilities.Tools;

namespace Demo.Skill
{
    public class 远程普攻 : Skill
    {
        public override void Load(cfg.Skill cfg)
        {
            base.Load(cfg);
            var bulletcfg = Tools.tables.TbBullet.Get(cfg.bullet);
            bulletPrefab = AssetDatabase.LoadAssetAtPath<Bullet.Bullet>($"Assets/Imports/Prefabs/Bullets/{bulletcfg.id}.prefab");
        }


        public override void 释放(Vector2 mousePos)
        {
            base.释放(mousePos);
            if (bulletPrefab)
            {
                if (Physics.Raycast(Game.Instance.mainCamera.ScreenPointToRay(Input.mousePosition), out var hit, 1000))
                {
                    var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, 0));
                    bullet.发射(hit.point);
                }
            }
        }
    }
}