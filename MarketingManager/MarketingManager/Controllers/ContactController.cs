using Microsoft.AspNetCore.Mvc;
using MarketingManager.Models;
using Newtonsoft.Json;

namespace MarketingManager.Controllers
{
    public class ContactController : Controller
    {

        private readonly MarketingSupportContext _context;

        public ContactController(MarketingSupportContext context)
        {
            _context = context;
        }

        [HttpPost]
        public void UpdateContact(string userId, string contactId, string contactJson)
        {
            Contact? contact = _context.Contacts.Where(c => c.UserId == userId && c.ContactId == contactId).FirstOrDefault();

            if (contact != null)
            {
                _context.Contacts.Remove(contact);  
            }

            contact = JsonConvert.DeserializeObject<Contact>(contactJson);

            if (contact == null)
            {
                return;
            }

            _context.Contacts.Add(contact);

            _context.SaveChanges();
        }


        [HttpPost]
        public void DeleteContact(string userId, string contactId)
        {
            Contact? contact = _context.Contacts.Where(c => c.UserId == userId && c.ContactId == contactId).FirstOrDefault();

            if (contact != null)
            {
                _context.Contacts.Remove(contact);
            }

            _context.SaveChanges();
        }
    }
}
