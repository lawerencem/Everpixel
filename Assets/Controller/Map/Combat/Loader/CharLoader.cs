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

        public void LoadSummon(CharController c, TileController t)
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

        private void RenderChar(CharController c, TileController t)
        {
            var sprite = c.View.Sprites[c.View.Torso];
            var render = c.Handle.AddComponent<SpriteRenderer>();
            c.Handle.transform.position = t.View.Center;
            c.Handle.transform.SetParent(this._container);
            c.Handle.name = c.View.Type.ToString() + " " + c.View.Race.ToString();
            render.sprite = sprite;
            render.sortingLayerName = Layers.CHAR_TORSO;
            this.TryAttachHead(c, Layers.CHAR_HEAD, c.View.Head, t);
            this.TryAttachDeco(c, t);
            this.TryAttachEquipment(c, t);
            this.TryAttachMount(c, t);
            c.SubComponents.Add(Layers.CHAR_TORSO, c.Handle);
            c.SubComponents.Add(Layers.CHAR_MAIN, c.Handle);

            if (!c.Proxy.LParty)
                c.Handle.transform.localRotation = Quaternion.Euler(0, 180, 0);

            // TODO: This really should be elsewhere, but it works for now.
            t.SetCurrent(c);
            c.SetTile(t);
        }

        private void TryAttachDeco(CharController c, TileController t)
        {
            this.TryAttachDecoHelper(c, Layers.CHAR_FACE, c.View.Face, t);
            this.TryAttachDecoHelper(c, Layers.CHAR_HEAD_DECO_1, c.View.HeadDeco1, t);
            this.TryAttachDecoHelper(c, Layers.CHAR_HEAD_DECO_2, c.View.HeadDeco2, t);
            this.TryAttachDecoHelper(c, Layers.CHAR_TORSO_DECO_1, c.View.TorsoDeco1, t);
            this.TryAttachDecoHelper(c, Layers.CHAR_TORSO_DECO_2, c.View.TorsoDeco2, t);
        }

        private void TryAttachDecoHelper(CharController c, string sort, int spriteIndex, TileController tile)
        {
            if (c.Proxy.Type == ECharType.Humanoid && spriteIndex >= 0)
            {
                var spriteHandler = new GameObject();
                var sprite = c.View.Sprites[spriteIndex];
                var render = spriteHandler.AddComponent<SpriteRenderer>();
                spriteHandler.transform.position = c.Handle.transform.position;

                if (sort == Layers.CHAR_FACE ||
                    sort == Layers.CHAR_HEAD_DECO_1 ||
                    sort == Layers.CHAR_HEAD_DECO_2)
                {
                    spriteHandler.transform.SetParent(c.SubComponents[Layers.CHAR_HEAD].transform);
                }
                else
                {
                    spriteHandler.transform.SetParent(c.Handle.transform);
                }

                spriteHandler.name = sort;
                render.sprite = sprite;
                render.sortingLayerName = sort;
                c.SubComponents.Add(sort, spriteHandler);
            }
        }

        private void TryAttachEquipment(CharController c, TileController t)
        {
            if (c.View.Armor != null)
                TryAttachEquipmentHelper(c, c.View.Armor, Layers.CHAR_ARMOR, t);
            if (c.View.Helm != null)
                TryAttachEquipmentHelper(c, c.View.Helm, Layers.CHAR_HELM, t, 0f, ViewParams.HELM_OFFSET);
            if (c.View.LWeapon != null)
                TryAttachEquipmentHelper(c, c.View.LWeapon, Layers.CHAR_L_WEAPON, t, ViewParams.WEAPON_OFFSET);
            if (c.View.RWeapon != null)
                TryAttachEquipmentHelper(c, c.View.RWeapon, Layers.CHAR_R_WEAPON, t, -ViewParams.WEAPON_OFFSET);
        }

        private void TryAttachEquipmentHelper(CharController c, VEquipment e, string sort, TileController tile, float xOffset = 0, float yOffset = 0)
        {
            if (e != null)
            {
                var spriteHandler = new GameObject();
                var sprite = e.Sprites[e.SpriteIndex];
                var render = spriteHandler.AddComponent<SpriteRenderer>();
                var position = c.Handle.transform.position;
                position.x += xOffset;
                position.y += yOffset;
                spriteHandler.transform.position = position;
                if (sort == Layers.CHAR_HELM)
                    spriteHandler.transform.SetParent(c.SubComponents[Layers.CHAR_HEAD].transform);
                else
                    spriteHandler.transform.SetParent(c.Handle.transform);
                spriteHandler.name = e.Name;
                render.sprite = sprite;
                render.sortingLayerName = sort;
                c.SubComponents.Add(sort, spriteHandler);
            }
        }

        private void TryAttachHead(CharController c, string sort, int spriteIndex, TileController tile)
        {
            if (c.Proxy.Type == ECharType.Humanoid)
            {
                var sprite = c.View.Sprites[spriteIndex];
                var spriteHandler = new GameObject();
                var render = spriteHandler.AddComponent<SpriteRenderer>();
                spriteHandler.transform.position = c.Handle.transform.position;
                spriteHandler.transform.SetParent(c.Handle.transform);
                spriteHandler.name = "Character Head";
                render.sprite = sprite;
                render.sortingLayerName = sort;
                c.SubComponents.Add(sort, spriteHandler);
            }
        }

        private void TryAttachMount(CharController c, TileController tile)
        {
            if (c.View.Mount != null)
            {
                var sprite = c.View.Mount.Sprites[0];
                var spriteHandler = new GameObject();
                var render = spriteHandler.AddComponent<SpriteRenderer>();
                var position = tile.View.Center;
                position.x += ViewParams.MOUNT_X_OFFSET;
                position.y -= ViewParams.MOUNT_Y_OFFSET;
                spriteHandler.transform.position = position;
                spriteHandler.transform.SetParent(c.Handle.transform);
                spriteHandler.name = c.View.Name + " " + c.View.Mount.Name + " Mount";
                render.sprite = sprite;
                render.sortingLayerName = Layers.CHAR_MOUNT;
                c.SubComponents.Add(Layers.CHAR_MOUNT, spriteHandler);
                var mountOffsetPos = c.Handle.transform.position;
                mountOffsetPos.y += ViewParams.MOUNT_Y_OFFSET;
                c.Handle.transform.position = mountOffsetPos;
            }
        }
    }
}