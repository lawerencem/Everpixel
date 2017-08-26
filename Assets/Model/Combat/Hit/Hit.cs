namespace Assets.Model.Combat.Hit
{
    public class Hit : AHit
    {
        public Hit(HitData d) : base(d) { }
    }
}

//public Hit(AbilityArgContainer arg)
//{

//}

//public void Done()
//{
//    this.IsFinished = true;
//    foreach (var callback in this._callbacks)
//        callback(this);
//}

//public void AddEffect(MEffect e)
//{
//    this._effects.Add(e);
//}

//public void AddEvent(MEvCombat e)
//{
//    e.AddCallback(this.Callback);
//    this._events.Add(e);
//    this._events.Sort((x, y) => x.Priority - y.Priority);
//}

//public void AddCallback(Callback callback)
//{
//    this._callbacks.Add(callback);
//}

//public void DoCallbacks()
//{
//    throw new NotImplementedException();
//}

//public void SetCallback(Callback callback)
//{
//    this._callbacks = new List<Callback>() { callback };
//}

//private void Callback(object o)
//{

//}
