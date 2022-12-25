using System;
namespace NLayerApp.BLL.Exceptions
{
	public class ArleadyExistsException:Exception
	{
		public ArleadyExistsException():base()
		{}
        public ArleadyExistsException(string message)
        : base(message)
        {
        }
        public ArleadyExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        public ArleadyExistsException(string name, object key)
            : base($"Entity \"{name}\" with field=({key}) arleady exists.")
        {
        }
    }
}

