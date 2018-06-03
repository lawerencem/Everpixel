using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Model.Character.Enum;
using Assets.Template.Other;
using Assets.View;
using Assets.View.Builder;
using Assets.View.Equipment;
using UnityEngine;

namespace Assets.Controller.Map.Combat.Loader
{
    public class CharLoader: ASingleton<CharLoader>
    {
        private Transform _container;

        public void Init(Transform container, MMapController map, MapInitInfo info)
        {
            this._container = container;
            this.InitViews(map, info);
        }

        public void LoadSummon(CChar c, CTile t)
        {
            var builder = new CharViewBuilder();
            c.SetView(builder.Build(c.Proxy));
            this.RenderChar(c, t);
        }

        private void InitViews(MMapController map, MapInitInfo info)
        {
            var builder = new CharViewBuilder();
            foreach (var party in map.GetLParties())
            {
                foreach (var c in party.GetChars())
                {
                    c.SetView(builder.Build(c.Proxy));
                    var tile = map.GetMap().GetTileForRow(c.Proxy.LParty, c.Proxy.StartCol);
                    this.RenderChar(c, tile);
                }
            }   
            foreach (var party in map.GetRParties())
            {
                foreach (var c in party.GetChars())
                {
                    c.SetView(builder.Build(c.Proxy));
                    var tile = map.GetMap().GetTileForRow(c.Proxy.LParty, c.Proxy.StartCol);
                    this.RenderChar(c, tile);
                }
            }   
        }

        private void RenderChar(CChar character, CTile tile)
        {
            var sprite = character.View.Sprites[character.View.Torso];
            var render = character.GameHandle.AddComponent<SpriteRenderer>();
            character.GameHandle.transform.position = tile.Handle.transform.position;
            character.GameHandle.transform.SetParent(this._container);
            character.GameHandle.name = character.View.Type.ToString() + " " + character.View.Race.ToString();
            render.sprite = sprite;
            render.sortingLayerName = SortingLayers.CHAR_TORSO;
            this.TryAttachHead(character, SortingLayers.CHAR_HEAD, character.View.Head, tile);
            this.TryAttachDeco(character, tile);
            this.TryAttachEquipment(character, tile);
            this.TryAttachMount(character, tile);
            character.SubComponents.Add(SortingLayers.CHAR_TORSO, character.GameHandle);
            character.SubComponents.Add(SortingLayers.CHAR_MAIN, character.GameHandle);

            if (!character.Proxy.LParty)
                character.GameHandle.transform.localRotation = Quaternion.Euler(0, 180, 0);

            // TODO: This really should be elsewhere, but it works for now.
            tile.SetCurrent(character);
            character.SetTile(tile);
            character.ProcessEnterNewTile(tile);
        }

        private void TryAttachDeco(CChar c, CTile t)
        {
            this.TryAttachDecoHelper(c, SortingLayers.CHAR_FACE, c.View.Face, t);
            this.TryAttachDecoHelper(c, SortingLayers.CHAR_HEAD_DECO_1, c.View.HeadDeco1, t);
            this.TryAttachDecoHelper(c, SortingLayers.CHAR_HEAD_DECO_2, c.View.HeadDeco2, t);
            this.TryAttachDecoHelper(c, SortingLayers.CHAR_TORSO_DECO_1, c.View.TorsoDeco1, t);
            this.TryAttachDecoHelper(c, SortingLayers.CHAR_TORSO_DECO_2, c.View.TorsoDeco2, t);
        }

