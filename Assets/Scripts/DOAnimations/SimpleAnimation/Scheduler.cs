
namespace _BaseFeatures.DOAnimations.Basic
{
    public class Scheduler : BaseAnimation
    {
        private readonly int _instanceID;
        protected override int GetInstanceID() => _instanceID;
        public Scheduler(int instanceID)
        {
            this._instanceID = instanceID;
        }
        protected override void OnUpdate(float evaluate)
        {
            // Do Nothing
        }
    }
}