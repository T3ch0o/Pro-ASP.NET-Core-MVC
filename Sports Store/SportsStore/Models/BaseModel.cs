namespace SportsStore.Models
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class BaseModel<T>
    {
        [BindNever]
        public T Id { get; set; }
    }
}
