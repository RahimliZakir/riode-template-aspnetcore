using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Riode.Template.WebUI.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.Models.DataContext
{
    public static class DataSeeding
    {
        async public static Task<IApplicationBuilder> DataSeed(this IApplicationBuilder app)
        {
            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            {
                RiodeDbContext db = scope.ServiceProvider.GetRequiredService<RiodeDbContext>();

                //Auto Database Migrate
                db.Database.Migrate();
                //Auto Database Migrate

                //Faqs
                if (!db.Faqs.Any())
                {
                    await db.Faqs.AddAsync(new Faq
                    {
                        Question = "Kapıda ödeme yapabilir miyim?",
                        Answer = "Kredi kartı veya Trendyol Cüzdan ile online ödeme yapabilirsiniz."
                    });

                    await db.Faqs.AddAsync(new Faq
                    {
                        Question = "Ödememi nasıl yaparım?",
                        Answer = "Ödemeyi kredi kartı, banka kartı ve Trendyol Cüzdanım ile gerçekleştirebilirsiniz. Kart bilgilerinizi girdikten sonra eğer taksit yapılabilen ürünlerse taksit seçeneklerinden birini seçip alışverişinizi tamamlayabilirsiniz.Kapıda ödeme ve havale yoluyla ödeme seçenekleri bulunmamaktadır."
                    });

                    await db.Faqs.AddAsync(new Faq
                    {
                        Question = "Hızlı Market siparişlerinde herhangi bir alt limit var mıdır?",
                        Answer = "Evet, minimum sepet tutarı seçtiğiniz mağazaya göre değişebilmektedir. Sipariş vermek istediğiniz mağaza görselinde minimum tutar bilgisini görüntüleyebilirsiniz."
                    });

                    await db.Faqs.AddAsync(new Faq
                    {
                        Question = "Nerelerden sipariş verebilirim?",
                        Answer = "Tüm ihtiyaçlarınıza 600'den fazla yerel ve ulusal zincir market ağımızla hizmet veriyoruz. Konumunuza bağlı olarak hizmet alabileceğiniz marketler farklılık gösterebilir. Uygulama üzerinden adres bilgilerinizi girerek bölgenizde hizmet veren mağazalarımızı görüntüleyebilirsiniz."
                    });

                    await db.SaveChangesAsync();
                }
                //Faqs
            }

            return app;
        }
    }
}
