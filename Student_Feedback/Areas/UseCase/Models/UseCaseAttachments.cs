using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gios_mvcSolution.Areas.UseCase.Models
{
    public class UseCaseAttachments
    {
        [Key]
        [Display(Name = "Record ID")]
        public int lngRecordID { get; set; }

        public int intUseCaseID { get; set; }

        public string strFileName { get; set; }

        public byte[] Attachment { get; set; }

        public string strFileType { get; set; }

        [Display(Name = "Date Created")]
        public Nullable<System.DateTime> dtmCreated { get; set; }
        [Display(Name = "Created By")]
        public string strCreatorID { get; set; }
        [Display(Name = "Last Modified")]
        public Nullable<System.DateTime> dtmModified { get; set; }
        [Display(Name = "Modified By")]
        public string strModifierID { get; set; }

        public ICollection<FileTypes> FileTypes { get; set; }
    }
}