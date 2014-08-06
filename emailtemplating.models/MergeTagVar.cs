using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailTemplating.Models
{
    public class MergeTagVar
    {
        #region Persisted Properties
        [Key]
        [Required]
        public int MergeTagVarID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Value { get; set; }

        #endregion

    }
}
