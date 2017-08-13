using Assets.Model.Map;
using Assets.View;
using Template.Hex;
using UnityEngine;

namespace Assets.Controller.Map.Combat.Loader
{
    public class MapLoader
    {
        private MapController _map;

        public Transform MapHolder;
        public Transform BackgroundTiles;

        public MapLoader()
        {
            this.MapHolder = new GameObject("Map").transform;
            this.MapHolder.tag = "BattleMap";
            this.BackgroundTiles = new GameObject("BackgroundTiles").transform;
            this.BackgroundTiles.transform.SetParent(this.MapHolder);
            this._map = new MapController();
        }

        public void Init(MapInitInfo info)
        {
            var hexMap = HexMapBuilder.GetMap(info.Rows, info.Cols, ViewParams.OFFSET, ViewParams.MAP_CENTER);
            var map = new MMap(hexMap);
            this._map.SetMap(map);
            this.InitTiles(info);
            this.InitParties(info);
            this.InitChars(info);
        }

        //public void BuildAndPlaceCharacter(
        //    CharParams cParams,
        //    ref List<CharController> controllers,
        //    TileController tile,
        //    bool lParty = false)
        //{
        //    var builder = new CharacterViewBuilder();
        //    var handle = new GameObject();
        //    var controller = handle.AddComponent<CharController>();
        //    controller.Init(handle);
        //    var view = builder.Build(cParams);
        //    controller.SetView(view, cParams);
        //    this.LayoutCharacterAtTile(controller, tile, cParams, lParty);
        //    controllers.Add(controller);
        //    controller.LParty = lParty;
        //}

        private void InitChars(MapInitInfo info)
        {
            var loader = new CharLoader();
            loader.Init(this._map, info);
        }

        private void InitParties(MapInitInfo info)
        {
            var loader = new PartyLoader();
            loader.Init(this._map, info);
        }

        private void InitTiles(MapInitInfo info)
        {
            var loader = new TileLoader();
            loader.InitTiles(this._map, info, BackgroundTiles);
            loader.InitMapDeco(this._map, info);
        }

        

        //private void AttachDeco(CharController c, string sort, int spriteIndex, TileController tile)
        //{
        //    if (c.Model.Type == ECharType.Humanoid && spriteIndex >= 0)
        //    {
        //        var sprite = c.View.Sprites[spriteIndex];
        //        var spriteHandler = new GameObject();
        //        var render = spriteHandler.AddComponent<SpriteRenderer>();
        //        spriteHandler.transform.position = c.Handle.transform.position;
        //        if (sort == Layers.CHAR_FACE || sort == Layers.CHAR_HEAD_DECO_1 || sort == Layers.CHAR_HEAD_DECO_2)
        //            spriteHandler.transform.SetParent(c.SpriteHandlerDict[Layers.CHAR_HEAD].transform);
        //        else
        //            spriteHandler.transform.SetParent(c.Handle.transform);
        //        spriteHandler.name = "Character Deco";
        //        render.sprite = sprite;
        //        render.sortingLayerName = sort;
        //        c.SpriteHandlerDict.Add(sort, spriteHandler);
        //    }
        //}

        //private void AttachHead(CharController c, string sort, int spriteIndex, TileController tile)
        //{
        //    if (c.Model.Type == ECharType.Humanoid)
        //    {
        //        var sprite = c.View.Sprites[spriteIndex];
        //        var spriteHandler = new GameObject();
        //        var render = spriteHandler.AddComponent<SpriteRenderer>();
        //        spriteHandler.transform.position = c.Handle.transform.position;
        //        spriteHandler.transform.SetParent(c.Handle.transform);
        //        spriteHandler.name = "Character Head";
        //        render.sprite = sprite;
        //        render.sortingLayerName = sort;
        //        c.SpriteHandlerDict.Add(sort, spriteHandler);
        //    }
        //}

        //private void AttachMount(CharController c, string sort, TileController tile)
        //{
        //    var sprite = c.View.Mount.Sprites[0];
        //    var spriteHandler = new GameObject();
        //    var render = spriteHandler.AddComponent<SpriteRenderer>();
        //    var position = tile.View.Center;
        //    position.x += ViewParams.MOUNT_X_OFFSET;
        //    position.y -= ViewParams.MOUNT_Y_OFFSET;
        //    spriteHandler.transform.position = position;
        //    spriteHandler.transform.SetParent(c.Handle.transform);
        //    spriteHandler.name = c.View.Name + " " + c.View.Mount.Name + " Mount";
        //    render.sprite = sprite;
        //    render.sortingLayerName = sort;
        //    c.SpriteHandlerDict.Add(sort, spriteHandler);
        //    var mountOffsetPos = c.Handle.transform.position;
        //    mountOffsetPos.y += ViewParams.MOUNT_Y_OFFSET;
        //    c.transform.position = mountOffsetPos;
        //}

        //private void BuildAndLayoutCharacter(
        //    CharParams cParams,
        //    ref List<CharController> controllers,
        //    bool lParty = false)
        //{
        //    var builder = new CharacterViewBuilder();
        //    var handle = new GameObject();
        //    var controller = handle.AddComponent<CharController>();
        //    controller.Init(handle);
        //    var view = builder.Build(cParams);
        //    controller.SetView(view, cParams);
        //    this.LayoutCharacter(controller, cParams, lParty);
        //    controllers.Add(controller);
        //    controller.LParty = lParty;
        //}

