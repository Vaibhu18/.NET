using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PaymentDetail
{
    [Key]
    public int PaymentId { get; set; }

    [Column(TypeName = "varchar(16)")]
    public string CardNumber { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string CardHolderName { get; set; }

    [Column(TypeName = "varchar(5)")]
    public string ExpirationDate { get; set; }

    [Column(TypeName = "varchar(3)")]
    public string SecurityCode { get; set; }
}
