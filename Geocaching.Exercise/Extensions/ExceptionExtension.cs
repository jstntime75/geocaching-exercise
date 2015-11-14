using System;
using System.Text;

namespace Geocaching.Exercise
{
    public static class ExceptionExtension
    {
        public static string GetExceptionDetail(this Exception ex)
        {
            var buffer = new StringBuilder();
            BuildExceptionDetail(ex, buffer);
            return buffer.ToString();
        }

        public static string GetAllExceptions(this Exception ex)
        {
            var buffer = new StringBuilder();
            BuildAllExceptions(ex, buffer);
            return buffer.ToString();
        }

        public static string GetDetailMessage(this Exception exc)
        {
            return exc.InnerException != null ? string.Format("{0}. Inner exception: {1}", exc.Message, exc.InnerException.GetDetailMessage()) : exc.Message;
        }

        private static void BuildExceptionDetail(Exception exception, StringBuilder buffer)
        {
            if (exception == null)
            {
                throw new ArgumentNullException("exception");
            }

            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

            var localBuffer = new StringBuilder();
            localBuffer.Append("Exception: ").AppendLine(exception.GetType().ToString());
            localBuffer.Append("Source: ").AppendLine(exception.Source);
            localBuffer.Append("Message: ").AppendLine(exception.Message);
            localBuffer.Append("StackTrace: ").AppendLine(exception.StackTrace);

            string localText = localBuffer.ToString();

            buffer.AppendLine(localText);
            if (exception.InnerException != null)
            {
                buffer.AppendLine("Inner Exception: ");
                BuildExceptionDetail(exception.InnerException, buffer);
            }
        }

        private static void BuildAllExceptions(Exception exception, StringBuilder buffer)
        {
            if (exception == null)
            {
                return;
            }

            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

            buffer.AppendLine(string.Empty);
            buffer.AppendLine(exception.Message);
            BuildAllExceptions(exception.InnerException, buffer);
        }
    }
}
