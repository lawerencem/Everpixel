using Model.Biomes;
using Model.Characters;
using System.Collections.Generic;
using UnityEngine;
using View.Biomes;
using View.Builders;
using View.Equipment;
using Controller.Characters;
using Model.Events.Combat;
using Model.Map;
using Controller.Map;
using View.Map;
using Generics.Hex;
using Assets.Generics;
using View.Scripts;

namespace Controller.Managers.Map
{
    public class CombatMapLoader
    {
        private const float MOUNT_X_OFFSET = 0.05f;
        private const float MOUNT_Y_OFFSET = 0.15f;
        private const float HELM_OFFSET = 0.15f;
        private const float WEAPON_OFFSET = 0.09f;

        private const int ROWS = 12;
        private const int COLS = 12;
        private const float OFFSET = 0.63f;

        private List<TileController> _emptyTiles = new List<TileController>();
        private CombatMap _map;

        public Transform MapHolder;
        public Transform BackgroundTiles;

        public void InitMap(BiomeEnum b)
        {
            this.MapHolder = new GameObject("Map").transform;
            this.BackgroundTiles = new GameObject("BackgroundTiles").transform;
            this.BackgroundTiles.transform.SetParent(this.MapHolder);
            this.InitBackgroundTiles(b);
            this.InitBackgroundDeco(b);
            this.InitGUI();
            this.InitParties();
        }

        public void BuildAndPlaceCharacter(
            CharacterParams cParams,
            ref List<GenericCharacterController> controllers,
            TileController tile,
            bool lParty = false)
        {
            var builder = new CharacterViewBuilder();
            var handle = new GameObject();
            var controller = handle.AddComponent<GenericCharacterController>();
            controller.Init(handle);
            var view = builder.Build(cParams);
            controller.SetView(view, cParams);
            this.LayoutCharacterAtTile(controller, tile, cParams, lParty);
            controllers.Add(controller);
            controller.LParty = lParty;
        }

        private void AttachDeco(GenericCharacterController c, string sort, int spriteIndex, TileController tile)
        {
            if (c.Model.Type == CharacterTypeEnum.Humanoid)
            {
                var sprite = c.View.Sprites[spriteIndex];
                var spriteHandler = new GameObject();
                var render = spriteHandler.AddComponent<SpriteRenderer>();
                spriteHandler.transform.position = c.Handle.transform.position;
                if (sort == "CharFace" || sort == "CharDeco1" || sort == "CharDeco2")
                    spriteHandler.transform.SetParent(c.SpriteHandlerDict["CharHead"].transform);
                else
                    spriteHandler.transform.SetParent(c.Handle.transform);
                spriteHandler.name = "Character Deco";
                render.sprite = sprite;
                render.sortingLayerName = sort;
                c.SpriteHandlerDict.Add(sort, spriteHandler);
            }
        }

        private void AttachHead(GenericCharacterController c, string sort, int spriteIndex, TileController tile)
        {
            if (c.Model.Type == CharacterTypeEnum.Humanoid)
            {
                var sprite = c.View.Sprites[spriteIndex];
                var spriteHandler = new GameObject();
                var render = spriteHandler.AddComponent<SpriteRenderer>();
                spriteHandler.transform.position = c.Handle.transform.position;
                spriteHandler.transform.SetParent(c.Handle.transform);
                spriteHandler.name = "Character Head";
                render.sprite = sprite;
                render.sortingLayerName = sort;
                c.SpriteHandlerDict.Add(sort, spriteHandler);
            }
        }

