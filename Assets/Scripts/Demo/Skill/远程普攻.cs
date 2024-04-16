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
            var script = Type.GetType(bulletcfg.script);
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/Prefabs/{bulletcfg.prefab}.prefab");
            var go = GameObject.Instantiate(prefab);
            try
            {
                go.gameObject.AddComponent(script);
                PrefabUtility.SaveAsPrefabAsset(go.gameObject, $"Assets/Imports/Prefabs/Bullets/{bulletcfg.id}.prefab");
                bulletPrefab = AssetDatabase.LoadAssetAtPath<Bullet.Bullet>($"Assets/Imports/Prefabs/Bullets/{bulletcfg.id}.prefab");
            }
            finally
            {
                GameObject.DestroyImmediate(go.gameObject);
            }
        }


        public override void 释放(Vector2 mousePos)
        {
            base.释放(mousePos);
            if (bulletPrefab)
            {
                var bullet = Instantiate(bulletPrefab);
                bullet.发射(mousePos);
            }
        }
    }
}