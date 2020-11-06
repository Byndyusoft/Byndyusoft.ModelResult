namespace Byndyusoft.ModelResult
{

    public class ErrorInfoItem
    {
        public string PropertyName { get; private set; }

        public string Error { get; private set; }

        public ErrorInfoItem(string propertyName, string error)
        {
            PropertyName = propertyName;
            Error = error;
        }
    }
}