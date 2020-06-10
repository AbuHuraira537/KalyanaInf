using System;
using System.Collections.Generic;

namespace KalyanaInfo.Models
{
    public partial class Person
    {
        public Person()
        {
            Mosque = new HashSet<Mosque>();
            Post = new HashSet<Post>();
            School = new HashSet<School>();
            Shope = new HashSet<Shope>();
            Video = new HashSet<Video>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public string SonOrDaughterOf { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string Hobby { get; set; }
        public string Mobile { get; set; }
        public string IdCardOrBForm { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int HasMobile { get; set; }
        public int Gender { get; set; }
        public int Education { get; set; }
        public int Profession { get; set; }
        public int Vehicle { get; set; }
        public int Family { get; set; }
        public string About { get; set; }
        public string Married { get; set; }

        public virtual Education EducationNavigation { get; set; }
        public virtual Family FamilyNavigation { get; set; }
        public virtual Gender GenderNavigation { get; set; }
        public virtual Mobile HasMobileNavigation { get; set; }
        public virtual Profession ProfessionNavigation { get; set; }
        public virtual Vehicle VehicleNavigation { get; set; }
        public virtual ICollection<Mosque> Mosque { get; set; }
        public virtual ICollection<Post> Post { get; set; }
        public virtual ICollection<School> School { get; set; }
        public virtual ICollection<Shope> Shope { get; set; }
        public virtual ICollection<Video> Video { get; set; }
    }
}
