using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Shrooms.Contracts.Constants;
using TemporaryDataLayer.Models;
using TemporaryDataLayer.Models.Users;

namespace TemporaryDataLayer
{
    public class Exam : BaseModelWithOrg
    {
        //[Required]
        //[StringLength(ValidationConstants.ExamMaxTitleLength)]
        //[Index]
        public string Title { get; set; }

        //[StringLength(ValidationConstants.ExamMaxNumberLength)]
        //[Index]
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
