
namespace Vehicle
{
    internal class FirstOrDefault
    {
        private Func<object, bool> p;

        public FirstOrDefault(Func<object, bool> p)
        {
            this.p = p;
        }
    }
}