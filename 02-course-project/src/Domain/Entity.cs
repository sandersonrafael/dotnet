using Flunt.Notifications;

namespace FinalProject.Domain;

// Notifiable<Notification> -> Comes from Flunt, to validate class fields
// To do the validations, it will need to use constructors
public abstract class Entity : Notifiable<Notification> 
    // To Don't save Notifiable class properties on database, it will need to modify DbContext method OnModelSaving to ignore he
{
    public Guid Id { get; set; }
    public string Name { get; protected set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string EditedBy { get; set; }
    public DateTime EditedOn { get; set; }

    public Entity()
    {
        Id = Guid.NewGuid();
    }
}
