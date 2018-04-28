using Assets.Controller.Character;
using Assets.Controller.Equipment.Weapon;
using Assets.Model.Action;
using UnityEngine;

namespace Assets.View.Equipment
{
    public class VWeaponUtil
    {
        public void DoSpearWallFX(MAction action)
        {
            var view = action.Data.ParentWeapon.View;
            if (!view.SpearWalling)
            {
                if (action.Data.LWeapon)
                {
                    var wponObject = action.Data.Source.SubComponents[Layers.CHAR_L_WEAPON];
                    if (action.Data.Source.Proxy.LParty)
                        this.DoSpearWallFXHelper(wponObject, true);
                    else
                        this.DoSpearWallFXHelper(wponObject, false);
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
            }
        }

        public void UndoSpearWallFX(MAction action)
        {
            var view = action.Data.ParentWeapon.View;
            if (view.SpearWalling)
            {
                if (action.Data.LWeapon)
                {
                    var wponObject = action.Data.Source.SubComponents[Layers.CHAR_L_WEAPON];
                    if (action.Data.Source.Proxy.LParty)
                        this.UndoSpearWallFXHelper(wponObject, true);
                    else
                        this.UndoSpearWallFXHelper(wponObject, false);
                }
                else
                {
                    var wpnObject = action.Data.Source.SubComponents[Layers.CHAR_R_WEAPON];
                    if (action.Data.Source.Proxy.LParty)
                        this.UndoSpearWallFXHelper(wpnObject, true);
                    else
                        this.UndoSpearWallFXHelper(wpnObject, false);
                }
                view.SpearWalling = false;
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
                    translate = new Vector3(-0.2f, 0.05f, 0f);
                    rotate = new Vector3(0f, 0f, 90f);
                }
                else
                {
                    translate = new Vector3(0.2f, -0.05f, 0f);
                    rotate = new Vector3(0f, 0f, -90f);
                }
                weapon.transform.Translate(translate);
                weapon.transform.Rotate(rotate);
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
                    translate = new Vector3(0.2f, -0.05f, 0f);
                    rotate = new Vector3(0f, 0f, -90f);
                }
                else
                {
                    translate = new Vector3(-0.2f, 0.05f, 0f);
                    rotate = new Vector3(0f, 0f, 90f);
                }
                weapon.transform.Rotate(rotate);
                weapon.transform.Translate(translate);
            }
        }
    }
}
