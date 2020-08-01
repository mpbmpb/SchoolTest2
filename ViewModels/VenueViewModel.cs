using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolTest2.Models;

namespace SchoolTest2.ViewModels
{
    public class VenueViewModel
    {
        public Venue Venue { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<SelectListItem> ContactList { get; set; }

        public VenueViewModel()
        {
        }

        public VenueViewModel(List<Contact> contacts)
        {
            Contacts = contacts;
            Venue = new Venue();
            ContactList = new List<SelectListItem>();
            ContactList.Add(new SelectListItem { Value = "0", Text = "-- select contact --" });

            foreach (var item in contacts)
            {
                ContactList.Add(new SelectListItem
                {
                    Value = item.ContactId.ToString(),
                    Text = item.Name
                });
            }
        }

        public VenueViewModel(Venue venue, List<Contact> contacts)
        {
            Contacts = contacts;
            Venue = venue;
            ContactList = new List<SelectListItem>();
            ContactList.Add(new SelectListItem { Value = "0", Text = "-- select contact --" });

            foreach (var item in contacts)
            {
                ContactList.Add(new SelectListItem
                {
                    Value = item.ContactId.ToString(),
                    Text = item.Name
                });
            }
        }
    }
}
