using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Riode.Template.WebUI.Models.DataContext;
using Riode.Template.WebUI.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.Controllers
{
    public class HomeController : Controller
    {
        readonly RiodeDbContext db;
        readonly IConfiguration conf;

        public HomeController(RiodeDbContext db, IConfiguration conf)
        {
            this.db = db;
            this.conf = conf;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        async public Task<IActionResult> ContactUs(Contact contact)
        {
            if (ModelState.IsValid)
            {
                SmtpClient client = new SmtpClient()
                {
                    Host = "smtp.mail.ru",
                    EnableSsl = true,
                    Port = 25
                };

                client.Credentials = new NetworkCredential(conf.GetValue<string>("FactoryCredentials:Email"), conf["FactoryCredentials:Pwd"]);

                MailMessage message = new MailMessage(conf.GetValue<string>("FactoryCredentials:Email"), contact.Email);
                message.Subject = "Sizin şərhiniz uğurla bizə göndərildi!";
                message.Body = $"Hörmətli {contact.Name}, sizin şərhiniz bizə uğurla göndərildi, komandamız ən qısa vaxtda şərhinizi cavablayacaqdır! Təşəkkür edirik!";

                try
                {
                    client.Send(message);

                    await db.Contacts.AddAsync(contact);
                    await db.SaveChangesAsync();

                    return Json(new
                    {
                        error = false,
                        message = "Şərhiniz uğurla göndərildi,  e-mail-inizi yoxlayın!"
                    });
                }
                catch (Exception)
                {
                    return Json(new
                    {
                        error = true,
                        message = "Məlumatlar yaddaşda saxlanılan zaman xəta baş verdi!"
                    });
                }
            }

            //return View();

            return Json(new
            {
                error = true,
                message = "Məlumatları doldurun!"
            });
        }

        async public Task<IActionResult> Faqs()
        {
            IEnumerable<Faq> data = await db.Faqs.Where(f => f.DeletedDate == null).ToListAsync();

            return View(data);
        }

        public IActionResult Error404()
        {
            return View();
        }

        [HttpPost]
        async public Task<IActionResult> Subscribe(string email)
        {
            Subscribe check = await db.Subscribes.FirstOrDefaultAsync(c => c.Email.Equals(email));

            if (check != null && (check.EmailConfirmed == null || check.EmailConfirmedDate == null))
            {
                return Json(new
                {
                    error = true,
                    message = "Mail-ə göndərilən linklə təsdiqləmə tamamlanmayıb!"
                });
            }
            else if (check != null && (check.EmailConfirmed != null || check.EmailConfirmedDate != null))
            {
                return Json(new
                {
                    error = false,
                    message = "Siz artıq bizim abunəliyimizə qoşulmusunuz!"
                });
            }

            SmtpClient client = new SmtpClient()
            {
                Host = "smtp.mail.ru",
                EnableSsl = true,
                Port = 25
            };

            client.Credentials = new NetworkCredential(conf.GetValue<string>("FactoryCredentials:Email"), conf["FactoryCredentials:Pwd"]);

            string token = $"subscribe-{check.Id}-{DateTime.Now:yyyyMMddHHmmss}";

            string path = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}?token={token}";

            MailMessage message = new MailMessage(conf.GetValue<string>("FactoryCredentials:Email"), email);
            message.Subject = "Uğurla abuna oldunuz!";
            message.Body = $"Hörmətli {email}, bu linkə <a href='{path}'>klik</a> edərək abunəliyinizi təsdiqləyin! Təşəkkür edirik!";
            message.IsBodyHtml = true;

            try
            {
                message.CC.Add(conf.GetValue<string>("FactoryCredentials:CC"));
                client.Send(message);

                Subscribe subscribe = new Subscribe
                {
                    Email = email
                };

                await db.Subscribes.AddAsync(subscribe);
                await db.SaveChangesAsync();

                return Json(new
                {
                    error = false,
                    message = "Abunəliyinizi təsdiq etmək üçün e-mail-inizə təsdiq mesajı göndərildi!"
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    error = true,
                    message = "Məlumatlar yaddaşda saxlanılan zaman xəta baş verdi!"
                });
            }
        }
    }
}
