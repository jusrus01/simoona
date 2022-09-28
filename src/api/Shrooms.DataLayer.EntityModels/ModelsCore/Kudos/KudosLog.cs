using System;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Enums;
using Shrooms.Contracts.Exceptions;

namespace Shrooms.DataLayer.EntityModels.ModelsCore.Kudos
{
    public class KudosLog : BaseModelWithOrg
    {
        public string EmployeeId { get; set; }

        public virtual ApplicationUser Employee { get; set; }

        public string KudosTypeName { get; set; }

        public decimal KudosTypeValue { get; set; }

        public KudosTypeEnum KudosSystemType { get; set; }

        public KudosStatus Status { get; set; }

        public decimal Points { get; set; }

        public string Comments { get; set; }

        public int MultiplyBy { get; set; }

        public int? KudosBasketId { get; set; }

        public virtual KudosBasket KudosBasket { get; set; }

        public string RejectionMessage { get; set; }

        public bool IsRecipientDeleted() => !string.IsNullOrEmpty(EmployeeId) && Employee == null;

        public bool IsMinus() => KudosSystemType == KudosTypeEnum.Minus;

        public string PictureId { get; set; }

        public void Approve(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ValidationException(ErrorCodes.UserNotFound, "User not found");
            }

            if (userId == CreatedBy)
            {
                throw new ValidationException(ErrorCodes.SenderReceiverCannotAcceptRejectKudos, "Sender/Receiver cannot accept/reject kudos logs");
            }

            if (userId == EmployeeId)
            {
                throw new ValidationException(ErrorCodes.CanNotSendKudosToSelf, "Kudos receiver can not be a sender");
            }

            ValidateStatusPending();

            Status = KudosStatus.Approved;
            Modified = DateTime.UtcNow;
            ModifiedBy = userId;
        }

        public void Reject(string userId, string reason)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ValidationException(ErrorCodes.UserNotFound, "User not found");
            }

            if (string.IsNullOrEmpty(reason))
            {
                throw new ValidationException(ErrorCodes.ContentDoesNotExist, "Rejection message is empty");
            }

            ValidateStatusPending();

            Status = KudosStatus.Rejected;
            RejectionMessage = reason;
            Modified = DateTime.UtcNow;
            ModifiedBy = userId;
        }

        private void ValidateStatusPending()
        {
            if (Status != KudosStatus.Pending)
            {
                throw new ValidationException(ErrorCodes.KudosAlreadyApproved, "Kudos log is already approved");
            }
        }
    }
}
