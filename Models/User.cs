using Microsoft.AspNetCore.Identity;
using RASCHET_HASHODOV.Models;
using System.Collections.Generic;

public class User : IdentityUser
{
    public User()
    {
        Expenses = new HashSet<Expense>();
    }

    public virtual ICollection<Expense> Expenses { get; set; }
}
