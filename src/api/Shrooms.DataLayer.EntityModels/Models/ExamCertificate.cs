namespace Shrooms.DataLayer.EntityModels.Models
{
    public class ExamCertificate
    {
        public int ExamId { get; set; }

        public Exam Exam { get; set; }

        public int CertificateId { get; set; }

        public Certificate Certificate { get; set; }
    }
}
