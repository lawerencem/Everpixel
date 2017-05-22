using View.Characters;
using Generics;
using Model.Biomes;
using Model.Characters;
using System.Collections.Generic;
using UnityEngine;
using View.Biomes;
using Model.Equipment;
using View.Builders;
using View.Equipment;
using Model.Parties;
using Controller.Characters;
using Assets.Controller.Managers;
using Model.Events.Combat;
using Model.Map;
using Controller.Map;
using View.Map;
using Generics.Hex;
using View.GUI;
using Assets.Generics;

namespace Controller.Managers.Map
{
    public class CombatMapLoader
    {
        private const int ROWS = 12;
        private const int COLS = 15;
        private const float OFFSET = 0.63f;

        private List<TileController> _emptyTiles = new List<TileController>();
        private CombatMap _map;

        public Transform MapHolder;
        public Transform BackgroundTiles;

        public CombatMapLoader() { }

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

        private void InitParties()
        {
            var controllers = new List<GenericCharacterController>();
            this.InitPlayerParty(ref controllers);
            this.InitEnemyParty(ref controllers);
            var e = new MapDoneLoadingEvent(CombatEventManager.Instance, controllers, this._map);
        }

        // TODO: Clean this up when implementing player stuff.
        private void InitPlayerParty(ref List<GenericCharacterController> controllers)
        {
            var playerChars = EnemyPartyLoader.Instance.GetParty(new Pair<string, int>("Orc Shock Troopas", 15));
            var builder = new CharacterViewBuilder();

            for (int i = 0; i < playerChars.Count; i++)
            {
                var handle = new GameObject();
                var controller = handle.AddComponent<GenericCharacterController>();
                controller.Init(handle);
                var view = builder.Build(playerChars[i]);
                controller.SetView(view, playerChars[i]);
                this.LayoutCharacter(controller, playerChars[i], false);
                controllers.Add(controller);
            }
        }

        private void InitEnemyParty(ref List<GenericCharacterController> controllers)
        {
            var enemies = EnemyPartyLoader.Instance.GetParty(new Pair<string, int>("Goblin War Party", 15));
            var builder = new CharacterViewBuilder();

            for(int i = 0; i < enemies.Count; i++)
            {
                var handle = new GameObject();
                var controller = handle.AddComponent<GenericCharacterController>();
                controller.Init(handle);
                var view = builder.Build(enemies[i]);
                controller.SetView(view, enemies[i]);
                this.LayoutCharacter(controller, enemies[i], true);
                controllers.Add(controller);
            }
        }

        private void InitBackgroundTiles(BiomeEnum b)
        {
            var tiles = MapBridge.Instance.GetBackgroundSprites(b);
            this.SetupBackground(tiles);
        }

        private void InitBackgroundDeco(BiomeEnum b)
        {
            // TODO: Clean this up
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
        }

        private void LayoutCharacter(GenericCharacterController c, CharacterParams cParams, bool enemyParty)
        {
            if (c.View != null)
            {
                var sprite = c.View.Sprites[c.View.Torso];
                var render = c.Handle.AddComponent<SpriteRenderer>();
                var tile = this._map.GetTileForRow(enemyParty, cParams.StartRow);
                c.Handle.transform.position = tile.View.Center;
                c.Handle.transform.SetParent(this.MapHolder);
                c.Handle.name = c.View.Type.ToString() + " " + c.View.Race.ToString();
                render.sprite = sprite;
                render.sortingLayerName = "CharTorso";
                c.SpriteHandlers.Add(c.Handle);
                AttachDeco(c, "CharFace", c.View.Face, tile);
                AttachDeco(c, "CharDeco1", c.View.Deco1, tile);
                AttachDeco(c, "CharDeco2", c.View.Deco2, tile);
                AttachDeco(c, "CharDeco3", c.View.Deco3, tile);
                if (c.View.Armor != null) { TryAttachEquipment(c, c.View.Armor, "CharArmor", tile); }
                if (c.View.Helm != null) { TryAttachEquipment(c, c.View.Helm, "CharHelm", tile, 0f, 0.15f); }
                if (c.View.LWeapon != null) { TryAttachEquipment(c, c.View.LWeapon, "CharLWeapon", tile, 0.09f); }
                if (c.View.Mount != null) { AttachMount(c, "CharMount", tile); }
                if (c.View.RWeapon != null) { TryAttachEquipment(c, c.View.RWeapon, "CharRWeapon", tile, -0.09f); }
                if (enemyParty)
                    c.Handle.transform.localRotation = Quaternion.Euler(0, 180, 0);
                tile.Model.Current = c;
                c.CurrentTile = tile;
            }
        }

        private void AttachDeco(GenericCharacterController c, string sort, int spriteIndex, TileController tile)
        {
            var sprite = c.View.Sprites[spriteIndex];
            var spriteHandler = new GameObject();
            var render = spriteHandler.AddComponent<SpriteRenderer>();
            spriteHandler.transform.position = tile.View.Center;
            spriteHandler.transform.SetParent(c.Handle.transform);
            spriteHandler.name = "Character Deco";
            render.sprite = sprite;
            render.sortingLayerName = sort;
            c.SpriteHandlers.Add(spriteHandler);
        }

        private void AttachMount(GenericCharacterController c, string sort, TileController tile)
        {
            var sprite = c.View.Mount.Sprites[0];
            var spriteHandler = new GameObject();
            var render = spriteHandler.AddComponent<SpriteRenderer>();
            var position = tile.View.Center;
            position.x += 0.05f;
            position.y -= 0.15f;
            spriteHandler.transform.position = position;
            spriteHandler.transform.SetParent(c.Handle.transform);
            spriteHandler.name = c.View.Name + " " + c.View.Mount.Name + " Mount";
            render.sprite = sprite;
            render.sortingLayerName = sort;
            c.SpriteHandlers.Add(spriteHandler);
        }

        private void TryAttachEquipment(GenericCharacterController c, EquipmentView e, string sort, TileController tile, float xOffset = 0, float yOffset = 0)
        {
            if (e != null)
            {
                var sprite = e.Sprites[e.Index];
                var spriteHandler = new GameObject();
                var render = spriteHandler.AddComponent<SpriteRenderer>();
                var position = tile.View.Center;
                position.x += xOffset;
                position.y += yOffset;
                spriteHandler.transform.position = position;
                spriteHandler.transform.SetParent(c.Handle.transform);
                spriteHandler.name = e.Name;
                render.sprite = sprite;
                render.sortingLayerName = sort;
                c.SpriteHandlers.Add(spriteHandler);
            }
        }
    }
}

