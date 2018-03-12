namespace PeNN.Data
{
    public abstract class Info
    {
        public DataShape Shape;

        public Label Information;

        public Info()
        {
            this.Information = null;
        }
    }
}
