using System;
namespace NLayerApp.BLL.Exceptions
{
    public class ModelErrorException : Exception
    {
        public ModelErrorException() : base()
        { }
        public ModelErrorException(string message)
        : base(message)
        {
        }
        public ModelErrorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

