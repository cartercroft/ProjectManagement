namespace ProjectManagement.Classes
{
    public class Response<T> : Response
    {
        public T? Result { get; set; } = default(T);
    }
    public class Response
    {
        public bool IsSuccess => String.IsNullOrEmpty(ErrorMessage);
        public string ErrorMessage { get; set; } = null!;
    }
}
