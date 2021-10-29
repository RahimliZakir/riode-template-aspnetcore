using Riode.Template.WebUI.AppCode.Infrastructure;

namespace Riode.Template.WebUI.Models.Entity
{
    public class SpecificationCategoryItem : HistoryWatch
    {
        public int SpecificationId { get; set; }

        public virtual Specification Specification { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
