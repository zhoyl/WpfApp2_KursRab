
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------


namespace WpfApp2
{

using System;
    using System.Collections.Generic;
    
public partial class Trainers
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Trainers()
    {

        this.Contracts = new HashSet<Contracts>();

    }


    public int id_Trainer { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public int Experience { get; set; }

    public string Passport_data { get; set; }

    public string Telephone { get; set; }

    public string Status { get; set; }

    public int id_Category { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Contracts> Contracts { get; set; }

    public virtual Categories Categories { get; set; }

}

}