        private void AttachMount(GenericCharacterController c, string sort, TileController tile)
        {
            var sprite = c.View.Mount.Sprites[0];
            var spriteHandler = new GameObject();
            var render = spriteHandler.AddComponent<SpriteRenderer>();
            var position = tile.View.Center;
            position.x += MOUNT_X_OFFSET;
            position.y -= MOUNT_Y_OFFSET;
            spriteHandler.transform.position = position;
            spriteHandler.transform.SetParent(c.Handle.transform);
            spriteHandler.name = c.View.Name + " " + c.View.Mount.Name + " Mount";
            render.sprite = sprite;
            render.sortingLayerName = sort;
            c.SpriteHandlerDict.Add(sort, spriteHandler);
            var mountOffsetPos = c.Handle.transform.position;
            mountOffsetPos.y += MOUNT_Y_OFFSET;
            c.transform.position = mountOffsetPos;
        }

        private void BuildAndLayoutCharacter(
            CharacterParams cParams, 
            ref List<GenericCharacterController> controllers, 
            bool lParty = false)
        {
            var builder = new CharacterViewBuilder();
            var handle = new GameObject();
            var controller = handle.AddComponent<GenericCharacterController>();
            controller.Init(handle);
            var view = builder.Build(cParams);
            controller.SetView(view, cParams);
            this.LayoutCharacter(controller, cParams, lParty);
            controllers.Add(controller);
            controller.LParty = lParty;
        }

        private void InitParties()
        {
            var lParty = new List<GenericCharacterController>();
            var rParty = new List<GenericCharacterController>();
            this.InitPlayerParty(ref lParty);
            this.InitEnemyParty(ref rParty);
            var e = new MapDoneLoadingEvent(CombatEventManager.Instance, lParty, rParty, this._map, this);
        }

        private void InitPlayerParty(ref List<GenericCharacterController> controllers)
        {
            var playerChars = EnemyPartyLoader.Instance.GetParty(new Pair<string, int>("Lizardman War Party", 15));
            for (int i = 0; i < playerChars.Count; i++)
                this.BuildAndLayoutCharacter(playerChars[i], ref controllers, true);
        }

        private void InitEnemyParty(ref List<GenericCharacterController> controllers)
        {
            var enemies = EnemyPartyLoader.Instance.GetParty(new Pair<string, int>("Goblin War Party", 15));
            for (int i = 0; i < enemies.Count; i++)
                this.BuildAndLayoutCharacter(enemies[i], ref controllers);
        }

        private void InitBackgroundTiles(BiomeEnum b)
        {
            var tiles = MapBridge.Instance.GetBackgroundSprites(b);
            this.SetupBackground(tiles);
        }

        private void InitBackgroundDeco(BiomeEnum b)
        {
            var sprites = MapBridge.Instance.GetBackgroundDecoSprites(b);
            for (int itr = 0; itr < 15; itr++)
            {
                var random = this._emptyTiles[Random.Range(0, this._emptyTiles.Count)];
                var sprite = sprites[Random.Range(0, sprites.Length)];
                this._emptyTiles.Remove(random);
                var spriteHandler = new GameObject();
                var render = spriteHandler.AddComponent<SpriteRenderer>();
                spriteHandler.transform.position = random.transform.position;
                spriteHandler.name = "Background Tile Deco";
                render.sprite = sprite;
                render.sortingLayerName = "BackgroundTileDeco";
                random.Model.Current = random;
            }
        }

        private void SetupBackground(Sprite[] tiles)
        {
            var center = new Vector3(-8, 5, 0);
            var hexMap = HexMapMaker.GetMap(ROWS, COLS, OFFSET, center);
            this._map = new CombatMap(hexMap);

            foreach (var hex in hexMap.Tiles)
            {
                var sprite = tiles[Random.Range(0, tiles.Length)];
                var spriteHandler = new GameObject();
                var controller = spriteHandler.AddComponent<TileController>();
                var render = spriteHandler.AddComponent<SpriteRenderer>();
                spriteHandler.transform.position = hex.Center;
                spriteHandler.transform.SetParent(this.BackgroundTiles);
                spriteHandler.name = "Background Tiles";
                render.sprite = sprite;
                render.sortingLayerName = "BackgroundTile";
                controller.SetModel(hex);
                controller.SetView(new HexTileView(hex));
                this._map.AddTileController(controller);
                controller.Init(spriteHandler);
            }

            this._map.InitControllerAdjacent();
            foreach (var tile in this._map.TileControllers) { this._emptyTiles.Add(tile); }
        }

