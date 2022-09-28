using System.Collections.Generic;
using System.Linq;

namespace Shrooms.DataLayer.EntityModels.ModelsCore
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
        internal ICollection<ExamCertificate> ExamCertificates { get; set; }
    }
}