        private void TryAttachDecoHelper(CChar c, string sort, int spriteIndex, CTile tile)
        {
            if (c.Proxy.Type == ECharType.Humanoid && spriteIndex >= 0)
            {
                var spriteHandler = new GameObject();
                var sprite = c.View.Sprites[spriteIndex];
                var render = spriteHandler.AddComponent<SpriteRenderer>();
                spriteHandler.transform.position = c.GameHandle.transform.position;

                if (sort == SortingLayers.CHAR_FACE ||
                    sort == SortingLayers.CHAR_HEAD_DECO_1 ||
                    sort == SortingLayers.CHAR_HEAD_DECO_2)
                {
                    spriteHandler.transform.SetParent(c.SubComponents[SortingLayers.CHAR_HEAD].transform);
                }
                else
                {
                    spriteHandler.transform.SetParent(c.GameHandle.transform);
                }

                spriteHandler.name = sort;
                render.sprite = sprite;
                render.sortingLayerName = sort;
                c.SubComponents.Add(sort, spriteHandler);
            }
        }

        private void TryAttachEquipment(CChar c, CTile t)
        {
            if (c.View.Armor != null)
                TryAttachEquipmentHelper(c, c.View.Armor, SortingLayers.CHAR_ARMOR, t);
            if (c.View.Helm != null)
                TryAttachEquipmentHelper(c, c.View.Helm, SortingLayers.CHAR_HELM, t, 0f, ViewParams.HELM_OFFSET);
            if (c.View.LWeapon != null)
                TryAttachEquipmentHelper(c, c.View.LWeapon, SortingLayers.CHAR_L_WEAPON, t, ViewParams.WEAPON_OFFSET);
            if (c.View.RWeapon != null)
                TryAttachEquipmentHelper(c, c.View.RWeapon, SortingLayers.CHAR_R_WEAPON, t, -ViewParams.WEAPON_OFFSET);
        }

        private void TryAttachEquipmentHelper(CChar c, VEquipment e, string sort, CTile tile, float xOffset = 0, float yOffset = 0)
        {
            if (e != null)
            {
                var spriteHandler = new GameObject();
                if (e.SpriteIndex > e.Sprites.Length)
                    throw new System.Exception();
                var sprite = e.Sprites[e.SpriteIndex];
                var render = spriteHandler.AddComponent<SpriteRenderer>();
                var position = c.GameHandle.transform.position;
                position.x += xOffset;
                position.y += yOffset;
                spriteHandler.transform.position = position;
                if (sort == SortingLayers.CHAR_HELM)
                    spriteHandler.transform.SetParent(c.SubComponents[SortingLayers.CHAR_HEAD].transform);
                else
                    spriteHandler.transform.SetParent(c.GameHandle.transform);
                spriteHandler.name = e.Name;
                render.sprite = sprite;
                render.sortingLayerName = sort;
                c.SubComponents.Add(sort, spriteHandler);
            }
        }

        private void TryAttachHead(CChar c, string sort, int spriteIndex, CTile tile)
        {
            if (c.Proxy.Type == ECharType.Humanoid)
            {
                var sprite = c.View.Sprites[spriteIndex];
                var spriteHandler = new GameObject();
                var render = spriteHandler.AddComponent<SpriteRenderer>();
                spriteHandler.transform.position = c.GameHandle.transform.position;
                spriteHandler.transform.SetParent(c.GameHandle.transform);
                spriteHandler.name = "Character Head";
                render.sprite = sprite;
                render.sortingLayerName = sort;
                c.SubComponents.Add(sort, spriteHandler);
            }
        }

        private void TryAttachMount(CChar c, CTile tile)
        {
            if (c.View.Mount != null)
            {
                var sprite = c.View.Mount.Sprites[0];
                var spriteHandler = new GameObject();
                var render = spriteHandler.AddComponent<SpriteRenderer>();
                var position = tile.Handle.transform.position;
                position.y -= ViewParams.MOUNT_Y_OFFSET;
                spriteHandler.transform.position = position;
                spriteHandler.transform.SetParent(c.GameHandle.transform);
                spriteHandler.name = c.View.Name + " " + c.View.Mount.Name + " Mount";
                render.sprite = sprite;
                render.sortingLayerName = SortingLayers.CHAR_MOUNT;
                c.SubComponents.Add(SortingLayers.CHAR_MOUNT, spriteHandler);
                var mountOffsetPos = c.GameHandle.transform.position;
                c.GameHandle.transform.position = mountOffsetPos;
            }
        }
    }
}