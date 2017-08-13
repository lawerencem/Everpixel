using Assets.View.Builder;

namespace Assets.Controller.Map.Combat.Loader
{
    public class CharLoader
    {
        public void Init(MapController map, MapInitInfo info)
        {
            this.InitViews(map, info);
        }

        private void InitViews(MapController map, MapInitInfo info)
        {
            var builder = new CharacterViewBuilder();
            foreach (var party in map.GetLParties())
                foreach (var controller in party.GetChars())
                    controller.SetView(builder.Build(controller.Model.GetParams()));
        }
    }
}
