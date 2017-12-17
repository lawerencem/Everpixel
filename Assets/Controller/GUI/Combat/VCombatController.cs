using Assets.Controller.Character;
using Assets.Model.Ability.Enum;
using Assets.Model.Action;
using Assets.Model.Combat.Hit;
using Assets.Template.CB;
using Assets.Template.Script;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Controller.GUI.Combat
{
    public class VCombatController : ICallback
    {
        private List<Callback> _callbacks;

        private static VCombatController _instance;
        public static VCombatController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new VCombatController();
                return _instance;
            }
        }

        public VCombatController()
        {
            this._callbacks = new List<Callback>();
        }

        public void AddCallback(Callback callback)
        {
            this._callbacks.Add(callback);
        }

        public void DisplayNewAction(MAction a)
        {
            this._callbacks.Clear();
            switch(a.ActiveAbility.Data.CastType)
            {
                case (ECastType.Bullet): { VHitController.Instance.ProcessBulletFX(a); } break;
                case (ECastType.Custom): { VHitController.Instance.ProcessCustomFX(a); } break;
                case (ECastType.Melee): { VHitController.Instance.ProcessMeleeHitFX(a); } break;
                case (ECastType.Single): { VHitController.Instance.ProcessSingleHitFX(a); } break;
                case (ECastType.Song): { VHitController.Instance.ProcessSongFX(a); } break;
            }
        }

        public void DisplayActionEventName(MAction a)
        {
            var data = new HitDisplayData();
            data.Color = CombatGUIParams.WHITE;
            data.Hit = null;
            data.Target = a.Data.Source.Handle;
            data.Text = a.ActiveAbility.Type.ToString().Replace("_", " ");
            data.YOffset = CombatGUIParams.FLOAT_OFFSET;
            data.Display();
        }

        public void DisplayText(string text, CChar tgt)
        {
            var data = new HitDisplayData();
            data.Color = CombatGUIParams.WHITE;
            data.Hit = null;
            data.Target = tgt.Handle;
            data.Text = text;
            data.YOffset = CombatGUIParams.FLOAT_OFFSET;
            data.Display();
        }

        public void DisplayText(HitDisplayData data)
        {
            var parent = GameObject.FindGameObjectWithTag("WorldSpaceCanvas");
            var display = new GameObject();
            var text = display.AddComponent<Text>();
            var position = data.Target.transform.position;
            position.y += data.YOffset;
            text.alignment = TextAnchor.MiddleCenter;
            text.color = data.Color;
            text.fontSize = 16;
            text.rectTransform.SetParent(parent.transform);
            text.rectTransform.position = position;
            text.rectTransform.sizeDelta = new Vector2(200f, 200f);
            text.rectTransform.localScale = new Vector3(0.0075f, 0.0075f);
            text.name = "Hit Text";
            text.text = data.Text;
            Font fontToUse = Resources.Load("Fonts/8bitOperatorPlus8-Bold") as Font;
            text.font = fontToUse;
            var script = display.AddComponent<SDestroyByLifetime>();
            script.AddCallback(data.CallbackHandler);
            script.Init(display, data.Dur);
            var floating = display.AddComponent<SFloatingText>();
            floating.Init(display, 0.0015f, data.Delay);
        }

        public void DoCallbacks()
        {
            foreach (var callback in this._callbacks)
                callback(this);
        }

        public void SetCallback(Callback callback)
        {
            this._callbacks = new List<Callback>() { callback };
        }
    }

    //    public void ProcessBlock(DisplayHitStatsEvent e)
    //    {
    //        this.DisplayText("Block", e.Hit.Target.Handle, CMapGUIControllerParams.WHITE, CMapGUIControllerParams.BLOCK_TEXT_OFFSET);
    //        if (FHit.HasFlag(e.Hit.Flags.CurFlags, FHit.Flags.Critical))
    //            this.DisplayText("Critical!", e.Hit.Target.Handle, CMapGUIControllerParams.RED, CMapGUIControllerParams.CRIT_TEXT_OFFSET);
    //        if (e.Hit.Target.Model.LWeapon != null && e.Hit.Target.Model.LWeapon.IsTypeOfShield())
    //        {
    //            var weapon = e.Hit.Target.SpriteHandlerDict[ViewParams.CHAR_L_WEAPON];
    //            var boomerang = weapon.AddComponent<BoomerangScript>();
    //            var position = weapon.transform.position;
    //            if (e.Hit.Target.LParty)
    //                position.x -= CMapGUIControllerParams.WEAPON_OFFSET;
    //            else
    //                position.x += CMapGUIControllerParams.WEAPON_OFFSET;
    //            boomerang.Init(weapon, position, CMapGUIControllerParams.WEAPON_PARRY);
    //        }
    //        if (e.Hit.Target.Model.RWeapon != null && e.Hit.Target.Model.RWeapon.IsTypeOfShield())
    //        {
    //            var weapon = e.Hit.Target.SpriteHandlerDict[ViewParams.CHAR_R_WEAPON];
    //            var boomerang = weapon.AddComponent<BoomerangScript>();
    //            var position = weapon.transform.position;
    //            if (e.Hit.Target.LParty)
    //                position.x -= CMapGUIControllerParams.WEAPON_OFFSET;
    //            else
    //                position.x += CMapGUIControllerParams.WEAPON_OFFSET;
    //            boomerang.Init(weapon, position, CMapGUIControllerParams.WEAPON_PARRY);
    //        }
    //    }

    //    public void ProcessBulletFX(DisplayActionEvent e)
    //    {
    //        this.DisplayActionEventName(e);
    //        if (this.IsFatality(e))
    //        {
    //            if (!this.FatalitySuccessful(e))
    //                this.ProcessBulletFXNonFatality(e);
    //        }
    //        else
    //            this.ProcessBulletFXNonFatality(e);
    //    }


    //    public void ProcessCharacterKilled(CharController c)
    //    {
    //        if (!c.KillFXProcessed)
    //        {
    //            foreach (var particle in c.Particles)
    //                GameObject.Destroy(particle);
    //            this.ProcessCharacterKilledHelper(c);
    //            this.RandomRotate(c.Handle);
    //            this.ProcessSplatter(5, c.CurrentTile, 1);
    //        }
    //        c.KillFXProcessed = true;
    //    }

    //    public void ProcessDodge(DisplayHitStatsEvent e)
    //    {
    //        var defenderJolt = e.Hit.Target.Handle.AddComponent<BoomerangScript>();
    //        defenderJolt.Init(e.Hit.Target.Handle, this.GetRandomDodgePosition(e), 6f);
    //        this.DisplayText("Dodge", e.Hit.Target.Handle, CMapGUIControllerParams.WHITE, CMapGUIControllerParams.DODGE_TEXT_OFFSET);
    //    }

    //    public void ProcessInjury(ApplyInjuryEvent e)
    //    {
    //        var text = e.Injury.Type.ToString().Replace("_", " ");
    //        this.DisplayText(text, e.Target.Handle, CMapGUIControllerParams.RED, CMapGUIControllerParams.INJURY_TEXT_OFFSET);
    //    }



    //    public void ProcessNormalHit(DisplayHitStatsEvent e)
    //    {
    //        if (e.Hit.Target.Model.GetCurrentHP() - e.Hit.Dmg > 0)
    //        {
    //            var position = e.Hit.Target.transform.position;
    //            position.y -= 0.08f;
    //            var defenderFlinch = e.Hit.Target.Handle.AddComponent<FlinchScript>();
    //            defenderFlinch.Init(e.Hit.Target, position, 8f);
    //        }
    //        if (FHit.HasFlag(e.Hit.Flags.CurFlags, FHit.Flags.Critical))
    //            this.DisplayText("Crit!", e.Hit.Target.Handle, CMapGUIControllerParams.RED, CMapGUIControllerParams.DODGE_TEXT_OFFSET);
    //    }

    //    public void ProcessResist(DisplayHitStatsEvent e)
    //    {
    //        this.DisplayText("Resist", e.Hit.Target.Handle, CMapGUIControllerParams.WHITE, CMapGUIControllerParams.CRIT_TEXT_OFFSET);
    //    }

    //    public void ProcessSummon(DisplayHitStatsEvent e)
    //    {
    //        e.Done();
    //    }

    //    public void ProcessParry(DisplayHitStatsEvent e)
    //    {
    //        this.DisplayText("Parry", e.Hit.Target.Handle, CMapGUIControllerParams.WHITE, CMapGUIControllerParams.PARRY_TEXT_OFFSET);
    //        if (e.Hit.Target.Model.LWeapon != null && !e.Hit.Target.Model.LWeapon.IsTypeOfShield())
    //        {
    //            var weapon = e.Hit.Target.SpriteHandlerDict[ViewParams.CHAR_L_WEAPON];
    //            var position = weapon.transform.position;
    //            var boomerang = weapon.AddComponent<BoomerangScript>();
    //            if (e.Hit.Target.LParty)
    //                position.x -= CMapGUIControllerParams.WEAPON_OFFSET;
    //            else
    //                position.x += CMapGUIControllerParams.WEAPON_OFFSET;
    //            boomerang.Init(weapon, position, CMapGUIControllerParams.WEAPON_PARRY);
    //        }
    //        if (e.Hit.Target.Model.RWeapon != null && !e.Hit.Target.Model.RWeapon.IsTypeOfShield())
    //        {
    //            var weapon = e.Hit.Target.SpriteHandlerDict[ViewParams.CHAR_R_WEAPON];
    //            var position = weapon.transform.position;
    //            var boomerang = weapon.AddComponent<BoomerangScript>();
    //            if (e.Hit.Target.LParty)
    //                position.x -= CMapGUIControllerParams.WEAPON_OFFSET;
    //            else
    //                position.x += CMapGUIControllerParams.WEAPON_OFFSET;
    //            boomerang.Init(weapon, position, CMapGUIControllerParams.WEAPON_PARRY);
    //        }
    //    }

    //    public void ProcessZoneFX(DisplayActionEvent e)
    //    {
    //        this.DisplayActionEventName(e);
    //        if (this.IsFatality(e))
    //        {
    //            if (!this.FatalitySuccessful(e))
    //                this.ProcessZoneFXNonFatality(e);
    //        }
    //        else
    //            this.ProcessZoneFXNonFatality(e);
    //    }

    //    private void AssignDeadLayer(GameObject o, string layer)
    //    {
    //        var renderer = o.GetComponent<SpriteRenderer>();
    //        if (renderer != null)
    //            renderer.sortingLayerName = layer;
    //    }

    //    private void ProcessCharacterKilledHelper(CharController c)
    //    {
    //        if (c.Model.Type == ECharacterType.Humanoid)
    //        {
    //            if (c.Model.Armor != null)
    //                this.AssignDeadLayer(c.SpriteHandlerDict[ViewParams.CHAR_ARMOR], ViewParams.DEAD_ARMOR);
    //            if (c.Model.Helm != null)
    //                this.AssignDeadLayer(c.SpriteHandlerDict[ViewParams.CHAR_HELM], ViewParams.DEAD_HELM);
    //            if (c.Model.LWeapon != null)
    //            {
    //                this.RandomMoveKill(c.SpriteHandlerDict[ViewParams.CHAR_L_WEAPON]);
    //                this.AssignDeadLayer(c.SpriteHandlerDict[ViewParams.CHAR_L_WEAPON], ViewParams.CHAR_L_WEAPON);
    //            }
    //            if (c.Model.RWeapon != null)
    //            {
    //                this.RandomMoveKill(c.SpriteHandlerDict[ViewParams.CHAR_R_WEAPON]);
    //                this.AssignDeadLayer(c.SpriteHandlerDict[ViewParams.CHAR_R_WEAPON], ViewParams.DEAD_R_WEAPON);
    //            }
    //            var eyes = c.SpriteHandlerDict[ViewParams.CHAR_FACE].GetComponent<SpriteRenderer>();
    //            if (eyes.sprite != null)
    //                eyes.sprite = CharacterSpriteLoader.Instance.GetHumanoidDeadEyes(c.Model.Race);

    //            if (c.SpriteHandlerDict.ContainsKey(ViewParams.CHAR_FACE))
    //                this.AssignDeadLayer(c.SpriteHandlerDict[ViewParams.CHAR_FACE], ViewParams.DEAD_FACE);
    //            if (c.SpriteHandlerDict.ContainsKey(ViewParams.CHAR_HEAD_DECO_1))
    //                this.AssignDeadLayer(c.SpriteHandlerDict[ViewParams.CHAR_HEAD_DECO_1], ViewParams.DEAD_HEAD_DECO_1);
    //            if (c.SpriteHandlerDict.ContainsKey(ViewParams.CHAR_HEAD_DECO_2))
    //                this.AssignDeadLayer(c.SpriteHandlerDict[ViewParams.CHAR_HEAD_DECO_2], ViewParams.DEAD_HEAD_DECO_2);
    //            if (c.SpriteHandlerDict.ContainsKey(ViewParams.CHAR_TORSO_DECO_1))
    //                this.AssignDeadLayer(c.SpriteHandlerDict[ViewParams.CHAR_TORSO_DECO_1], ViewParams.DEAD_TORSO_DECO_1);
    //            if (c.SpriteHandlerDict.ContainsKey(ViewParams.CHAR_TORSO_DECO_2))
    //                this.AssignDeadLayer(c.SpriteHandlerDict[ViewParams.CHAR_TORSO_DECO_2], ViewParams.DEAD_TORSO_DECO_2);
    //            if (c.SpriteHandlerDict.ContainsKey(ViewParams.CHAR_HEAD))
    //                this.AssignDeadLayer(c.SpriteHandlerDict[ViewParams.CHAR_HEAD], ViewParams.DEAD_HEAD);
    //            if (c.SpriteHandlerDict.ContainsKey(ViewParams.CHAR_TORSO))
    //                this.AssignDeadLayer(c.SpriteHandlerDict[ViewParams.CHAR_TORSO], ViewParams.DEAD_TORSO);

    //            if (c.SpriteHandlerDict.ContainsKey(ViewParams.CHAR_MOUNT))
    //            {
    //                // TODO: Mounts spawning on kill
    //                c.SpriteHandlerDict[ViewParams.CHAR_MOUNT].transform.SetParent(null);
    //                c.SpriteHandlerDict[ViewParams.CHAR_MOUNT].transform.position = c.CurrentTile.Model.Center;
    //            }
    //        }
    //    }




    //    private void ProcessZoneFXNonFatality(DisplayActionEvent e)
    //    {
    //        var attackerScript = e.EventController.Source.Handle.AddComponent<AttackerJoltScript>();
    //        var position = Vector3.Lerp(e.EventController.Target.Model.Center, e.EventController.Source.CurrentTile.Model.Center, 0.85f);
    //        attackerScript.Init(e.EventController.Source, position, 8f, e.AttackFXDone);

    //        var sprite = AttackSpriteLoader.Instance.GetAttackSprite(e.EventController.Action);
    //        var action = e.EventController.Action;
    //        foreach (var tile in e.EventController.Target.Model.GetAoETiles((int)action.AoE))
    //        {
    //            var tileDeco = new GameObject();
    //            var renderer = tileDeco.AddComponent<SpriteRenderer>();
    //            renderer.sprite = sprite;
    //            this.RandomRotate(tileDeco);
    //            tileDeco.transform.position = tile.Center;
    //            tileDeco.transform.SetParent(tile.Parent.transform);
    //            renderer.sortingLayerName = CMapGUIControllerParams.PARTICLES_LAYER;
    //        }
    //    }

    
    //}
}
