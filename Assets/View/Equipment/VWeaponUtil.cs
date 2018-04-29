using Assets.Controller.Character;
using Assets.Controller.Equipment.Weapon;
using Assets.Model.Action;
using UnityEngine;

namespace Assets.View.Equipment
{
    public class VWeaponUtil
    {
        private readonly float SHIELD_WALL_Y_AXIS = 0.1f;
        private readonly float SPEAR_WALL_X_AXIS = 0.2f;
        private readonly float SPEAR_WALL_Y_AXIS = 0.05f;
        private readonly float SPEAR_WALL_ROTATION = 90f;

        public void DoSpearWallFX(MAction action)
        {
            var view = action.Data.ParentWeapon.View;
            if (!view.SpearWalling)
            {
                if (action.Data.LWeapon)
                {
                    var wpnObject = action.Data.Source.SubComponents[Layers.CHAR_L_WEAPON];
                    if (action.Data.Source.Proxy.LParty)
                        this.DoSpearWallFXHelper(wpnObject, true);
                    else
                        this.DoSpearWallFXHelper(wpnObject, false);
                }
                else
                {
                    var wpnObject = action.Data.Source.SubComponents[Layers.CHAR_R_WEAPON];
                    if (action.Data.Source.Proxy.LParty)
                        this.DoSpearWallFXHelper(wpnObject, true);
                    else
                        this.DoSpearWallFXHelper(wpnObject, false);
                }
                view.SpearWalling = true;
            }
        }

        public void DoShieldWall(MAction action)
        {
            var view = action.Data.ParentWeapon.View;
            if (!view.ShieldWalling)
            {
                if (action.Data.LWeapon)
                {
                    var wpnObject = action.Data.Source.SubComponents[Layers.CHAR_L_WEAPON];
                    if (action.Data.Source.Proxy.LParty)
                        this.DoShieldWallFXHelper(wpnObject, true);
                    else
                        this.DoShieldWallFXHelper(wpnObject, false);
                }
                else
                {
                    var wpnObject = action.Data.Source.SubComponents[Layers.CHAR_R_WEAPON];
                    if (action.Data.Source.Proxy.LParty)
                        this.DoShieldWallFXHelper(wpnObject, true);
                    else
                        this.DoShieldWallFXHelper(wpnObject, false);
                }
                view.ShieldWalling = true;
            }
        }

        public void UndoShieldWallFX(CChar source, CWeapon weapon, bool lWeapon)
        {
            var view = weapon.View;
            if (view.ShieldWalling)
            {
                if (lWeapon)
                {
                    var wpnObject = source.SubComponents[Layers.CHAR_L_WEAPON];
                    this.UndoShieldWallFXHelper(wpnObject, lWeapon);
                }
                else
                {
                    var wpnObject = source.SubComponents[Layers.CHAR_R_WEAPON];
                    this.UndoShieldWallFXHelper(wpnObject, lWeapon);
                }
                view.ShieldWalling = false;
            }
        }

        public void UndoSpearWallFX(CChar source, CWeapon weapon, bool lWeapon)
        {
            var view = weapon.View;
            if (view.SpearWalling)
            {
                if (lWeapon)
                {
                    var wpnObject = source.SubComponents[Layers.CHAR_L_WEAPON];
                    this.UndoSpearWallFXHelper(wpnObject, lWeapon);
                }
                else
                {
                    var wpnObject = source.SubComponents[Layers.CHAR_R_WEAPON];
                    this.UndoSpearWallFXHelper(wpnObject, lWeapon);
                }
                view.SpearWalling = false;
            }
        }

        private void DoShieldWallFXHelper(GameObject weapon, bool lParty)
        {
            if (weapon != null)
            {
                var translate = new Vector3(0f, SHIELD_WALL_Y_AXIS, 0f);
                weapon.transform.Translate(translate);
            }
        }

        private void DoSpearWallFXHelper(GameObject weapon, bool lParty)
        {
            if (weapon != null)
            {
                var translate = new Vector3();
                var rotate = new Vector3();
                if (lParty)
                {
                    translate = new Vector3(-SPEAR_WALL_X_AXIS, SPEAR_WALL_Y_AXIS, 0f);
                    rotate = new Vector3(0f, 0f, SPEAR_WALL_ROTATION);
                }
                else
                {
                    translate = new Vector3(SPEAR_WALL_X_AXIS, -SPEAR_WALL_Y_AXIS, 0f);
                    rotate = new Vector3(0f, 0f, -SPEAR_WALL_ROTATION);
                }
                weapon.transform.Translate(translate);
                weapon.transform.Rotate(rotate);
            }
        }

        private void UndoShieldWallFXHelper(GameObject weapon, bool lParty)
        {
            if (weapon != null)
            {
                var translate = new Vector3(0f, -SHIELD_WALL_Y_AXIS, 0f);
                weapon.transform.Translate(translate);
            }
        }

        private void UndoSpearWallFXHelper(GameObject weapon, bool lParty)
        {
            if (weapon != null)
            {
                var translate = new Vector3();
                var rotate = new Vector3();
                if (lParty)
                {
                    translate = new Vector3(SPEAR_WALL_X_AXIS, -SPEAR_WALL_Y_AXIS, 0f);
                    rotate = new Vector3(0f, 0f, -SPEAR_WALL_ROTATION);
                }
                else
                {
                    translate = new Vector3(-SPEAR_WALL_X_AXIS, SPEAR_WALL_Y_AXIS, 0f);
                    rotate = new Vector3(0f, 0f, SPEAR_WALL_ROTATION);
                }
                weapon.transform.Rotate(rotate);
                weapon.transform.Translate(translate);
            }
        }
    }
}
