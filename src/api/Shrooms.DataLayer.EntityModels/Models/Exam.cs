using System.Collections.Generic;
using System.Linq;
using Shrooms.DataLayer.EntityModels.Models.Users;

namespace Shrooms.DataLayer.EntityModels.Models
{
    public class Exam : BaseModelWithOrg
    {
        public string Title { get; set; }

        public string Number { get; set; }

        public virtual IEnumerable<Certificate> Certificates
        { 
            get => ExamCertificates.Select(model => model.Certificate);
        }

        public IEnumerable<ApplicationUser> ApplicationUsers 
        {
            get => ApplicationUserExams.Select(model => model.ApplicationUser);
        }

        // Required for many-to-many
        public ICollection<ApplicationUserExam> ApplicationUserExams { get; set; }

        public ICollection<ExamCertificate> ExamCertificates { get; set; }
    }
}
