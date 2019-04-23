using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Catalog.Models
{
    public class FilterModel
    {
        public FilterModel(string facilityType, string facilityName, double rating)
        {
            SelectedType = facilityType;
            SelectedName = facilityName;
           // SelectedPrice = price;
            SelectedRating = rating;
            FacilityTypes = new SelectList(
                new List<string>
                {
                    "All",
                    "Pub",
                    "Restaurant",
                    "Hookah"
                }
            );
        }
        public SelectList FacilityTypes { get; private set; }
        public string SelectedType { get; private set; } 
        public string SelectedName { get; private set; }    
        //public double SelectedPrice { get; private set; }
        public double SelectedRating { get; private set; }
    }
}