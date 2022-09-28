using System.Collections.Generic;
using System.Linq;

namespace Shrooms.DataLayer.EntityModels.Models
{
    public class Certificate : AbstractClassifier
    {
        public virtual IEnumerable<Exam> Exams
        {
            get => ExamCertificates.Select(model => model.Exam); 
        }

        public bool InProgress { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        // Required for many-to-many
        public ICollection<ExamCertificate> ExamCertificates { get; set; }
    }
}