using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication6.Models
{
    [MetadataType(typeof(GroupMetaData))]
    public partial class Group
    {

    }

    public class GroupMetaData
    {
        [Required (AllowEmptyStrings = false,ErrorMessage = "Please provide 'Group Name'")]
        public string GroupName { get; set; }
    }
}