        //private void InitParties()
        //{
        //    var lParty = new List<CharController>();
        //    var rParty = new List<CharController>();
        //    this.InitPlayerParty(ref lParty);
        //    this.InitEnemyParty(ref rParty);
        //    //var e = new EvCombatMapLoaded(CombatEventManager.Instance, lParty, rParty, this._map, this);
        //}

        //private void InitPlayerParty(ref List<CharController> controllers)
        //{
        //    //var playerChars = EnemyPartyLoader.Instance.GetParty(new Pair<string, int>("Orcs", 20));
        //    //for (int i = 0; i < playerChars.Count; i++)
        //    //    this.BuildAndLayoutCharacter(playerChars[i], ref controllers, true);
        //}

        //private void InitEnemyParty(ref List<CharController> controllers)
        //{
        //    //var enemies = EnemyPartyLoader.Instance.GetParty(new Pair<string, int>("Lizardman War Party", 20));
        //    //for (int i = 0; i < enemies.Count; i++)
        //    //    this.BuildAndLayoutCharacter(enemies[i], ref controllers);
        //}

        //private void InitGUI()
        //{
        //    //for (int i = 0; i < 7; i++)
        //    //{
        //    //    var tag = "WpnBtnTag" + i;
        //    //    var btnContainer = GameObject.FindGameObjectWithTag(tag);
        //    //    var script = btnContainer.AddComponent<WpnBtnClick>();
        //    //    script.Init(tag);
        //    //}
        //    //var abilitiesBtn = GameObject.FindGameObjectWithTag("AbilitiesBtnTag");
        //    //var clickScript = abilitiesBtn.AddComponent<AbilityModalBtnClick>();
        //    //clickScript.Init("AbilitiesBtnTag");
        //}

        //private void LayoutCharacter(CharController c, CharParams cParams, bool LParty)
        //{
        //    if (c.View != null)
        //    {
        //        var tile = this._map.GetTileForRow(LParty, cParams.StartRow);
        //        this.LayoutCharacterAtTile(c, tile, cParams, LParty);
        //    }
        //}

        //private void LayoutCharacterAtTile(CharController c, TileController tile, CharParams cParams, bool lParty)
        //{
        //    if (c.View != null)
        //    {
        //        var sprite = c.View.Sprites[c.View.Torso];
        //        var render = c.Handle.AddComponent<SpriteRenderer>();
        //        c.Handle.transform.position = tile.View.Center;
        //        c.Handle.transform.SetParent(this.MapHolder);
        //        c.Handle.name = c.View.Type.ToString() + " " + c.View.Race.ToString();
        //        render.sprite = sprite;
        //        render.sortingLayerName = Layers.CHAR_TORSO;
        //        c.SpriteHandlerDict.Add(Layers.CHAR_TORSO, c.Handle);
        //        AttachHead(c, Layers.CHAR_HEAD, c.View.Head, tile);
        //        c.SpriteHandlerDict.Add(Layers.CHAR_MAIN, c.Handle);
        //        if (c.View.Mount != null) { AttachMount(c, Layers.CHAR_MOUNT, tile); }
        //        AttachDeco(c, Layers.CHAR_FACE, c.View.Face, tile);
        //        AttachDeco(c, Layers.CHAR_HEAD_DECO_1, c.View.HeadDeco1, tile);
        //        AttachDeco(c, Layers.CHAR_HEAD_DECO_2, c.View.HeadDeco2, tile);
        //        AttachDeco(c, Layers.CHAR_TORSO_DECO_1, c.View.TorsoDeco1, tile);
        //        AttachDeco(c, Layers.CHAR_TORSO_DECO_2, c.View.TorsoDeco2, tile);
        //        if (c.View.Armor != null) { TryAttachEquipment(c, c.View.Armor, Layers.CHAR_ARMOR, tile); }
        //        if (c.View.Helm != null) { TryAttachEquipment(c, c.View.Helm, Layers.CHAR_HELM, tile, 0f, ViewParams.HELM_OFFSET); }
        //        if (c.View.LWeapon != null) { TryAttachEquipment(c, c.View.LWeapon, Layers.CHAR_L_WEAPON, tile, ViewParams.WEAPON_OFFSET); }
        //        if (c.View.RWeapon != null) { TryAttachEquipment(c, c.View.RWeapon, Layers.CHAR_R_WEAPON, tile, -ViewParams.WEAPON_OFFSET); }
        //        if (!lParty)
        //            c.Handle.transform.localRotation = Quaternion.Euler(0, 180, 0);
        //        tile.SetCurrent(c);
        //        c.CurrentTile = tile;
        //    }
        //}

        //private void TryAttachEquipment(CharController c, VEquipment e, string sort, TileController tile, float xOffset = 0, float yOffset = 0)
        //{
        //    if (e != null)
        //    {
        //        var sprite = e.Sprites[e.Index];
        //        var spriteHandler = new GameObject();
        //        var render = spriteHandler.AddComponent<SpriteRenderer>();
        //        var position = c.Handle.transform.position;
        //        position.x += xOffset;
        //        position.y += yOffset;
        //        spriteHandler.transform.position = position;
        //        if (sort == Layers.CHAR_HELM)
        //            spriteHandler.transform.SetParent(c.SpriteHandlerDict[Layers.CHAR_HEAD].transform);
        //        else
        //            spriteHandler.transform.SetParent(c.Handle.transform);
        //        spriteHandler.name = e.Name;
        //        render.sprite = sprite;
        //        render.sortingLayerName = sort;
        //        c.SpriteHandlerDict.Add(sort, spriteHandler);
        //    }
        //}
    }
}

