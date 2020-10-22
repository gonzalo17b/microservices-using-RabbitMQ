using System;

namespace RabbitsBurrow.Infrastructure
{
    public static class Ensure
    {
        public static void ThatIsNotNull(object parameter)
        {
            if (parameter == null)
            {
                var message = $"The parameter '{parameter}' is not valid.";
                throw new ArgumentNullException(message);
            }
        }

        public static void That<TException>(bool condition, string message = "") 
            where TException : Exception, new()
        {
            if (!condition)
            {
                throw (TException)Activator.CreateInstance(typeof(TException), message);
            }
        }
    }
}
