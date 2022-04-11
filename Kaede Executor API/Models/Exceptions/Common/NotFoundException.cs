namespace Kaede_Executor_API.Models.Exceptions.Common
{
    public class NotFoundException : BaseException
    {
        public NotFoundException()
            : base(1004, "Sorry, we couldn't find the resource you were looking for.")
        {
            StatusCode = 404;
        }
    }
}
