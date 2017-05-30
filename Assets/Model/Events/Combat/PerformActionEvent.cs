﻿using Controller.Characters;
using Controller.Managers;
using Controller.Map;
using Generics.Scripts;
using Model.Abilities;
using UnityEngine;

namespace Model.Events.Combat
{
    public class PerformActionEvent : CombatEvent
    {
        public GenericAbility Action { get; set; }
        public GenericCharacterController Source { get; set; }
        public GenericCharacterController Target { get; set; }

        public PerformActionEvent(
            CombatEventManager parent, 
            TileController source, 
            TileController target, 
            GenericAbility action) :
            base(CombatEventEnum.PerformActionEvent, parent)
        {
            if (!this._parent.GetLock())
            {
                if (source.Model.Current.GetType() == typeof(GenericCharacterController) &&
                    target.Model.Current.GetType() == typeof(GenericCharacterController))
                {
                    this.Source = source.Model.Current as GenericCharacterController;
                    this.Target = target.Model.Current as GenericCharacterController;
                    this.Action = action;

                    double fatigueCost = this.Action.StaminaCost;

                    if (this.Source.Model.Armor != null)
                        fatigueCost *= this.Source.Model.Armor.FatigueCost;
                    if (this.Source.Model.Helm != null)
                        fatigueCost *= this.Source.Model.Helm.FatigueCost;
                    if (this.Source.Model.LWeapon != null)
                        fatigueCost *= this.Source.Model.LWeapon.FatigueCostMod;
                    if (this.Source.Model.RWeapon != null)
                        fatigueCost *= this.Source.Model.RWeapon.FatigueCostMod;

                    if (this.Action.APCost < this.Source.Model.CurrentAP  &&
                        fatigueCost < this.Source.Model.CurrentStamina)
                    {
                        this._parent.Lock();
                        this.Source.Model.CurrentAP -= this.Action.APCost;
                        this.Source.Model.CurrentStamina -= (int)fatigueCost;
                        this.RegisterEvent();
                    }
                }
            }
        }
    }
}