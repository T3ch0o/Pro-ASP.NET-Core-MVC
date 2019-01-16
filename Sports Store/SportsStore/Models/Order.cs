namespace SportsStore.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class Order : BaseModel<int>
    {
        [BindNever]
        public ICollection<CartLine> CartLines { get; set; }

        [Display(Name = "Name:")]
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }

        [Display(Name = "Line 1:")]
        [Required(ErrorMessage = "Please enter the first address line")]
        public string Line1 { get; set; }

        [Display(Name = "Line 2:")]
        public string Line2 { get; set; }

        [Display(Name = "Line 3:")]
        public string Line3 { get; set; }

        [Display(Name = "City:")]
        [Required(ErrorMessage = "Please enter a city name")]
        public string City { get; set; }

        [Display(Name = "State:")]
        [Required(ErrorMessage = "Please enter a state name")]
        public string State { get; set; }

        [Display(Name = "Zip:")]
        public string Zip { get; set; }

        [Display(Name = "Country:")]
        [Required(ErrorMessage = "Please enter a country name")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
