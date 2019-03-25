namespace Brother.Bpac
{
    public class BpacObject
    {
        private readonly bpac.Object _obj;

        public string Text { get => _obj.Text; set => _obj.Text = value; }

        internal BpacObject(bpac.Object obj)
        {
            _obj = obj;
        }
    }
}
