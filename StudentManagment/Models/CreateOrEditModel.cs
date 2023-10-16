using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagment.Models
{
    public class CreateOrEditModel
    {

        public Student student { get; set; }

        public bool IsEditMode =>(student != null ? true : false);




    }
}
