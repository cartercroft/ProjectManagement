namespace ProjectManagement.Public.Models
{
    public class ViewModelBase
    {
        //If I need to extend upon the functionality of IDs to allow for different types, just take a TKey type param in the class definition :)
        public int Id { get; set; }
    }
}
