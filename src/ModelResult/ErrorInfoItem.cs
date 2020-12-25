namespace Byndyusoft.ModelResult
{
    public class ErrorInfoItem
    {
        public ErrorInfoItem(string propertyName, string error)
        {
            PropertyName = propertyName;
            Error = error;
        }

        public string PropertyName { get; private set; }

        public string Error { get; private set; }

        public override string ToString()
        {
            return $"\"{PropertyName}\": {Error}";
        }
    }
}