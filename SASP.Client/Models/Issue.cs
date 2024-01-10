using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASP.Client.Models
{
    public class Issue
    {
        public int IssueId { get; set; }

        public int CatalogId { get; set; }

        public int TypeIssueId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}
