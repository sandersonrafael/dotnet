using Flunt.Validations;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FinalProject.Domain.Products;

public class Category : Entity
{

    public bool Active { get; private set; }

    /* Example */
    //public string Email { get; set; }
    /* Example */

    public Category(string name/*, string email*/, string createdBy, string editedBy) : base() 
    {
        Name = name;
        Active = true;
        CreatedBy = createdBy;
        EditedBy = editedBy;
        CreatedOn = DateTime.UtcNow;
        EditedOn = DateTime.UtcNow;

        ValidateFields();
    }

    public void EditInfo(string name, bool active)
    {
        Active = active;
        Name = name;

        ValidateFields();
    }

    private void ValidateFields()
    {
        // Using Flunt:
        // To add a custom message, use the third argument
        var contract = new Contract<Category>()
            .IsNotNullOrEmpty(Name, "name", "name can't be null or empty. Please try again with a valid value")
            .IsGreaterOrEqualsThan(Name, 3, "name")
            .IsNotNullOrEmpty(CreatedBy, "createdBy", "createdBy can't be null or empty. Please try again with a valid value")
            .IsNotNullOrEmpty(EditedBy, "editedBy", "editedBy can't be null or empty. Please try again with a valid value")
            /*.IsNotNullOrEmpty(email, "Email")
            .IsEmail(email, "Email")*/;

        AddNotifications(contract);
    }
}