        private void InitGUI()
        {
            for (int i = 0; i < 7; i++)
            {
                var tag = "WpnBtnTag" + i;
                var btnContainer = GameObject.FindGameObjectWithTag(tag);
                var script = btnContainer.AddComponent<WpnBtnClick>();
                script.Init(tag);
            }
            var abilitiesBtn = GameObject.FindGameObjectWithTag("AbilitiesBtnTag");
            var clickScript = abilitiesBtn.AddComponent<AbilityModalBtnClick>();
            clickScript.Init("AbilitiesBtnTag");
        }

        private void LayoutCharacter(GenericCharacterController c, CharacterParams cParams, bool LParty)
        {
            if (c.View != null)
            {
                var tile = this._map.GetTileForRow(LParty, cParams.StartRow);
                this.LayoutCharacterAtTile(c, tile, cParams, LParty);
            }
        }

        private void LayoutCharacterAtTile(GenericCharacterController c, TileController tile, CharacterParams cParams, bool lParty)
        {
            if (c.View != null)
            {
                var sprite = c.View.Sprites[c.View.Torso];
                var render = c.Handle.AddComponent<SpriteRenderer>();
                c.Handle.transform.position = tile.View.Center;
                c.Handle.transform.SetParent(this.MapHolder);
                c.Handle.name = c.View.Type.ToString() + " " + c.View.Race.ToString();
                render.sprite = sprite;
                render.sortingLayerName = "CharTorso";
                c.SpriteHandlerDict.Add("CharTorso", c.Handle);
                AttachHead(c, "CharHead", c.View.Head, tile);
                c.SpriteHandlerDict.Add("Character Main", c.Handle);
                if (c.View.Mount != null) { AttachMount(c, "CharMount", tile); }
                AttachDeco(c, "CharFace", c.View.Face, tile);
                AttachDeco(c, "CharDeco1", c.View.Deco1, tile);
                AttachDeco(c, "CharDeco2", c.View.Deco2, tile);
                AttachDeco(c, "CharDeco3", c.View.Deco3, tile);
                AttachDeco(c, "CharDeco4", c.View.Deco4, tile);
                if (c.View.Armor != null) { TryAttachEquipment(c, c.View.Armor, "CharArmor", tile); }
                if (c.View.Helm != null) { TryAttachEquipment(c, c.View.Helm, "CharHelm", tile, 0f, HELM_OFFSET); }
                if (c.View.LWeapon != null) { TryAttachEquipment(c, c.View.LWeapon, "CharLWeapon", tile, WEAPON_OFFSET); }
                if (c.View.RWeapon != null) { TryAttachEquipment(c, c.View.RWeapon, "CharRWeapon", tile, -WEAPON_OFFSET); }
                if (!lParty)
                    c.Handle.transform.localRotation = Quaternion.Euler(0, 180, 0);
                tile.Model.Current = c;
                c.CurrentTile = tile;
            }
        }

        private void TryAttachEquipment(GenericCharacterController c, EquipmentView e, string sort, TileController tile, float xOffset = 0, float yOffset = 0)
        {
            if (e != null)
            {
                var sprite = e.Sprites[e.Index];
                var spriteHandler = new GameObject();
                var render = spriteHandler.AddComponent<SpriteRenderer>();
                var position = c.Handle.transform.position;
                position.x += xOffset;
                position.y += yOffset;
                spriteHandler.transform.position = position;
                if (sort == "CharHelm")
                    spriteHandler.transform.SetParent(c.SpriteHandlerDict["CharHead"].transform);
                else
                    spriteHandler.transform.SetParent(c.Handle.transform);
                spriteHandler.name = e.Name;
                render.sprite = sprite;
                render.sortingLayerName = sort;
                c.SpriteHandlerDict.Add(sort, spriteHandler);
            }
        }
    }
}

