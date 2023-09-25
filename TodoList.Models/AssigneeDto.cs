using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models.Enums;

namespace TodoList.Models
{
    public class AssigneeDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
    }
}