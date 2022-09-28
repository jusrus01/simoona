using System.Collections.Generic;
using System.Linq;
using Shrooms.DataLayer.EntityModels.ModelsCore.Users;

namespace Shrooms.DataLayer.EntityModels.ModelsCore
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
        internal ICollection<ApplicationUserExam> ApplicationUserExams { get; set; }

        internal ICollection<ExamCertificate> ExamCertificates { get; set; }
    }
}
